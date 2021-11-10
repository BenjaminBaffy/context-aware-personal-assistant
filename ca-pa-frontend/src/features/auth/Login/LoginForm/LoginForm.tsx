import React from 'react'
import { useSelector } from 'react-redux'
import { Button, Col, Form, Input, Row} from 'antd'
import { RootState } from '../../../../store/store'
import ErrorDisplay from '../../../../components/ErrorDisplay/ErrorDisplay'
import useLoginForm from './useLoginForm'
import useFormRules from '../../../../hooks/useFormRules'
import { LockOutlined, UserOutlined } from '@ant-design/icons'

import styles from './LoginForm.module.scss'

interface ILoginFormProps { }

const LoginForm: React.FC<ILoginFormProps> = () => {
    const { error } = useSelector((state: RootState) => state.auth)
    const [form] = Form.useForm()
    const { handleFormFinish } = useLoginForm()

    const { requiredInput } = useFormRules()

    const initialValues = {
        username: "",
        password: ""
    }

    return (
        <>
            {error && <ErrorDisplay message={error} />}
            <Form
                form={form}
                onFinish={handleFormFinish}
                initialValues={initialValues}
            >
                <Form.Item className={styles.formItem} name="username" required rules={requiredInput}>
                    <Input
                        prefix={<UserOutlined />}
                        placeholder="Username"
                        className={styles.input}
                    />
                </Form.Item>
                <Form.Item className={styles.formItem} name="password" required rules={requiredInput}>
                    <Input
                        prefix={<LockOutlined />}
                        placeholder="Password"
                        type="password"
                        className={styles.input}
                    />
                </Form.Item>
            </Form>
            <Row justify="end">
                <Col>
                    <Button className={styles.loginButton} htmlType="submit" onClick={() => form.submit()} type="primary">LOG IN</Button>
                </Col>
            </Row>
        </>
    )
}

export default LoginForm
