import axios from "axios"

export default{
    getAllFamilies(){
        return axios.get('/camp/familyList');
    },
    getFamily(familyId){
        return axios.get(`/camp/family/${familyId}`);
    },
    updateFamily(family, camperCode){
        return axios.put(`/camp/family/${camperCode}`, family);
    },
    deleteFamily(familyId){
        return axios.delete(`/camp/family/${familyId}`);
    }
}