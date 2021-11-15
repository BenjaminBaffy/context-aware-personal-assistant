import React from 'react'
import { Tooltip } from 'antd'
import Icon from '@ant-design/icons'
import classnames from 'classnames'
import { ReactComponent as MicrophoneSVG } from '../../assets/micrphone.svg'
import styles from './Microphone.module.scss'

interface IMicrophoneProps {
    inline?: boolean;
    onClick?: () => void;
    filled?: boolean,
    children?: React.ReactElement | React.ReactElement[] | string;
    [x: string]: any;
}

const Microphone: React.FC<IMicrophoneProps> = ({ onClick = () => {}, filled = false, children, ...rest }) => {
    const { className } = rest

    return (
        <div className={styles.container}>
            <Tooltip title="Ask Rasa!">
                <div onClick={onClick}>
                    <Icon className={classnames(styles.microphone, filled ? styles.filled : styles.outlined, className)} component={MicrophoneSVG} />
                </div>
            </Tooltip>
        </div>
    )
}

export default Microphone
