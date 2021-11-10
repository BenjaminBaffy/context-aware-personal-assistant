import { Col, Row, Tooltip } from 'antd'
import Icon from '@ant-design/icons'
import { ReactComponent as MicrophoneSVG } from '../../../assets/micrphone.svg'
import LoginForm from './LoginForm/LoginForm'
import useLogin from './useLogin'

import styles from './Login.module.scss'

// How to Persist a Logged-in User in React
// https://www.freecodecamp.org/news/how-to-persist-a-logged-in-user-in-react/

const Login = () => {
    useLogin()

    return (
        <Row style={{ height: '100vh' }} justify="center" className={styles.animatedBackground}>
            <Col>
                <Row style={{ height: '100%' }} align="middle" justify="center">
                    <Col>
                    <div className={styles.appTitle}>{process.env.REACT_APP_PROJECT_TITLE + "®"}</div>
                        <div className={styles.container}>

                            <Row justify="center">
                                <Col span={20}>

                                    <Row justify="space-between" align="middle" className={styles.titleBar}>
                                        <Col>
                                            <h1 className={styles.title}>Sign in</h1>
                                        </Col>
                                        <Col>
                                            <Tooltip title="Help from RASA">
                                                <div onClick={() => console.log('Activate RASA')}>
                                                    <Icon className={styles.microphone} component={MicrophoneSVG} />
                                                </div>
                                            </Tooltip>
                                        </Col>
                                    </Row>

                                </Col>
                            </Row>

                            <Row justify="center">
                                <Col span={20}>
                                    <LoginForm />
                                </Col>
                            </Row>

                        </div>
                    </Col>
                </Row>
            </Col>
        </Row>
    )
}

export default Login
