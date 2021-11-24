import { rasaClient } from "./ApiService"
import { BotMessageViewModel } from "./_generated/generatedBackendApi"

const RasaService = {
    send: (message: string) => rasaClient.sendMessage(new BotMessageViewModel({
        sender: "CAPA Frontend", // TODO figure out what to do with sender
        message
    }))
}

export default RasaService