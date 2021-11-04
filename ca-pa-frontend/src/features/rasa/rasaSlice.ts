import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import RasaService from '../../services/api/RasaService'
import { AppThunk } from '../../store/store'

interface RasaState {
    response: string;

    loading: boolean;
    error: string;
}

const initialState: RasaState = {
    response: "",

    loading: false,
    error: ""
};

export const rasaSlice = createSlice({
    name: 'rasa',
    initialState,
    reducers: {
        setResponse: (state, action: PayloadAction<string>) => {
            state.response = action.payload
        },
        setLoading: (state, action: PayloadAction<boolean>) => {
            state.loading = action.payload
        },
        setError: (state, action: PayloadAction<string>) => {
            state.error = action.payload
        },
    },
});

const {
    setResponse,
    setLoading,
    setError,
} = rasaSlice.actions;

const send = (message: string): AppThunk => async (dispatch, getState) => {
    try {
        dispatch(setLoading(true))
        const response = await RasaService.send(message)
        dispatch(setResponse(response.message || "*no response*"))
    } catch(e: any) {
        dispatch(setError(e.toString()))
    } finally {
        dispatch(setLoading(false))
    }
}

export const rasaActions = {
    setResponse,
    setLoading,
    setError,
    send,
}

export default rasaSlice.reducer;
