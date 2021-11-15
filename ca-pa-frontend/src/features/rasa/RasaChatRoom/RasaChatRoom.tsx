import { CloseOutlined } from '@ant-design/icons'
import { Button, Col, Row, Spin, Switch } from 'antd'
import React, { useCallback, useEffect, useRef } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import ErrorDisplay from '../../../components/ErrorDisplay/ErrorDisplay'
import { useLocalStorage } from '../../../hooks/useLocalStorage'
import Message from '../../../models/Message'
import { LocalStorageKey } from '../../../services/persistance/localStorage'
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
    const { response, lastCommand, loading, error, isMock } = useRasa()

    const lastMessageRef = useRef() as React.MutableRefObject<HTMLDivElement>;

    const updateConversation = useCallback((message: Message) => {
        if (message.content) {
            setConversation([...conversation, message])
        }
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [conversation])

    useEffect(() => {
        lastMessageRef.current.scrollIntoView({ behavior: "smooth" })
    }, [lastMessageRef, conversation])

    useEffect(() => {
        updateConversation(response)
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [response])

    useEffect(() => {
        updateConversation(lastCommand)
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [lastCommand])

    return (
        <div className={styles.container}>
            <Row justify="space-between">
                <Col>
                    <Switch checkedChildren="real" unCheckedChildren="mock" onChange={() => dispatch(rasaActions.setIsMock(!isMock))} />
                </Col>
                <Col>
                    <Button className={styles.clearButton} icon={<CloseOutlined />} onClick={() => setConversation([])} >Clear</Button>
                </Col>
            </Row>
            <Row>
                <Col span={24}>
                    <div className={styles.messages}>
                        {conversation.map((message: Message, i: number) => (
                            <ChatBubble key={i} away={message.user?.name !== user.name}>{message.content}</ChatBubble>
                        ))}
                        { loading &&
                            <Row style={{ marginLeft: '1.5rem' }}>
                                <Col span={4}>
                                    <pre>{"Rasa is typing..."}</pre>
                                </Col>
                                <Col>
                                    <Spin />
                                </Col>
                            </Row>
                        }
                        <ErrorDisplay message={error} />
                        <div ref={lastMessageRef} />
                    </div>
                </Col>
            </Row>
            <Row className={styles.input} justify="center">
                <Col>
                    <RasaInput inline />
                </Col>
            </Row>
        </div>
    )
}

export default RasaChatRoom
