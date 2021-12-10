import axios from "axios"

export default{
    getUpdatesByCamperCode(camperId){
        return axios.get(`/update/camper/${camperId}`);
    },
    getUpdatesByFamilyId(familyId){
        return axios.get('/update/family', familyId);
    },
    approveRequest(requestId){
        return axios.put(`approval/camper/${requestId}`)
    },
}