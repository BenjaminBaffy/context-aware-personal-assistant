import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import Message from '../../models/Message'
import RasaService from '../../services/api/RasaService'
import { AppThunk } from '../../store/store'
import delay from '../../utils/delay'

interface RasaState {
    response: Message;
    lastCommand: Message;

    sendOnSpeechEnd: boolean;

    isMock: boolean;
    loading: boolean;
    error: string;
}

const initialState: RasaState = {
    response: {
        content: ''
    },
    lastCommand: {
        content: ''
    },

    sendOnSpeechEnd: true,
    isMock: false,

    loading: false,
    error: ""
};

export const rasaSlice = createSlice({
    name: 'rasa',
    initialState,
    reducers: {
        setResponse: (state, action: PayloadAction<Message>) => {
            state.response = action.payload
        },
        setLastCommand: (state, action: PayloadAction<Message>) => {
            state.lastCommand = action.payload
        },
        setSendOnSpeechEnd: (state, action: PayloadAction<boolean>) => {
            state.sendOnSpeechEnd = action.payload
        },
        setLoading: (state, action: PayloadAction<boolean>) => {
            state.loading = action.payload
        },
        setError: (state, action: PayloadAction<string>) => {
            state.error = action.payload
        },
        setIsMock: (state, action: PayloadAction<boolean>) => {
            state.isMock = action.payload
        },

    },
});

const {
    setResponse,
    setLastCommand,
    setSendOnSpeechEnd,
    setLoading,
    setError,
    setIsMock,
} = rasaSlice.actions;

const send = (message: string, mock: boolean = false): AppThunk => async (dispatch, getState) => {
    const { isMock } = getState().rasa
    dispatch(setError(''))

    try {
        dispatch(setLoading(true))

        let response: any = null

        if (isMock) {
            const responseMock: any = {
                message: "Hi! How are you?",
            }
            response = await delay(responseMock, 1200)// RasaService.send(message)
        } else {
            response = await RasaService.send(message)
        }

        const payload: Message = {
            user: {
                name: 'Rasa',
            },
            content: response.message,
            uuid: `${Math.random()}`
        }

        dispatch(setResponse(payload))
    } catch(e: any) {
        dispatch(setError(e.toString()))
    } finally {
        dispatch(setLoading(false))
    }
}

export const rasaActions = {
    setResponse,
    setLastCommand,
    setSendOnSpeechEnd,
    setLoading,
    setError,
    setIsMock,
    send,
}

export default rasaSlice.reducer;
