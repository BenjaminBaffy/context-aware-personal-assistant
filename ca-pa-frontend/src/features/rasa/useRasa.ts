import { useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import SpeechRecognition, {
    useSpeechRecognition,
} from "react-speech-recognition";
import { RootState } from "../../store/store";
import { rasaActions } from "./rasaSlice";

const useRasa = () => {
    const dispatch = useDispatch();
    const { user } = useSelector((state: RootState) => state.auth);
    const { response, lastCommand, sendOnSpeechEnd, loading, error, isMock } = useSelector(
        (state: RootState) => state.rasa
    );
    const { transcript, listening, resetTranscript } = useSpeechRecognition();
    const [textToSend, setTextToSend] = useState<string>("");

    const speak = useCallback(() => {
        // turn on audio input from user
        SpeechRecognition.startListening();
        // capture audio
        // convert speech-to-text
        // save text to state
    }, []);

    const endSpeak = useCallback(() => {
        // turn on audio input from user
        SpeechRecognition.stopListening();
        // capture audio
        // convert speech-to-text
        // save text to state
    }, []);

    const submitCommand = useCallback(
        (message?: string) => {
            const msg = message || textToSend
            if (msg === "" || loading) return;
            dispatch(
                rasaActions.setLastCommand({
                    user,
                    content: msg,
                    uuid: `${Math.random()}`,
                })
            );
            dispatch(rasaActions.send(msg))
            setTextToSend("")
        },
        [dispatch, textToSend, loading, user]
    );

    useEffect(() => {
        console.log(`transcript: ${transcript}`)
        setTextToSend(transcript)
    }, [transcript])

    return {
        speak,
        endSpeak,
        setTextToSend,
        submitCommand,
        resetTranscript,
        textToSend,
        transcript,
        listening,
        sendOnSpeechEnd,
        response,
        lastCommand,
        loading,
        error,
        isMock,
    };
};

export default useRasa;
