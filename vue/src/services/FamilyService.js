import axios from "axios"

export default {
    getAllFamilies() {
        return axios.get('/camp/familyList');
    },
    updateFamily(family, username) {
        return axios.post(`/update/family/${username}`, family);
    },
    addFamily(family) {
        return axios.post('/Camp/family', family);
    },
    deleteFamily(familyId) {
        return axios.delete(`/camp/family/${familyId}`);
    },
    logChanges(update) {
        return axios.post('update/family', update)
    },
}