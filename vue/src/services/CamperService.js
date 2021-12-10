import axios from "axios"

export default{
    getCamper(camperCode){
        return axios.get(`/Camp/Camper/${camperCode}`);
    },
    getAllCampers(){
        return axios.get('/Camp/CamperList');
    },
    addCamperFamily(CamperFamily){
        return axios.post('/Camp/Camper', CamperFamily);
    },
    deleteCamper(camperCode){
        return axios.delete('/Camp', camperCode);
    },
    updateCamper(camper){
        return axios.post('/update/camper', camper)
    }
}