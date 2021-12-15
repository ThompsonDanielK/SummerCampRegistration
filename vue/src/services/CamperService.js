import axios from "axios"

export default {
    logChanges(update) {
        return axios.post('update/camper', update)
    },
    logNote(update) {
        return axios.post('update/notes', update)
    },
    getAllCampers() {
        return axios.get('/Camp/CamperList');
    },
    addCamper(camper) {
        return axios.post('/Camp/Camper', camper);
    },
    deleteCamper(camperCode) {
        return axios.delete('/Camp', camperCode);
    },
    updateCamper(camper) {
        return axios.post('/update/camper', camper)
    },
    addNote(note){
        return axios.post('/camp/note', note)
    }

}