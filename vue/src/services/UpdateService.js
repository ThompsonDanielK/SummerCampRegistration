import axios from "axios"

export default{
    logChanges(changes){
        return axios.put('/updates', changes)
    },
    updateChanges(changes){
        return axios.put('/updates', changes)
    }
}