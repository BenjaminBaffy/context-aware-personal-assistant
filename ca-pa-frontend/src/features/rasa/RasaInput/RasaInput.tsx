import { SendOutlined } from '@ant-design/icons'
import { Button, Col, Input, Row } from 'antd'
import classNames from 'classnames'
import React, { useCallback, useState } from 'react'
import Microphone from '../../../components/Microphone/Microphone'
import useRasa from '../useRasa'
import styles from './RasaInput.module.scss'

interface IRasaInputProps {
    icon?: React.ReactElement;
    inline?: boolean;
    children?: React.ReactElement | React.ReactElement[] | string;
    [x: string]: any;
}

const RasaInput: React.FC<IRasaInputProps> = ({ icon, inline, children, ...rest }) => {
    const [dialogOpen, setDialogOpen] = useState<boolean>(false)
    const [key, setKey] = useState<number>(Math.random())
    const { submitCommand, speak, textToSend, setTextToSend } = useRasa()
    // const [inputText]

    // if not inline
    const openDialog = useCallback(() => {
        if (!inline) {
            setDialogOpen(x => !x)
            setKey(Math.random())
        }
        speak()
    }, [inline, speak])

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
                        <div className={styles.icon} onClick={openDialog}>
                            {icon || <Microphone className={styles.icon} />}
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
        </div>
    )
}

export default RasaInput
