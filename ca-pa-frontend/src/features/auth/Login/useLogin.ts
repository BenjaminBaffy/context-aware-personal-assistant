import { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { useLocalStorage } from '../../../hooks/useLocalStorage'
import { LocalStorageKeys } from '../../../services/persistance/localStorage'
import { RootState } from '../../../store/store'
import { setLoggedIn, setUser } from '../authSlice'

const useLogin = () => {
    const dispatch = useDispatch()
    const { user } = useSelector((state: RootState) => state.auth)
    const [loggedInUser, setLoggedInUser] = useLocalStorage(LocalStorageKeys.UserDetails, user, true)

    useEffect(() => {
        dispatch(setUser(loggedInUser))
        dispatch(setLoggedIn(loggedInUser.name !== null))
    }, [dispatch, loggedInUser]);
}

export default useLogin
