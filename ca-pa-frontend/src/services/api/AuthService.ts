import { authenticationClient } from "./ApiService"
import { PasswordLoginViewModel } from "./_generated/generatedBackendApi"

const AuthService = {
    login: (username: string, password: string) => authenticationClient.login(new PasswordLoginViewModel({
        loginName: username,
        password: password
    })),
    logout: () => authenticationClient.logout()
}

export default AuthService
