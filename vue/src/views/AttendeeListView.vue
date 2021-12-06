<template>
  <section>
    <h1>Attendees</h1>
    <button type="button" v-on:click="showAddForm = true" v-show="!showAddForm">Add New Attendee</button>
    <form class="form-group" v-show="showAddForm">
        <label for="firstName">First Name:</label>
        <input v-model="newAttendee.firstName" name="firstName" type='text' />
        <label for="lastName">Last Name:</label>
        <input v-model="newAttendee.lastName" name="lastName" type='text' />
        <label for="city">City:</label>
        <input v-model="newAttendee.city" name="city" type='text' />
        <label for="state">State:</label>
        <select v-model="newAttendee.state" name="state">
          <option v-for="state in states" v-bind:key="state">
            {{ state }}
          </option>
        </select>
        <label for="zip">Zip Code:</label>
        <input v-model="newAttendee.zip" name="zip" type='text' />
        <button type="submit" v-on:click.prevent="saveNewAttendee()" v-bind:disabled="!filledForm">
            Submit</button>
        <button type="button" v-on:click="showAddForm = false; newAttendee = {}">Cancel</button>
    </form>
    <table>
      <thead>
        <tr>
          <td>Search:</td>
          <td><input type="text" v-model="firstNameToFilter" placeholder="First Name"></td>
          <td><input type="text" v-model="lastNameToFilter" placeholder="Last Name"></td>
          <td><input type="text" v-model="cityToFilter" placeholder="City"></td>
          <td><input type="text" v-model="stateToFilter" placeholder="State"></td>
          <td><input type="text" v-model="zipToFilter" placeholder="Zip Code"></td>
        </tr>
      </thead>
        <tr>
          <td>Id</td>
          <td>First Name:</td>
          <td>Last Name:</td>
          <td>City:</td>
          <td>State:</td>
          <td>Zip Code:</td>
          <td></td>
        </tr>
      <tr
        v-for="attendee in this.filteredAttendees"
        v-bind:key="attendee.id"
        v-bind:showDetails="false"
      >
        <td>{{ attendee.id }}</td>
        <td>{{ attendee.firstName }}</td> 
        <td>{{ attendee.lastName }}</td>
        <td>{{ attendee.city }}</td>
        <td>{{ attendee.state }}</td>
        <td>{{ attendee.zip }}</td>
        <td><router-link v-bind:to="{name:'attendee', params: {attendeeId: attendee.id}}"><button type="button">Edit</button></router-link></td>
        <td><button type="button" v-on:click="deleteAttendee(attendee.id)">Delete</button></td>
     </tr>
    </table>
  </section>
</template>

<script>
import AttendeeService from '../services/AttendeeService.js';

export default {
  data() {
    return {
      newAttendee: {
        firstName: '',
        lastName: '',
        city: '',
        state: '',
        zip: '',
        },
      showAddForm: false,
      attendees: [],
      firstNameToFilter: '',
      lastNameToFilter: '',
      cityToFilter: '',
      stateToFilter: '',
      zipToFilter: '',
      states: ["AL", "AK", "AR", "AZ", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA",
               "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH",
               "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VA",
               "VT", "WA", "WI", "WV", "WY"
              ],
    };
  },
  created() {
    AttendeeService.getAllAttendees()
    .then(response =>{
        console.log('Got all attendees', response.data);
        this.attendees = response.data;
    })
    .catch(response => {
        console.error('Problem getting all attendees', response)
    })
  },
  computed: {
      filledForm(){
          return this.newAttendee.firstName && this.newAttendee.lastName && this.newAttendee.city && this.newAttendee.state && this.newAttendee.zip;
      },
    filteredAttendees() {
        let attendeesList = this.attendees;
        if(this.firstNameToFilter)
        {
            attendeesList = attendeesList.filter(a => a.firstName.toLowerCase().includes(this.firstNameToFilter.toLowerCase()))
        }
        if(this.lastNameToFilter)
        {
            attendeesList = attendeesList.filter(a => a.lastName.toLowerCase().includes(this.lastNameToFilter.toLowerCase()))
        }
        if(this.cityToFilter)
        {
            attendeesList = attendeesList.filter(a => a.city.toLowerCase().includes(this.cityToFilter.toLowerCase()))
        }
        if(this.stateToFilter)
        {
            attendeesList = attendeesList.filter(a => a.state.toLowerCase().includes(this.stateToFilter.toLowerCase()))
        }
        if(this.zipToFilter)
        {
            attendeesList = attendeesList.filter(a => a.zipCode.toLowerCase().includes(this.zipToFilter.toLowerCase()))
        }        
    return attendeesList;
    }
  },
  methods: {
      saveNewAttendee(){
          this.$store.commit('ADD_ATTENDEE', this.newAttendee);
      },
      deleteAttendee(attendeeId){
          this.$store.commit('DELETE_ATTENDEE', attendeeId);
      },
  }
};
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

section{
    background-color: $textLight;
    padding: 10px;
    border-radius: 20px;
    border: 2px solid $textDark;
}
table{
    width: 100%;
    padding: 10px;
}
input{
    width: 80%;
}
.form-group{
    display:flex;
    flex-direction: column;
    width: 30%;
    margin: 10px;
    background-color: $secondary;
    padding: 10px;
    border-radius: 10px;
}
input, .form-group input, select{
    margin: 5px;
    background-color: $textDark;
    color: $textLight;
    border-radius: 10px;
    padding-left: 10px;
    border: 1px solid $highlight;
}
button{
    background-color: $textDark;
    color: $textLight;
    border-radius: 10px;
    border: 1px solid $highlight;
}
button:disabled{
    background-color: $secondary;
}
</style>