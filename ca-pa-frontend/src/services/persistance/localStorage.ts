export enum LocalStorageKey {
    UserDetails = "ca-pa-userDetails",
}

export const localStorage = {
    get: (key: LocalStorageKey) => {
        try {
            const item = window.localStorage.getItem(key)
            return item ? JSON.parse(item) : null
        } catch(e) {
            console.log(e)
        }
    },
    set: (key: LocalStorageKey, value: any) => {
        try {
            window.localStorage.setItem(key, JSON.stringify(value))
        } catch(e) {
            console.log(e)
        }
    }
}