import { Redirect, Route, Switch } from 'react-router-dom'
import DemoComponent from '../features/demo/DemoComponent/DemoComponent'

const Routes = () => {
    // TODO add Layout

    return (
        <Switch>
            <Route exact path="/">
                <h1>Hello World</h1>
            </Route>
            <Route exact path="/demo">
                <DemoComponent />
            </Route>
        </Switch>
    )
}

export default Routes