import { Col, Row } from 'antd'
import React from 'react'
import classNames from 'classnames'
import styles from './ChatBubble.module.scss'

interface IChatBubbleProps {
    author?: string;
    away?: boolean;
    children?: React.ReactElement | React.ReactElement[] | string;
}

const ChatBubble: React.FC<IChatBubbleProps> = ({ author, away, children }) => {
    return (
        <Row justify={away ? "start" : "end"}>
            <Col span={24} className={styles.container}>
                {/* <div>{author}</div> */}
                <div className={classNames(away ? styles.away : styles.home, styles.message)}>
                    {children}
                </div>
            </Col>
        </Row>
    )
}

export default ChatBubble
