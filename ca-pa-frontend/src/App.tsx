import { useSelector } from 'react-redux'
import Login from './features/auth/Login/Login'
import Routes from './routes/Routes'
import { RootState } from './store/store'

function App() {
    const { loggedIn } = useSelector((store: RootState) => store.auth)
    return loggedIn ? <Routes /> : <Login />
}

export default App;
