import { rasaClient } from "./ApiService"
import { BotMessageViewModel } from "./_generated/generatedBackendApi"

const RasaService = {
    send: ({ sender, message }: {sender: string, message: string}) => rasaClient.sendMessage(new BotMessageViewModel({
        sender,
        message
    }))
}

export default RasaService