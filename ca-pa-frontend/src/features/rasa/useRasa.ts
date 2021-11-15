import { useCallback, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { RootState } from '../../store/store'
import { rasaActions } from './rasaSlice'

const useRasa = () => {
    const dispatch = useDispatch()
    const { user } = useSelector((state: RootState) => state.auth)
    const { response, lastCommand, loading, error, isMock } = useSelector((state: RootState) => state.rasa)
    const [textToSend, setTextToSend] = useState<string>('')

    const speak = useCallback(() => {
        // turn on audio input from user
        // capture audio
        // convert speech-to-text
        // save text to state
    }, [])

    const submitCommand = useCallback((message: string) => {
        if (textToSend === '' || loading) return
        dispatch(rasaActions.setLastCommand({ user, content: message, uuid: `${Math.random()}` }))
        dispatch(rasaActions.send(message))

        setTextToSend('')
    }, [dispatch, textToSend, loading, user])

    return {
        speak,
        setTextToSend,
        submitCommand,
        textToSend,
        response,
        lastCommand,
        loading,
        error,
        isMock,
    }
}

export default useRasa
