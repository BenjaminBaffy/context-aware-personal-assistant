import { Route, Routes as BrowserRoutes } from 'react-router-dom'
import DemoComponent from '../features/demo/DemoComponent/DemoComponent'
import HomePage from '../features/home/HomePage/HomePage'
import LoggedInLayout from '../features/layout/LoggedInLayout'
import RasaChatRoom from '../features/rasa/RasaChatRoom/RasaChatRoom'

const Routes = () => {
    const Layout = LoggedInLayout;

    return (
        <Layout>
            <BrowserRoutes>
                <Route exact path="/" element={<HomePage />} />
                <Route exact path="/demo" element={<DemoComponent />} />
                <Route exact path="/rasa" element={<RasaChatRoom />} />
            </BrowserRoutes>
        </Layout>
    )
}

export default Routes