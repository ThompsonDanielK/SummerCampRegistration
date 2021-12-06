<template>
  <section>
    <h1>Campers</h1>
    <button type="button" v-on:click="showAddForm = true" v-show="!showAddForm">Add New Camper</button>
    <form class="form-group" v-show="showAddForm">
        <label for="firstName">First Name:</label>
        <input v-model="newCamper.firstName" name="firstName" type='text' />
        <label for="lastName">Last Name:</label>
        <input v-model="newCamper.lastName" name="lastName" type='text' />
        <label for="city">City:</label>
        <input v-model="newCamper.city" name="city" type='text' />
        <label for="state">State:</label>
        <select v-model="newCamper.state" name="state">
          <option v-for="state in states" v-bind:key="state">
            {{ state }}
          </option>
        </select>
        <label for="zip">Zip Code:</label>
        <input v-model="newCamper.zip" name="zip" type='text' />
        <button type="submit" v-on:click.prevent="saveNewCamper()" v-bind:disabled="!filledForm">
            Submit</button>
        <button type="button" v-on:click="showAddForm = false; newcamper = {}">Cancel</button>
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
        v-for="camper in this.filteredCampers"
        v-bind:key="camper.id"
        v-bind:showDetails="false"
      >
        <td>{{ camper.id }}</td>
        <td>{{ camper.firstName }}</td> 
        <td>{{ camper.lastName }}</td>
        <td>{{ camper.city }}</td>
        <td>{{ camper.state }}</td>
        <td>{{ camper.zip }}</td>
        <td><router-link v-bind:to="{name:'camper', params: {camperId: camper.id}}"><button type="button">Edit</button></router-link></td>
        <td><button type="button" v-on:click="deleteCamper(camper.id)">Delete</button></td>
     </tr>
    </table>
  </section>
</template>

<script>
import CamperService from '../services/CamperService.js';

export default {
  data() {
    return {
      newCamper: {
        firstName: '',
        lastName: '',
        city: '',
        state: '',
        zip: '',
        },
      showAddForm: false,
      campers: [],
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
    CamperService.getAllCampers()
    .then(response =>{
        console.log('Got all campers', response.data);
        this.campers = response.data;
    })
    .catch(response => {
        console.error('Problem getting all campers', response)
    })
  },
  computed: {
      filledForm(){
          return this.newCamper.firstName && this.newCamper.lastName && this.newCamper.city && this.newCamper.state && this.newCamper.zip;
      },
    filteredCampers() {
        let campersList = this.campers;
        if(this.firstNameToFilter)
        {
            campersList = campersList.filter(a => a.firstName.toLowerCase().includes(this.firstNameToFilter.toLowerCase()))
        }
        if(this.lastNameToFilter)
        {
            campersList = campersList.filter(a => a.lastName.toLowerCase().includes(this.lastNameToFilter.toLowerCase()))
        }
        if(this.cityToFilter)
        {
            campersList = campersList.filter(a => a.city.toLowerCase().includes(this.cityToFilter.toLowerCase()))
        }
        if(this.stateToFilter)
        {
            campersList = campersList.filter(a => a.state.toLowerCase().includes(this.stateToFilter.toLowerCase()))
        }
        if(this.zipToFilter)
        {
            campersList = campersList.filter(a => a.zipCode.toLowerCase().includes(this.zipToFilter.toLowerCase()))
        }        
    return campersList;
    }
  },
  methods: {
      saveNewCamper(){
          this.$store.commit('ADD_camper', this.newcamper);
      },
      deleteCamper(camperId){
          this.$store.commit('DELETE_camper', camperId);
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