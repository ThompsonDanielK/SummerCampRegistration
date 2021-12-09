<template>
  <section>
    <div class="tableHead">
    <h1>Campers</h1>
      <router-link v-bind:to="{name: 'new'}">
        <button type="button">Add New Camper</button>
      </router-link>
    </div>
    <table>
      <thead>
        <tr>
          <td>Search:</td>
          <td>
            <input type="text" v-model="firstNameToFilter" placeholder="First Name"  />
          </td>
          <td>
            <input type="text" v-model="lastNameToFilter" placeholder="Last Name"  />
          </td>
          <td>
            <select v-model="minAgeToFilter" class="ageFilter">
            <option>Any</option>
            <option v-for="num in minNums" v-bind:key="num">{{num}}</option>
            </select>
            <select v-model="maxAgeToFilter" class="ageFilter">
            <option>Any</option>
            <option v-for="num in maxNums" v-bind:key="num">{{num}}</option>
            </select>
            </td>
            <td>
              <select v-model="paymentStatusToFilter">
              <option>All</option>
              <option>Paid</option>
              <option>Unpaid</option>
              </select>
          </td>
          <td>
            <input type="text" v-model="registrarToFilter" placeholder="Registrar ID" />
          </td>
          <td>
            <input type="text" v-model="familyIdToFilter" placeholder="Family ID" />
          </td>
          <td>
            <input type="text" v-model="missingToFilter" />
          </td>
          <td>
              <select v-model="activeToFilter">
              <option>All</option>
              <option>Active</option>
              <option>Inactive</option>
              </select>
          </td>
        </tr>
      </thead>
      <tr class="labels">
        <td>Camper Code</td>
        <td>First Name</td>
        <td>Last Name</td>
        <td id="age">Age</td>
        <td>Payment Status</td>
        <td>Registrar</td>
        <td>Family</td>
        <td>Missing Info</td>
        <td>Active Status</td>
      </tr>
      <tr
        v-for="camper in this.filteredCampers"
        v-bind:key="camper.camperCode"
        v-bind:showDetails="false"
      >
        <td>{{ camper.camperCode }}</td>
        <td>{{ camper.firstName }}</td>
        <td>{{ camper.lastName }}</td>
        <td id="age">{{ camper.age }}</td>
        <td>{{ camper.paymentStatus }}</td>
        <td>{{ camper.registrar }}</td>
        <td>{{ camper.familyId }} {{camper.familyName}}</td>
        <td>{{camper.missingData.join(', ')}}</td>
        <td>{{camper.active}}</td>
        <td class="buttons">
          <router-link v-bind:to="{ name: 'camper', params: { camperCode: camper.camperCode },}"><button type="button">Edit</button></router-link>
          <button type="button" v-on:click="deleteCamper(camper.camperCode)">
            Delete
          </button>
        </td>
      </tr>
    </table>
  </section>
</template>

<script>
import CamperService from "../services/CamperService.js";

export default {
  data() {
    return {
      campers: [],
      families: [],
      firstNameToFilter: "",
      lastNameToFilter: "",
      familyIdToFilter: "",
      minAgeToFilter: 'Any',
      maxAgeToFilter: 'Any',
      paymentStatusToFilter: 'All',
      registrarToFilter: '',
      missingToFilter: '',
      activeToFilter: 'All',
    };
  },
  created() {
    this.families = this.$store.state.families; 
    this.campers = this.$store.state.campers;

  },
  computed: {
    minNums(){
      let list = []
      for(let num = 1; num < 100; num++)
      {
        list.push(num);
      }
      return list;
    },
    maxNums(){
      let list = [];
      let minAge; 
      if(this.minAgeToFilter == 'Any')
      {
        minAge = 1;
      }
      else
      {
        minAge=parseInt(this.minAgeToFilter);
      }
      for(let num = minAge + 1; num < 101; num++)
      {
        list.push(num);
      }
      return list;
    },
    filteredCampers() {
      let campersList = this.campers;
      if (this.firstNameToFilter) {
        campersList = campersList.filter((a) =>
          a.firstName
            .toLowerCase()
            .includes(this.firstNameToFilter.toLowerCase())
        );
      }
      if (this.lastNameToFilter) {
        campersList = campersList.filter((a) =>
          a.lastName.toLowerCase().includes(this.lastNameToFilter.toLowerCase())
        );
      }
      if (this.paymentStatusToFilter && this.paymentStatusToFilter != 'All') {
        campersList = campersList.filter((a) =>
          a.paymentStatus == this.paymentStatusToFilter);
      }
      if (this.familyIdToFilter) {
        campersList = campersList.filter((a) =>
          a.familyId.toString().includes(this.familyIdToFilter.toString())
        );
      }
      let minAge = 0;
      let maxAge = 0;
      if(this.minAgeToFilter != 'Any')
      {
        minAge = parseInt(this.minAgeToFilter);
      }
      if(this.maxAgeToFilter != 'Any')
      {
        maxAge = parseInt(this.maxAgeToFilter);
      }
      if (minAge && maxAge) {
        campersList = campersList.filter((a) =>
          a.age >= minAge && a.age <= maxAge);
      }
      if(minAge && !maxAge) {
        campersList = campersList.filter((a) =>
          a.age >= minAge);
      }
      if(!minAge && maxAge) {
        campersList = campersList.filter((a) =>
          a.age <= maxAge);
      }
      return campersList;
    },
  },
  methods: {
    deleteCamper(camperCode) {
        CamperService.deleteCamper(this.camper.camperCode)
        .then(() => {
            console.log('Removed camper');
            this.$store.commit("DELETE_CAMPER", this.newCamper.camperCode);
        })
        .catch(response => {
            console.error('Problem deleting camper', response);
        })
      this.$store.commit("DELETE_camper", camperCode);
    },
  }
}
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

section {
  background-color: $textLight;
  padding: 1%;
  border-radius: 20px;
  border: 2px solid $textDark;
}
td{
  padding-right: 1%;
  width: 10%;
  }
table {
  padding: 1%;
  font-size: 90%;
}
button {
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  border: 1px solid $highlight;
  text-shadow: 2px 1px 1px black;
  font-family: 'Lora', serif;
  box-shadow: 1.5px 1px 1px $secondary;
  margin-right: 5px;
}
.buttons{
  display: flex;
}
input, select{
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  border: 1px solid $highlight;
  width: 100%;
  font-size: 0.9rem;
  font-family: 'Lora', serif;
  box-shadow: 2px 1px 1px $secondary;
}
input::placeholder{
  color: $textLight;
}
.ageFilter{
  width: 50%;
}
.tableHead{
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.labels{
  font-weight: bold;
  font-size: 1rem;
}
tr{
  margin: 10%;
}
</style>