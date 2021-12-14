<template>
  <section>
    <div class="tableHead">
      <h1>Campers</h1>
      <router-link v-bind:to="{ name: 'new' }">
        <button type="button">Add New Camper</button>
      </router-link>
    </div>
    <table>
      <thead>
        <tr>
          <td class="labels">Search:</td>
          <td>
            <input
              type="text"
              v-model="firstNameToFilter"
              placeholder="First Name"
            />
          </td>
          <td>
            <input
              type="text"
              v-model="lastNameToFilter"
              placeholder="Last Name"
            />
          </td>
          <td>
            <select v-model="minAgeToFilter" class="ageFilter">
              <option>Any</option>
              <option v-for="num in minNums" v-bind:key="num">{{ num }}</option>
            </select>
            -
            <select v-model="maxAgeToFilter" class="ageFilter">
              <option>Any</option>
              <option v-for="num in maxNums" v-bind:key="num">{{ num }}</option>
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
            <input
              type="text"
              v-model="registrarToFilter"
              placeholder="Registrar"
            />
          </td>
          <td>
            <input
              type="text"
              v-model="familyIdToFilter"
              placeholder="Family"
            />
          </td>
          <td>
            <select v-model="activeToFilter">
              <option>Active</option>
              <option>Inactive</option>
              <option>All</option>
            </select>
          </td>
          <td>
            <select v-model="missingToFilter">
              <option value="">All</option>
              <option value="first_name">First Name</option>
              <option value="last_name">Last Name</option>
              <option value="age">Age</option>
              <option value="payment_status">Payment Status</option>
              <option value="registrar">Registrar</option>
              <option value="family">Family</option>
              <option value="active_status">Active Status</option>
            </select>
          </td>
        </tr>
      </thead>
      <tr class="labels">
        <td>Id</td>
        <td>First Name</td>
        <td>Last Name</td>
        <td id="age">Age</td>
        <td>Payment</td>
        <td>Registrar</td>
        <td>Family</td>
        <td>Status</td>
        <td>Missing</td>
      </tr>
      <tr
        v-for="camper in filteredCampers"
        v-bind:key="camper.camperCode"
        v-bind:showDetails="false"
      >
        <td>{{ camper.camperCode }}</td>
        <td>{{ camper.firstName }}</td>
        <td>{{ camper.lastName }}</td>
        <td>{{ camper.age }}</td>
        <td>
          <span v-if="camper.paymentStatus">Paid</span>
          <span v-if="!camper.paymentStatus">Unpaid</span>
        </td>
        <td>{{ camper.registrar }}</td>
        <td>{{ camper.familyId }} --<br />{{ camper.familyName }}</td>
        <td>
          <span v-if="camper.activeStatus">Active</span>
          <span v-if="!camper.activeStatus">Inactive</span>
        </td>
        <td>
          <ul>
            <li v-for="string in camper.missingData" v-bind:key="string">
              {{ string }}
            </li>
          </ul>
        </td>
        <td class="buttons">
          <router-link
            v-bind:to="{
              name: 'camper',
              params: { camperCode: camper.camperCode },
            }"
            ><button type="button">Edit</button></router-link
          >
        </td>
      </tr>
    </table>
    <email-form v-bind:filteredCampers="filteredCampers" />
  </section>
</template>

<script>
import CamperService from "../services/CamperService.js";
import FamilyService from "../services/FamilyService.js";
import EmailForm from "../components/EmailForm.vue"

export default {
  components:{
    EmailForm,
  },
  data() {
    return {
      firstNameToFilter: "",
      lastNameToFilter: "",
      familyIdToFilter: "",
      minAgeToFilter: "Any",
      maxAgeToFilter: "Any",
      paymentStatusToFilter: "All",
      registrarToFilter: "",
      missingToFilter: "",
      activeToFilter: "Active",
      campers: [],
      families: [],
    };
  },
  computed: {
    minNums() {
      let list = [];
      for (let num = 1; num < 100; num++) {
        list.push(num);
      }
      return list;
    },
    maxNums() {
      let list = [];
      let minAge;
      if (this.minAgeToFilter == "Any") {
        minAge = 1;
      } else {
        minAge = parseInt(this.minAgeToFilter);
      }
      for (let num = minAge + 1; num < 101; num++) {
        list.push(num);
      }
      return list;
    },
    filteredCampers() {
      if(!this.campers.length > 0)
      {
        return [];
      }
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
      if (this.paymentStatusToFilter != "All") {
        campersList = campersList.filter(
          (a) => this.paymentStatusToFilter === 'Paid'? a.paymentStatus: !a.paymentStatus
        );
      }
      if (this.familyIdToFilter) {
        campersList = campersList.filter(
          (a) =>
            a.familyId.toString().includes(this.familyIdToFilter.toString()) ||
            a.familyName.includes(this.familyIdToFilter)
        );
      }
      let minAge = 0;
      let maxAge = 0;
      if (this.minAgeToFilter != "Any") {
        minAge = parseInt(this.minAgeToFilter);
      }
      if (this.maxAgeToFilter != "Any") {
        maxAge = parseInt(this.maxAgeToFilter);
      }
      if (minAge && maxAge) {
        campersList = this.campers.filter(
          (a) => a.age >= minAge && a.age <= maxAge
        );
      }
      if (minAge && !maxAge) {
        campersList = campersList.filter((a) => a.age >= minAge);
      }
      if (!minAge && maxAge) {
        campersList = campersList.filter((a) => a.age <= maxAge);
      }
      if (this.activeToFilter != "All") {
        campersList = campersList.filter((c) =>
          this.activeToFilter == "Active" ? c.activeStatus : !c.activeStatus
        );
      }
      if (this.missingToFilter) {
        campersList = campersList.filter((a) =>
          a.missingData.includes(this.missingToFilter)
        );
      }
      return campersList;
    },
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
            this.$store.commit("SET_FAMILY_LIST", this.families);
            this.$store.commit("SET_CAMPER_LIST", this.campers);
            this.$forceUpdate();
          })
          .catch((response) => {
            console.error("Problem getting all families", response);
          });
      })
      .catch((response) => {
        console.error("Problem getting all campers", response);
      });
  },
};
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

section {
  background-color: $textLight;
  padding: 1%;
  border-radius: 20px;
  border: 2px solid $textDark;
}
td {
  padding-right: 1%;
  width: 10%;
}
table {
  padding: 1%;
  font-size: 120%;
}
button {
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  border: 1px solid $highlight;
  text-shadow: 2px 1px 1px black;
  font-family: "Lora", serif;
  box-shadow: 1.5px 1px 1px $secondary;
  margin-right: 5px;
}
input,
select {
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  border: 1px solid $highlight;
  width: 100%;
  font-size: 0.9rem;
  font-family: "Lora", serif;
  box-shadow: 2px 1px 1px $secondary;
}
input::placeholder {
  color: $textLight;
}
.ageFilter {
  width: 40%;
}
.tableHead {
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-size: 1.2rem;
}
.labels {
  font-weight: bold;
  font-size: 1.2rem;
}
tr {
  margin: 10%;
}
tr td:last-child {
  width: 1%;
}
tr td:nth-child(6) {
  width: 5%;
}
tr td:nth-child(5) {
  width: 5%;
}
tr td:nth-child(8) {
  width: 4%;
}
tr td:first-child {
  width: 4%;
}
</style>