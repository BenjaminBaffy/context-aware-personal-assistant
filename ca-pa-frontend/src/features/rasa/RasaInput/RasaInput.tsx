import { SendOutlined } from '@ant-design/icons'
import { Button, Col, Input, Row } from 'antd'
import classNames from 'classnames'
import React, { useCallback, useState } from 'react'
import Microphone from '../../../components/Microphone/Microphone'
import useRasa from '../useRasa'
import styles from './RasaInput.module.scss'

interface IRasaInputProps {
    inline?: boolean;
    children?: React.ReactElement | React.ReactElement[] | string;
}

const RasaInput: React.FC<IRasaInputProps> = ({ inline, children }) => {
    const [dialogOpen, setDialogOpen] = useState<boolean>(false)
    const { submitCommand, speak, textToSend, setTextToSend } = useRasa()
    // const [inputText]

    // if not inline
    const openDialog = useCallback(() => {
        !inline && setDialogOpen(x => !x)
        speak()
    }, [inline, speak])

    const send = useCallback((e: any) => {
        e.preventDefault()
        submitCommand(textToSend)
    }, [submitCommand, textToSend])

    return (
        <div className={styles.container}>
            {/* {children} */}
            <Row align="top">
                <Col>
                    <Microphone className={styles.icon} onClick={openDialog} />
                </Col>
                <Col>
                    <div className={classNames(styles.inputpopup, { [styles.visible]: inline || dialogOpen, [styles.inline]: inline })}>
                        <Row justify="space-between">
                            <Col span={21}>
                                <Input className={styles.inputfield} onPressEnter={send} value={textToSend} onChange={(e: any) => setTextToSend(e.target.value)} />
                            </Col>
                            <Col span={2}>
                                <Button className={styles.sendIcon} onClick={send} htmlType="submit" icon={<SendOutlined />} />
                            </Col>
                        </Row>
                    </div>
                </Col>
            </Row>
        </div>
    )
}

export default RasaInput
