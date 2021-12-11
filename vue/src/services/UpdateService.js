import axios from "axios"

export default{
    getUpdatesByCamperCode(camperId){
        return axios.get(`/update/camper/${camperId}`);
    },
    getUpdatesByFamilyId(familyId){
        return axios.get('/update/family', familyId);
    },
    approveAllRequests(requests){
        return axios.put('update/approval/camper', requests)
    },
    rejectRequest(requestId){
        return axios.put(`update/rejection/camper/${requestId}`)
    },
    rejectAllRequests(requests){
        return axios.put('update/rejection/camper', requests)
    }

}