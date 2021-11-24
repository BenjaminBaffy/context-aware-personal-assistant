import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import AuthService from '../../services/api/AuthService';
import { localStorage, LocalStorageKey } from '../../services/persistance/localStorage'
import { AppThunk } from '../../store/store'

interface AuthState {
    accessToken: string | undefined;

    user: {
        name: string | undefined;
        userId: string;
        roles: [];
    };

    loggedIn: boolean;
    loading: boolean;
    error?: string;
}

const initialState: AuthState = {
    accessToken: undefined,

    user: {
        name: undefined,
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
        setAccessToken: (state, action: PayloadAction<string | undefined>) => {
            state.accessToken = action.payload;
        },
        resetUser: () => {
            return initialState;
        },
    },
});

const {
    setLoading,
    setError,
    setLoggedIn,
    loginSuccess,
    setUser,
    setAccessToken,
    resetUser,
} = authSlice.actions;

const login = (credentials: { username: string, password: string }): AppThunk => async (dispatch, getState) => {
    const { username, password } = credentials

    try {
        dispatch(setLoading(true))

        const response = await AuthService.login(username, password)

        const user = {
            name: response.fullName,
            userId: '',
            roles: []
        }

        dispatch(setUser(user))
        dispatch(setAccessToken(response.accessToken))
        localStorage.set(LocalStorageKey.UserDetails, user)
        localStorage.set(LocalStorageKey.AccessToken, response.accessToken)

        dispatch(loginSuccess())
    } catch(e: any) {
        dispatch(setError(e.toString()))
    } finally {
        dispatch(setLoading(false))
    }
}

const logout = (): AppThunk => async (dispatch, getState) => {
    try {
        // addtional api calls
        // ...

        // clear localStorage
        localStorage.set(LocalStorageKey.UserDetails, initialState.user)
        localStorage.remove(LocalStorageKey.AccessToken)

        dispatch(resetUser())
        dispatch(setAccessToken(undefined))
        dispatch(setLoggedIn(false))
    } catch(e: any) {
        dispatch(setError(e.toString()))
    } finally {

    }
}

export const authActions = {
    setLoading,
    setError,
    setLoggedIn,
    loginSuccess,
    setUser,
    resetUser,
    login,
    logout,
}

export default authSlice.reducer;
