import axios from "axios"

export default{
    getAllFamilies(){
        return axios.get('/camp/familyList');
    },
    updateFamily(family, username){
        return axios.post(`/update/family/${username}`, family);
    },
    deleteFamily(familyId){
        return axios.delete(`/camp/family/${familyId}`);
    }
}