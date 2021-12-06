import axios from "axios"

export default{
    getCamper(camperId){
        return axios.get(`/Camp/${camperId}`);
    },
    getAllCampers(){
        return axios.get('/CamperList');
    },
    addCamper(camper){
        return axios.post('/Camp', camper);
    },
    deleteCamper(camperId){
        return axios.delete('/Camp', camperId);
    },
    updateCamper(camper){
        return axios.put(`/Camp/${camper.id}`, camper)
    },
    logChanges(changes){
        return axios.put('/updates', changes)
    }
}