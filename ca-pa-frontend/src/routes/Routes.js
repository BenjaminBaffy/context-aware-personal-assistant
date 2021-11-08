import { Route, Switch } from 'react-router-dom'
import DemoComponent from '../features/demo/DemoComponent/DemoComponent'
import HomePage from '../features/home/HomePage/HomePage'
import LoggedInLayout from '../features/layout/LoggedInLayout'

const Routes = () => {
    const Layout = LoggedInLayout;

    return (
        <Layout>
            <Switch>
                <Route exact path="/">
                    <HomePage />
                </Route>
                <Route exact path="/demo">
                    <DemoComponent />
                </Route>
            </Switch>
        </Layout>
    )
}

export default Routes