import { configureStore, combineReducers, Action } from "@reduxjs/toolkit";
import { ThunkAction } from 'redux-thunk';
import demoSlice from "../features/demo/demoSlice";

// When creating new slices, no matter how nested they are, make sure to combine the reducers at places, where necessary.
// The root level is here at rootReducer.
const rootReducer = combineReducers({
    demo: demoSlice,
})

const store = configureStore({
    middleware: (getDefaultMiddleware) => getDefaultMiddleware({
        serializableCheck: false,
    }),
    reducer: rootReducer,
});

export type RootState = ReturnType<typeof rootReducer>;
export type AppDispatch = typeof store.dispatch;
export type AppThunk = ThunkAction<void, RootState, null, Action<string>>;
export default store;