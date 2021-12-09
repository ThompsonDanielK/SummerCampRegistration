<template>
  <section>
    <div class="tableHead">
    <h1>Families</h1>
    <router-link v-bind:to="{name: 'new'}">
      <button type="button">Add New Camper</button>
    </router-link>
    </div>
    <table>
      <thead>
        <tr>
          <td>Search:</td>
          <td>
            <input type="text" v-model="fullNameToFilter" placeholder="Full Name"  />
          </td>
          <td>
            <input type="text" v-model="cityToFilter" placeholder="City"  />
          </td>
          <td>
            <input type="text" v-model="stateToFilter" placeholder="State"  />
          </td>
        </tr>
      </thead>
      <tr>
        <td>Family ID:</td>
        <td>Full Name:</td>
        <td>City:</td>
        <td>State:</td>
      </tr>
      <tr
        v-for="family in this.filteredFamilies"
        v-bind:key="family.familyId"
        v-bind:showDetails="false"
      >
        <td>{{ family.familyId }}</td>
        <td>{{ family.fullName }}</td>
        <td>{{ family.city }}</td>
        <td>{{ family.state }}</td>

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
      fullNameToFilter: "",
      cityToFilter: "",
      stateToFilter: "",
    };
  },
  created() {
    this.families = this.$store.state.families;
  },
  computed: {
    filteredFamilies() {
      let campersList = this.families;
      if (this.fullNameToFilter) {
        campersList = campersList.filter((a) =>
          a.fullName
            .toLowerCase()
            .includes(this.fullNameToFilter.toLowerCase())
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
  border: 2px solid $highlight;
  text-shadow: 2px 1px 1px black;
  font-size: 1rem;
  font-family: 'Russo One', sans-serif;
}
input{
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  border: 2px solid $highlight;
  width: 90%;
  font-weight: bold;
  font-size: 1rem;
  font-family: 'Russo One', sans-serif;
}
.tableHead{
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>