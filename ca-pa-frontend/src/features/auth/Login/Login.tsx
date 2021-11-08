import { Col, Row } from 'antd'
import Icon from '@ant-design/icons'
import { ReactComponent as MicrophoneSVG } from '../../../assets/micrphone.svg'
import LoginForm from './LoginForm/LoginForm'
import useLogin from './useLogin'

// TODO add Form rules
// TODO add LoginForm

// How to Persist a Logged-in User in React
// https://www.freecodecamp.org/news/how-to-persist-a-logged-in-user-in-react/

const Login = () => {
    useLogin()

    return (
        <>
            <Row justify="center">
                <Col>
                    <div style={{ width: '10rem', margin: '0 auto' }}>
                        <Row justify="space-between" align="middle">
                            <Col>
                                <h1 style={{ fontSize: '32px', marginBottom: '1rem' }}>Login</h1>
                            </Col>
                            <Col>
                                <Icon component={MicrophoneSVG} style={{ fontSize: '32px', marginBottom: '1rem' }} />
                            </Col>
                        </Row>
                    </div>
                </Col>
            </Row>
            <Row justify="center">
                <Col>
                    <LoginForm />
                </Col>
            </Row>
        </>
    )
}

export default Login
