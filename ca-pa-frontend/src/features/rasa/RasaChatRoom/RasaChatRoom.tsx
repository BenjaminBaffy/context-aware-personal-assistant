import { CloseOutlined } from '@ant-design/icons'
import { Button, Col, Row, Spin, Switch } from 'antd'
import React, { useCallback, useEffect, useMemo, useRef, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import ErrorDisplay from '../../../components/ErrorDisplay/ErrorDisplay'
import { useLocalStorage } from '../../../hooks/useLocalStorage'
import Message from '../../../models/Message'
import { localStorage, LocalStorageKey } from '../../../services/persistance/localStorage'
import { RootState } from '../../../store/store'
import RasaInput from '../RasaInput/RasaInput'
import { rasaActions } from '../rasaSlice'
import useRasa from '../useRasa'
import ChatBubble from './ChatBubble/ChatBubble'
import styles from './RasaChatRoom.module.scss'

const RasaChatRoom = () => {
    const dispatch = useDispatch()
    const { user } = useSelector((state: RootState) => state.auth)
    const [conversation, setConversation] = useLocalStorage(LocalStorageKey.Conversation, [], true)
    const { response, lastCommand, loading, error, isMock, sendOnSpeechEnd } = useRasa()

    const lastMessageRef = useRef() as React.MutableRefObject<HTMLDivElement>;

    const updateConversation = useCallback((message?: Message) => {
        if (message?.content) {
            setConversation((prev: any) => [...prev, message])
        }
    }, [setConversation])

    useEffect(() => {
        lastMessageRef.current.scrollIntoView({ behavior: "smooth" })
    }, [lastMessageRef, conversation])

    useEffect(() => {
        updateConversation(response)
        return () => {
            dispatch(rasaActions.setResponse(undefined))
        }
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [response])

    useEffect(() => {
        updateConversation(lastCommand)
        return () => {
            dispatch(rasaActions.setLastCommand(undefined))
        }
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [lastCommand])

    const spinner = useMemo(() => {
        return ( loading ?
            <Row style={{ marginLeft: '1.5rem' }}>
                <Col>
                    <pre>{"Rasa is typing..."}</pre>
                </Col>
                <Col className={styles.spin}>
                    <Spin />
                </Col>
            </Row> : null
        )
    }, [loading])

    return (
        <div className={styles.container}>
            <Row justify="space-between">
                <Col>
                    <Row className={styles.toggles}>
                        <Col>
                            <Switch defaultChecked checkedChildren="real api" unCheckedChildren="mock response" onChange={() => dispatch(rasaActions.setIsMock(!isMock))} />
                        </Col>
                        <Col>
                            <Switch defaultChecked checkedChildren="send on silence" unCheckedChildren="send manually" onChange={() => dispatch(rasaActions.setSendOnSpeechEnd(!sendOnSpeechEnd))} />
                        </Col>
                    </Row>
                </Col>
                <Col>
                    <Button className={styles.clearButton} icon={<CloseOutlined />} onClick={() => {
                        localStorage.remove(LocalStorageKey.Conversation)
                        setConversation([])
                    }}>Clear</Button>
                </Col>
            </Row>
            <Row>
                <Col span={24}>
                    <div className={styles.messages}>
                        {conversation.map((message: Message, i: number) => (
                            <ChatBubble key={i} away={message.user?.name !== user.name}>{message.content}</ChatBubble>
                        ))}
                        {spinner}
                        <ErrorDisplay message={error} />
                        <div ref={lastMessageRef} />
                    </div>
                </Col>
            </Row>
            <Row justify="center">
                <Col>
                    <div className={styles.input}>
                        <RasaInput inline />
                    </div>
                </Col>
            </Row>
        </div>
    )
}

export default RasaChatRoom
