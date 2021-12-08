import axios from "axios"

export default{
    getAllFamilies(){
        return axios.get('/camp/familyList');
    },
    getFamily(familyId){
        return axios.get(`/camp/family/${familyId}`);
    },
    addNewFamily(family){
        return axios.post('/camp/family', family);
    },
    updateFamily(family, camperCode){
        return axios.put(`/camp/family/${camperCode}`, family);
    },
    deleteFamily(familyId){
        return axios.delete(`/camp/family/${familyId}`);
    }
}