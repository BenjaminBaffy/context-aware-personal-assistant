import React, { useCallback } from 'react'
import { Button, Layout, Menu } from 'antd'
import { Link, useLocation } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux'
import { LogoutOutlined, UserOutlined } from '@ant-design/icons'

import { RootState } from '../../store/store'
import { logout } from '../auth/authSlice'

import styles from './LoggedInLayout.module.scss'

const { Header, Content } = Layout;

interface ILayoutProps {
    children: React.ReactElement | React.ReactElement[] | string
}

const LoggedInLayout: React.FC<ILayoutProps> = ({ children }) => {
    const dispatch = useDispatch();
    const location = useLocation();
    const path = location.pathname;
    const { name } = useSelector((state: RootState) => state.auth.user)

    const logoutHandler = useCallback(() => {
        dispatch(logout())
    }, [dispatch])

    return (
        <Layout className={styles.layout}>
            <Header className={styles.header}>
                <Menu mode="horizontal" selectedKeys={[path]} className={styles.menu}>
                    <Menu.Item key="/">
                        <Link to="/">Home</Link>
                    </Menu.Item>
                    <Menu.Item key="/demo">
                        <Link to="/demo">Demo</Link>
                    </Menu.Item>
                </Menu>
                <div className={styles.rightMenu}>
                    <div>
                        <Button icon={<UserOutlined />} type="text">
                        {name}
                        </Button>
                    </div>
                    <div>
                        <Button icon={<LogoutOutlined />} type="text" onClick={logoutHandler}>
                            Log out
                        </Button>
                    </div>
                </div>
            </Header>
            <Content className={styles.content}>{children}</Content>
        </Layout>
    )
}

export default LoggedInLayout
