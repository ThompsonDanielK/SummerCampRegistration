import axios from "axios"

export default{
    getAllUpdates(){
        return axios.get(`/update/camperUpdateList`);
    },
    getUpdatesByCamperCode(camperId){
        return axios.get(`/update/camper/${camperId}`);
    },
    getUpdatesByFamilyId(familyId){
        return axios.get('/update/family', familyId);
    },
    approveAllRequests(requestIds){
        return axios.put('update/approval/camper', requestIds)
    },
    rejectRequest(requestId){
        return axios.put(`update/rejection/camper/${requestId}`)
    },
    rejectAllRequests(requestIds){
        return axios.put('update/rejection/camper', requestIds)
    }

}