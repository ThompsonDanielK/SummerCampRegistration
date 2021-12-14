import axios from "axios"

export default {
    sendEmail(email) {
        return axios.post('email', email)
    }
}