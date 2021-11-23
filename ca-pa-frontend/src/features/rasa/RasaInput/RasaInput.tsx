import React, { useCallback, useState } from 'react'
import { SendOutlined } from '@ant-design/icons'
import { Button, Col, Input, Row } from 'antd'
import classNames from 'classnames'

import Microphone from '../../../components/Microphone/Microphone'
import useRasa from '../useRasa'
import styles from './RasaInput.module.scss'
import { spawn } from 'child_process'

interface IRasaInputProps {
    icon?: React.ReactElement;
    inline?: boolean;
    children?: React.ReactElement | React.ReactElement[] | string;
    [x: string]: any;
}

const RasaInput: React.FC<IRasaInputProps> = ({ icon, inline, children, ...rest }) => {
    const [dialogOpen, setDialogOpen] = useState<boolean>(false)
    const [key, setKey] = useState<number>(Math.random())
    const { submitCommand, speak, endSpeak, textToSend, setTextToSend, listening } = useRasa()

    const handleMicClick = useCallback(() => {
        if (!inline) {
            setDialogOpen(x => !x)
            setKey(Math.random())
        }
        !listening ? speak() : endSpeak()
    }, [inline, speak, endSpeak, listening])

    const send = useCallback((e: any) => {
        e.preventDefault()
        submitCommand(textToSend)
        if (!inline) {
            setDialogOpen(false)
        }
    }, [submitCommand, textToSend, inline])

    return (
        <div className={styles.container} {...rest}>
            {/* {children} */}
            <Row align="top" justify="center">
                <Col>
                    <div className={styles.iconContainer}>
                        <div className={styles.icon} onClick={handleMicClick}>
                            {icon || <Microphone className={classNames(styles.icon, { [styles.listening]: listening })} />}
                        </div>
                    </div>
                </Col>
                <Col>
                    <div key={key} className={classNames(styles.inputContainer, { [styles.visible]: inline || dialogOpen, [styles.inline]: inline, [styles.hover]: !inline })}>
                        <Row justify="space-between">
                            <Col span={20}>
                                <Input className={styles.inputfield} onPressEnter={send} value={textToSend} onChange={(e: any) => setTextToSend(e.target.value)} />
                            </Col>
                            <Col span={4}>
                                <Row justify="end">
                                    <Col>
                                        <Button className={styles.sendIcon} onClick={send} htmlType="submit" icon={<SendOutlined />} />
                                    </Col>
                                </Row>
                            </Col>
                        </Row>
                    </div>
                </Col>
            </Row>
            {listening && <span>Speak slowly!</span>}
        </div>
    )
}

export default RasaInput
