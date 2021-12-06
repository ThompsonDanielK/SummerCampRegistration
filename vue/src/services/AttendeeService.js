import axios from "axios"

export default{
    getAttendee(attendeeId){
        return axios.get(`/attendee/${attendeeId}`);
    },
    getAllAttendees(){
        return axios.get('/attendee');
    },
    addAttendee(attendee){
        return axios.post('/attendee', attendee);
    },
    deleteAttendee(attendeeId){
        return axios.delete('/attendee', attendeeId);
    },
    updateAttendee(attendee){
        return axios.put(`/attendee/${attendee.id}`, attendee)
    },
    logChanges(changes){
        return axios.put('/updates', changes)
    }
}