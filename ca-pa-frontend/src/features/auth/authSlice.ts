import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { localStorage, LocalStorageKeys } from '../../services/persistance/localStorage'
import { AppThunk } from '../../store/store'

interface AuthState {
    accessToken: string | null;

    user: {
        name: string | null;
        userId: string;
        roles: [];
    };

    loggedIn: boolean;
    loading: boolean;
    error?: string;
}

const initialState: AuthState = {
    accessToken: null,

    user: {
        name: null,
        userId: '',
        roles: [],
    },

    loggedIn: false,
    loading: false,
    error: undefined,
};

export const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        setLoading: (state, action: PayloadAction<boolean>) => {
            state.loading = action.payload
        },
        setError: (state, action: PayloadAction<string>) => {
            state.error = action.payload
        },
        loginSuccess: (state) => {
            state.loggedIn = true
            state.error = undefined
        },
        setLoggedIn: (state, action: PayloadAction<boolean>) => {
            state.loggedIn = action.payload
        },
        setUser: (state, action) => {
            state.user = action.payload;
        },
        resetUser: () => {
            return initialState;
        },
    },
});

export const {
    setLoading,
    setError,
    setLoggedIn,
    loginSuccess,
    setUser,
    resetUser
} = authSlice.actions;

export const login = (credentials: { username: string, password: string }): AppThunk => async (dispatch, getState) => {
    const { username, password } = credentials

    try {
        dispatch(setLoading(true))

        // verify user + passwd
        // const response = await ...
        const response = {
            name: username,
            userId: 'id',
            roles: [],
        }

        dispatch(setUser(response))
        localStorage.set(LocalStorageKeys.UserDetails, response)

        dispatch(loginSuccess())
    } catch(e: any) {
        dispatch(setError(e.toString()))
    } finally {
        dispatch(setLoading(false))
    }
}

export const logout = (): AppThunk => async (dispatch, getState) => {
    try {
        // addtional api calls
        // ...

        // clear localStorage
        localStorage.set(LocalStorageKeys.UserDetails, initialState.user)

        dispatch(resetUser())
    } catch(e: any) {
        dispatch(setError(e.toString()))
    } finally {

    }
}

export default authSlice.reducer;
