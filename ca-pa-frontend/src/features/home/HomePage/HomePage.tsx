import React from 'react'
import { useSelector } from 'react-redux'
import { RootState } from '../../../store/store'

const HomePage = () => {
    const { name } = useSelector((state: RootState) => state.auth.user)

    return (
        <div>
            <h1>Hello {name}</h1>
        </div>
    )
}

export default HomePage
