import style from './LoggedInLayout.module.scss'
import { Button, Layout, Menu } from 'antd'
import React, { useCallback } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { LogoutOutlined, UserOutlined } from '@ant-design/icons'
import { RootState } from '../../store/store'
import { logout } from '../auth/authSlice'

const { Header, Content } = Layout;

interface ILayoutProps {
    children: React.ReactElement | React.ReactElement[] | string
}

const LoggedInLayout: React.FC<ILayoutProps> = ({ children }) => {
    const dispatch = useDispatch();
    const { name } = useSelector((state: RootState) => state.auth.user)

    const logoutHandler = useCallback(() => {
        dispatch(logout())
    }, [dispatch])

    return (
        <Layout className={style.layout}>
            <Header className={style.header}>
                <div className={style.rightMenu}>
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
            <Content className={style.content}>{children}</Content>
        </Layout>
    )
}

export default LoggedInLayout
