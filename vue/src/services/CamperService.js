import axios from "axios"

export default{
    getCamper(camperId){
        return axios.get(`/camper/${camperId}`)
    }
}