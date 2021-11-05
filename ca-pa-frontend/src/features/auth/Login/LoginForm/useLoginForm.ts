import { useCallback } from "react"
import { useDispatch } from "react-redux"
import { authActions } from "../../authSlice"

const useLoginForm = () => {
    const dispatch = useDispatch()

    const handleFormFinish = useCallback((values: any) => {
        const { username, password } = values

        const credentials = {
            username,
            password: `${password}`
        }

        dispatch(authActions.login(credentials))
    }, [dispatch])

    return {
        handleFormFinish,
    }
}

export default useLoginForm
