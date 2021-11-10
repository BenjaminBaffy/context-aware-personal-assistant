import { Col, Row } from 'antd'
import Icon from '@ant-design/icons'
import { ReactComponent as MicrophoneSVG } from '../../../assets/micrphone.svg'
import LoginForm from './LoginForm/LoginForm'
import useLogin from './useLogin'

import styles from './Login.module.scss'

// TODO add Form rules
// TODO add LoginForm

// How to Persist a Logged-in User in React
// https://www.freecodecamp.org/news/how-to-persist-a-logged-in-user-in-react/

const Login = () => {
    useLogin()

    return (
        <Row style={{ height: '100vh' }} justify="center" className={styles.animatedBackground}>
            <Col>
                <Row style={{ height: '100%' }} align="middle" justify="center">
                    <Col>
                        <div className={styles.container}>
                            <Row justify="center">
                                <Col span={20}>
                                    <Row justify="space-between" align="middle">
                                        <Col>
                                            <h1 className={styles.title}>Login</h1>
                                        </Col>
                                        <Col>
                                            <Icon component={MicrophoneSVG} style={{ fontSize: '32px', marginBottom: '1rem' }} />
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
