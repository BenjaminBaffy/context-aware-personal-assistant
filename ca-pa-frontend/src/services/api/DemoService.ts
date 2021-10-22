import ApiService from "./ApiService"

const DemoService = {
    getDemoStuff: (arg: number) => ApiService.get(`Demo/${arg}`),
    postDemoStuff: (arg: number, data: Object) => ApiService.post(`Demo/${arg}`, data),
}

export default DemoService