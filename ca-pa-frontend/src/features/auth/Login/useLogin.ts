import { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { useLocalStorage } from '../../../hooks/useLocalStorage'
import { LocalStorageKey } from '../../../services/persistance/localStorage'
import { RootState } from '../../../store/store'
import { authActions } from '../authSlice'

const useLogin = () => {
    const dispatch = useDispatch()
    const { user } = useSelector((state: RootState) => state.auth)
    const [loggedInUser, setLoggedInUser] = useLocalStorage(LocalStorageKey.UserDetails, user, true)

    useEffect(() => {
        dispatch(authActions.setUser(loggedInUser))
        dispatch(authActions.setLoggedIn(loggedInUser.name !== undefined))
    }, [dispatch, loggedInUser]);
}

export default useLogin
