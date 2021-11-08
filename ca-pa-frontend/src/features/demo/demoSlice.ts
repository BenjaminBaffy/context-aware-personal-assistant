import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { AppThunk } from "../../store/store";

// 1) create interface for slice state
export interface IDemoState {
    demoStateField: string;
    loading: boolean;
    error: string;
    something: any[];
}

// 2) initial values for state
const initialState: IDemoState = {
    demoStateField: 'Initial state is (re)set when reloaded.',
    loading: false,
    error: '',
    something: [],
}

// 3) create slice with name + initialState + reducers
const demoSlice = createSlice({
    name: 'demo',
    initialState,
    reducers: {
        setLoading(state, action: PayloadAction<boolean>) {
            state.loading = action.payload
        },
        setError(state, action: PayloadAction<string>) {
            state.error = action.payload
        },
        getSomethingSuccess(state, action: PayloadAction<any[]>) {
            state.something = action.payload
        },
        setDemoStateField(state, action: PayloadAction<string>) {
            state.demoStateField = action.payload
            // return { ...state, demoStateField: action.payload } // should be copy of state, aka functional, but Redux-Toolkit handles it imperatively
        },
    }
})

// 4) extract actions
const {
    setDemoStateField,
    setLoading,
    setError,
    getSomethingSuccess,
} = demoSlice.actions

// 5) create async actions with possible side effects (aka Thunks)
const getSomething = (): AppThunk => async (dispatch, getState) => {
    // access current state if necessary
    const { demoStateField } = getState().demo

    // construct api call request payload
    const payload = {
        // ...
    }

    try {
        // dispatch setLoading true
        dispatch(setLoading(true))

        // await api call (payload)
        // ...
        const result: any[] = []

        // dispatch success
        dispatch(getSomethingSuccess(result))
    } catch (error) {
        // dispatch error msg
    } finally {
        // dispatch setLoading false
        dispatch(setLoading(false))
    }
}

// 6) export actions
export const demoActions = {
    setDemoStateField,
    setLoading,
    setError,
    getSomethingSuccess,
    getSomething,
}

// 7) export the reducer of the slice
export default demoSlice.reducer