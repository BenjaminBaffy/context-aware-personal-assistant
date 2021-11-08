import { Route, Routes as BrowserRoutes } from 'react-router-dom'
import DemoComponent from '../features/demo/DemoComponent/DemoComponent'
import HomePage from '../features/home/HomePage/HomePage'
import LoggedInLayout from '../features/layout/LoggedInLayout'

const Routes = () => {
    const Layout = LoggedInLayout;

    return (
        <Layout>
            <BrowserRoutes>
                <Route exact path="/" element={<HomePage />} />
                <Route exact path="/demo" element={<DemoComponent />} />
            </BrowserRoutes>
        </Layout>
    )
}

export default Routes