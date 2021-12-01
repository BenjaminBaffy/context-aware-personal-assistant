import User from "./User"

export default interface Message {
    content: string;
    uuid?: string
    user?: User;
    date?: string;
}