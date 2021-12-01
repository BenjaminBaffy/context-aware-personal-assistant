import React, { useCallback } from 'react'
import { Button, Layout, Menu } from 'antd'
import { Link, useLocation } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux'
import { HomeFilled, HomeOutlined, LogoutOutlined, UserOutlined } from '@ant-design/icons'
import Icon from '@ant-design/icons'

import { RootState } from '../../store/store'
import { authActions } from '../auth/authSlice'

import styles from './LoggedInLayout.module.scss'
import useNav from '../nav/useNav'

const { Header, Content } = Layout;

interface ILayoutProps {
    children: React.ReactElement | React.ReactElement[] | string
}

const LoggedInLayout: React.FC<ILayoutProps> = ({ children }) => {
    const dispatch = useDispatch();
    const location = useLocation();
    const path = location.pathname;
    const { name } = useSelector((state: RootState) => state.auth.user)

    const { tabs } = useNav()

    const logoutHandler = useCallback(() => {
        dispatch(authActions.logout())
    }, [dispatch])

    return (
        <Layout className={styles.layout}>
            <Header className={styles.header}>
                <Menu mode="horizontal" selectedKeys={[path]} className={styles.menu}>
                    {tabs.map(tab => (
                        <Menu.Item key={tab.linkTo}>
                            <Link to={tab.linkTo} className={styles.tab}>
                                {tab.title()}
                            </Link>
                        </Menu.Item>
                    ))}
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
