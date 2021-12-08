<template>
  <section>
    <h1>Campers</h1>
    <router-link v-bind:to="{name: 'new'}">
      <button type="button">Add New Camper</button>
    </router-link>
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
            <input type="text" v-model="familyIdToFilter" placeholder="Family ID" />
          </td>
        </tr>
      </thead>
      <tr>
        <td>Camper Code:</td>
        <td>First Name:</td>
        <td>Last Name:</td>
        <td>Family:</td>
        <td></td>
      </tr>
      <tr
        v-for="camper in this.filteredCampers"
        v-bind:key="camper.camperCode"
        v-bind:showDetails="false"
      >
        <td>{{ camper.camperCode }}</td>
        <td>{{ camper.firstName }}</td>
        <td>{{ camper.lastName }}</td>
        <td>{{ camper.familyId }}</td>
        <td>
          <router-link v-bind:to="{ name: 'camper', params: { camperCode: camper.camperCode },}"><button type="button">Edit</button></router-link>
        </td>
        <td>
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
      firstNameToFilter: "",
      lastNameToFilter: "",
      familyIdToFilter: "",
    };
  },
  created() {
    CamperService.getAllCampers()
      .then((response) => {
        console.log("Got all campers", response.data);
        this.campers = response.data;
      })
      .catch((response) => {
        console.error("Problem getting all campers", response);
      });
  },
  computed: {
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
      if (this.familyIdToFilter) {
        campersList = campersList.filter((a) =>
          a.familyId.toString().includes(this.familyIdToFilter.toString())
        );
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
  },
};
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

section {
  background-color: $textLight;
  padding: 2%;
  border-radius: 20px;
  border: 2px solid $textDark;
}
td{
  padding-right: 2%;
  }
table {
  padding: 2%;
}
button {
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  border: 1px solid $highlight;
}
input{
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  border: 1px solid $highlight;
  width: 90%;
}
</style>