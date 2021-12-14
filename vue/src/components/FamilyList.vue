<template>
  <section>
    <div class="tableHead">
      <h1>Families</h1>
      <router-link v-bind:to="{ name: 'new' }">
        <button type="button">Add New Camper</button>
      </router-link>
    </div>
    <table>
      <thead>
        <tr>
          <td>Search:</td>
          <td>
            <input
              type="text"
              v-model="fullNameToFilter"
              placeholder="Full Name"
            />
          </td>
          <td>
            <input type="text" v-model="cityToFilter" placeholder="City" />
          </td>
          <td>
            <select v-model="stateToFilter" placeholder="State">
              <option></option>
              <option v-for="state in $store.state.states" v-bind:key="state">
                {{ state }}
              </option>
            </select>
          </td>
        </tr>
      </thead>
      <tr>
        <td>ID:</td>
        <td>Full Name:</td>
        <td>City:</td>
        <td>State:</td>
        <td>Campers:</td>
        <td></td>
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
          <ul>
            <li v-for="camper in family.campers" v-bind:key="camper.camperCode">
              <router-link
                v-bind:to="{
                  name: 'camper',
                  params: { camperCode: camper.camperCode },
                }"
                >{{ camper.camperCode }} - {{ camper.firstName }}
                {{ camper.lastName }}</router-link
              >
            </li>
          </ul>
        </td>
        <td>
          <router-link
            v-bind:to="{
              name: 'family',
              params: { familyId: family.familyId },
            }"
            ><button type="button">Edit</button></router-link
          >
        </td>
      </tr>
    </table>
  </section>
</template>

<script>
import CamperService from "../services/CamperService.js";
import FamilyService from "../services/FamilyService.js";

export default {
  data() {
    return {
      campers: [],
      families: [],
      fullNameToFilter: "",
      cityToFilter: "",
      stateToFilter: "",
    };
  },
  created() {
    CamperService.getAllCampers()
      .then((response) => {
        this.campers = response.data;
        console.log("Got all campers", this.campers);
        FamilyService.getAllFamilies()
          .then((response) => {
            this.families = response.data;
            console.log("Got all families", this.families);
            this.families.forEach((f) => {
              f.campers = this.campers.filter((c) => c.familyId == f.familyId);
            });
            this.$store.commit("SET_CAMPER_LIST", this.campers);
          })
          .catch((response) => {
            console.error("Problem getting all families", response);
          });
      })
      .catch((response) => {
        console.error("Problem getting all campers", response);
      });
  },
  computed: {
    filteredFamilies() {
      let campersList = this.families;
      if (this.fullNameToFilter) {
        campersList = campersList.filter((a) =>
          a.fullName.toLowerCase().includes(this.fullNameToFilter.toLowerCase())
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
};
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

td {
  padding-right: 2%;
}
table {
  padding: 2%;
  display: block;
}
button {
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  border: 2px solid $highlight;
  text-shadow: 2px 1px 1px black;
  font-size: 1rem;
  font-family: "Lora", serif;
}
input,
select {
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  border: 2px solid $highlight;
  width: 90%;
  font-weight: bold;
  font-size: 1rem;
  font-family: "Lora", serif;
}
.tableHead,
thead,
tr:nth-child(2) {
  font-size: 105%;
  font-weight: bold;
}
td:first-child {
  width: 10%;
}
td:nth-child(2) {
  width: 20%;
}
td:nth-child(3) {
  width: 16%;
}
td:nth-child(4) {
  width: 10%;
}
td:nth-child(5) {
  width: 30%;
  padding: 0;
}
td:last-child {
  width: 6%;
}
input::-webkit-input-placeholder {
  color: $textLight;
}
a {
  color: $highlight;
  text-decoration: none;
}
.tableHead {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
}
</style>