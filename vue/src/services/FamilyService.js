import axios from "axios"

export default{
    getAllFamilies(){
        return axios.get('/camp/family');
    },
    addNewFamily(family){
        return axios.post('/camp/family', family);
    },
    updateFamily(family){
        return axios.put(`/camp/family/${family.Id}`, family);
    },
    deleteFamily(familyId){
        return axios.delete(`/camp/family/${familyId}`);
    }
}