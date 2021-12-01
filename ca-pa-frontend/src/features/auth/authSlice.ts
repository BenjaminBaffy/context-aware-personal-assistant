import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import User from "../../models/User";
import {
    localStorage,
    LocalStorageKey,
} from "../../services/persistance/localStorage";
import AuthService from '../../services/api/AuthService';
import { AppThunk } from '../../store/store'
import { setAccessToken } from "../../services/api/ApiService"

interface AuthState {
    user: User;
    loggedIn: boolean;
    loading: boolean;
    error?: string;
}

const initialState: AuthState = {
    user: {
        name: undefined,
        userId: undefined,
        roles: [],
    },

    loggedIn: false,
    loading: false,
    error: undefined,
};

export const authSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        setLoading: (state, action: PayloadAction<boolean>) => {
            state.loading = action.payload;
        },
        setError: (state, action: PayloadAction<string>) => {
            state.error = action.payload;
        },
        loginSuccess: state => {
            state.loggedIn = true;
            state.error = undefined;
        },
        setLoggedIn: (state, action: PayloadAction<boolean>) => {
            state.loggedIn = action.payload;
        },
        setUser: (state, action) => {
            state.user = action.payload;
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
    resetUser,
} = authSlice.actions;

const login = (credentials: { username: string; password: string }): AppThunk =>
    async (dispatch, getState) => {
        const { username, password } = credentials;

    try {
        dispatch(setLoading(true))

        const response = await AuthService.login(username, password)

        const user = {
            name: response.fullName,
            userId: (response as any).userId || '',
            roles: (response as any).roles || []
        }

        dispatch(setUser(user))
        localStorage.set(LocalStorageKey.UserDetails, user)
        localStorage.set(LocalStorageKey.AccessToken, response.accessToken)
        setAccessToken(response.accessToken!)

        dispatch(loginSuccess());
    } catch (e: any) {
        dispatch(setError(e.toString()));
    } finally {
        dispatch(setLoading(false));
    }
};

const logout = (): AppThunk => async (dispatch, getState) => {
    try {
        // addtional api calls
        // ...

        // clear localStorage
        localStorage.remove(LocalStorageKey.UserDetails)
        localStorage.remove(LocalStorageKey.Conversation)
        localStorage.remove(LocalStorageKey.AccessToken)

        dispatch(resetUser())
        dispatch(setLoggedIn(false))
    } catch(e: any) {
        dispatch(setError(e.toString()))
    } finally {
    }
};

export const authActions = {
    setLoading,
    setError,
    setLoggedIn,
    loginSuccess,
    setUser,
    resetUser,
    login,
    logout,
};

export default authSlice.reducer;
