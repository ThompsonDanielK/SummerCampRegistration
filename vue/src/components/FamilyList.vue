<template>
  <section>
    <h1>Families</h1>
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
        </tr>
      </thead>
      <tr>
        <td>Family ID:</td>
        <td>First Name:</td>
        <td>Last Name:</td>
        <td></td>
      </tr>
      <tr
        v-for="family in this.filteredFamilies"
        v-bind:key="family.familyId"
        v-bind:showDetails="false"
      >
        <td>{{ family.familyId }}</td>
        <td>{{ family.firstName }}</td>
        <td>{{ family.lastName }}</td>
        <td>{{ family.familyId }}</td>
        <td>
          <router-link v-bind:to="{ name: 'family', params: { familyId: family.familyId },}"><button type="button">Edit</button></router-link>
        </td>
        <td>
          <button type="button" v-on:click="deleteFamily(family.familyId)">
            Delete
          </button>
        </td>
      </tr>
    </table>
  </section>
</template>

<script>
import FamilyService from "../services/FamilyService.js";

export default {
  data() {
    return {
      families: [],
      firstNameToFilter: "",
      lastNameToFilter: "",
    };
  },
  created() {
    FamilyService.getAllFamilies()
      .then((response) => {
        console.log("Got all families", response.data);
        this.families = response.data;
      })
      .catch((response) => {
        console.error("Problem getting all families", response);
      });
  },
  computed: {
    filteredCampers() {
      let campersList = this.families;
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
      if (this.cityToFilter) {
        campersList = campersList.filter((a) =>
          a.city.toLowerCase().includes(this.cityToFilter.toLowerCase())
        );
      }
      if (this.stateToFilter) {
        campersList = campersList.filter((a) =>
          a.state.toLowerCase().includes(this.stateToFilter.toLowerCase())
        );
      }
      if (this.zipToFilter) {
        campersList = campersList.filter((a) =>
          a.zipCode.toLowerCase().includes(this.zipToFilter.toLowerCase())
        );
      }
      return campersList;
    },
  },
  methods: {
    deleteFamily(familyId) {
        FamilyService.deleteCamper(familyId)
        .then(() => {
            console.log('Removed family');
            this.$store.commit("DELETE_FAMILY", familyId);
        })
        .catch(response => {
            console.error('Problem deleting family', response);
        })
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