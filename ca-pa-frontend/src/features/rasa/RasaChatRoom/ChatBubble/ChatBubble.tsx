import { Col, Row } from 'antd'
import React from 'react'

import styles from './ChatBubble.module.scss'

interface IChatBubbleProps {
    author?: string;
    away?: boolean;
    children?: React.ReactElement | React.ReactElement[] | string;
}

const ChatBubble: React.FC<IChatBubbleProps> = ({ author, away, children }) => {
    return (
        <Row justify={away ? "start" : "end"}>
            <Col span={8}>
                {/* <div>{author}</div> */}
                <div className={away ? styles.away : styles.home}>
                    {children}
                </div>
            </Col>
        </Row>
    )
}

export default ChatBubble
