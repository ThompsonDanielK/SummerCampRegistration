import axios from "axios"

export default {
    getAllUpdates() {
        return axios.get(`/update/camperUpdateList`);
    },
    getAllFamilyUpdates() {
        return axios.get(`/update/familyUpdateList`);
    },
    getUpdatesByCamperCode(camperId) {
        return axios.get(`/update/camper/${camperId}`);
    },
    getUpdatesByFamilyId(familyId) {
        return axios.get(`/update/family/${familyId}`);
    },
    approveAllRequests(requestIds) {
        return axios.put('update/approval/camper', requestIds)
    },
    rejectRequest(requestId) {
        return axios.put(`update/rejection/camper/${requestId}`)
    },
    rejectAllRequests(requestIds) {
        return axios.put('update/rejection/camper', requestIds)
    },
    approveAllFamilyRequests(requestIds) {
        return axios.put('update/approval/family', requestIds)
    },
    rejectFamilyRequest(requestId) {
        return axios.put(`update/rejection/family/${requestId}`)
    },
    rejectAllFamilyRequests(requestIds) {
        return axios.put('update/rejection/family', requestIds)
    }
}