import React from 'react'
import styles from './ErrorDisplay.module.scss'

interface IErrorDisplayProps {
    message: string;
}

const ErrorDisplay: React.FC<IErrorDisplayProps> = ({
    message
}) => {
    return (
        <div className={styles.error}>
            {message}
        </div>
    )
}

export default ErrorDisplay
