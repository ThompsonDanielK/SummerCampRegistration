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
        <camper-list-filter v-bind:campers="campers" />
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
        v-for="camper in $store.state.filteredCampers"
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
    <email-form v-bind:campers="campers" />
  </section>
</template>

<script>
import CamperService from "../services/CamperService.js";
import FamilyService from "../services/FamilyService.js";
import EmailForm from "../components/EmailForm.vue";
import CamperListFilter from "../components/CamperListFilter.vue";

export default {
  components:{
    EmailForm,
    CamperListFilter
  },
  data() {
    return {
      campers: [],
      families: [],
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