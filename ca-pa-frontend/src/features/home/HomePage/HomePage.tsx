import React from 'react'
import { useSelector } from 'react-redux'
import { RootState } from '../../../store/store'

import styles from './HomePage.module.scss'

const HomePage = () => {
    const { name } = useSelector((state: RootState) => state.auth.user)

    return (
        <div className={styles.container}>
            <div className={styles.content}>
                <div className={styles.greet}>
                    Welcome, <span className={styles.name}>{name}</span>!
                </div>
                <div className={styles.description}>
                    <p>
                        This <strong>Context-Aware Personal Assistant</strong> helps you with daily tasks, that require some kind of context about the user, such as
                    </p>
                    <ul>
                        <li>space - <span><i>"What's the weather like <strong>here</strong>?"</i></span></li>
                        <li>time - <span><i>"What's on my <strong>current</strong> grocery list?"</i></span></li>
                        <li>etc...</li>
                    </ul>
                    <p>
                        Visit the <i><strong>Chat with Rasa</strong></i> tab, and talk to the assistant yourself.
                    </p>
                </div>
            </div>
            <div className={styles.footer}>
                This application is made possible by the help of five Computer Science students. 2021, ELTE CS MSc.
            </div>
        </div>
    )
}

export default HomePage
