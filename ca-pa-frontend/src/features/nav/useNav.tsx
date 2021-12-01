import React, { useMemo } from 'react'
import { HomeFilled, WechatOutlined } from '@ant-design/icons'

const useNav = () => {
    const tabs = useMemo(() => [
        {
            linkTo: '/',
            title: () => (
                <>
                    <HomeFilled />
                    Home
                </>
            )
        },
        {
            linkTo: '/rasa',
            title: () => (
                <>
                    <WechatOutlined />
                    Chat with Rasa
                </>
            )
        },
    ], [])

    return {
        tabs
    }
}

export default useNav
