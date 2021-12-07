<template>
  <section>
    <h1>Campers</h1>
    <button type="button" v-on:click="showAddForm = true" v-show="!showAddForm">
      Add New Camper
    </button>
    <form class="form-group" v-show="showAddForm">
      <label for="firstName">First Name:</label>
      <input v-model="newCamper.firstName" name="firstName" type="text" />
      <label for="lastName">Last Name:</label>
      <input v-model="newCamper.lastName" name="lastName" type="text" />
      <label for="familyId">Family:</label>
      <input v-model="newCamper.familyId" name="familyId" type="text" />
      <button type="submit" v-on:click.prevent="saveNewCamper()" v-bind:disabled="!filledForm">Submit</button>
      <button type="button" v-on:click=" showAddForm = false; newcamper = {};">Cancel</button>
    </form>
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
          <router-link
            v-bind:to="{
              name: 'camper',
              params: { camperCode: camper.camperCode },
            }"
            ><button type="button">Edit</button></router-link
          >
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
      newCamper: {
        firstName: "",
        lastName: "",
        familyId: "",
      },
      showAddForm: false,
      campers: [],
      firstNameToFilter: "",
      lastNameToFilter: "",
      familyIdToFilter: "",
      states: ["AL", "AK", "AR", "AZ", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA",
               "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH",
               "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VA",
               "VT", "WA", "WI", "WV", "WY"],
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
    filledForm() {
      return (
        this.newCamper.firstName &&
        this.newCamper.lastName &&
        this.newCamper.familyId
      );
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
    saveNewCamper() {
        CamperService.addCamper(this.newCamper)
        .then(response => {
            console.log('New camper added', response.data);
            this.$store.commit("ADD_CAMPER", this.newcamper);
            this.$router.push(`/camp/camper/${response.data.camperCode}`);
        })
        .catch(response => {
            console.error('Problem adding new camper', response);
        })
    },
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
  padding: 10px;
  border-radius: 20px;
  border: 2px solid $textDark;
}
table {
  width: 100%;
  padding: 10px;
}
input {
  width: 80%;
}
.form-group {
  display: flex;
  flex-direction: column;
  width: 30%;
  margin: 10px;
  background-color: $secondary;
  padding: 10px;
  border-radius: 10px;
  border: 2px solid black;
}
input,
.form-group input,
select {
  margin: 5px;
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  padding-left: 10px;
  border: 1px solid $highlight;
}
button {
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  border: 1px solid $highlight;
}
button:disabled {
  background-color: $secondary;
}
</style>