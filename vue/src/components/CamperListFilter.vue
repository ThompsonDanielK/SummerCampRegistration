<template>
  <tr>
    <td class="labels">Search:</td>
    <td>
      <input type="text" v-model="firstNameToFilter" placeholder="First Name" />
    </td>
    <td>
      <input type="text" v-model="lastNameToFilter" placeholder="Last Name" />
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
      <input type="text" v-model="registrarToFilter" placeholder="Registrar" />
    </td>
    <td>
      <input type="text" v-model="familyIdToFilter" placeholder="Family" />
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
</template>

<script>
export default {
    props:{
        campers: Array,
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
       if (this.registrarToFilter) {
           campersList = campersList.filter((a) =>
          a.registrar.toLowerCase().includes(this.registrarToFilter.toLowerCase())
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
      this.$state.commit('SET_FILTERED_CAMPERS', this.filteredCampers);
      return campersList;
    }
    },
};
</script>

<style>
</style>