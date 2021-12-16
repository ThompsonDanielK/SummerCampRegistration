<template>
  <table>
    <thead>
      <tr>
        <td>ID</td>
        <td>Family</td>
        <td>Action</td>
        <td>Value</td>
        <td>Current</td>
        <td>New</td>
        <td>Requester</td>
        <td>Requested</td>
        <td>Finalized</td>
        <td>Status</td>
      </tr>
    </thead>
    <tr v-for="update in updates" v-bind:key="update.requestId">
      <td>{{ update.requestId }}</td>
      <td><router-link v-bind:to="{name: 'family', params: {familyId: update.familyId}}">{{ update.familyId }}</router-link></td>
      <td>{{ update.action }}</td>
      <td>{{ update.fieldToBeChanged }}</td>
      <td>{{ update.oldData }}</td>
      <td>{{ update.newData }}</td>
      <td>{{ update.requestor }}</td>
      <td>{{ update.requestDate }}</td>
      <td>{{ update.finalizeDate }}</td>
      <td>{{ update.status }}</td>
    </tr>
  </table>
</template>

<script>
import UpdateService from "../services/UpdateService.js";

export default {
  data() {
    return {
      updates: [],
    };
  },
  created() {
    UpdateService.getAllFamilyUpdates()
      .then((response) => {
        console.log("Got all updates", response.data);
        this.updates = response.data;
        this.updates.forEach((u) => {
          if (!u.oldData) {
            u.oldData = "N/A";
          }
          if (!u.newData) {
            u.newData = "N/A";
          }
          u.requestDate =
            new Date(u.requestDate).getMonth() +
            1 +
            "/" +
            new Date(u.requestDate).getDate() +
            "/" +
            new Date(u.requestDate).getFullYear();
          u.finalizeDate =
            new Date(u.finalizeDate).getMonth() +
            1 +
            "/" +
            new Date(u.finalizeDate).getDate() +
            "/" +
            new Date(u.finalizeDate).getFullYear();
          if (u.finalizeDate == "1/1/1") {
            u.finalizeDate = "N/A";
          }
        });
        this.$forceUpdate();
      })
      .catch((response) => {
        console.error("Could not get updates", response);
      });
  },
};
</script>

<style lang="scss" scoped>
@import "../styles/colors.scss";

table {
  display: block;
}
input {
  width: 90%;
}
td {
  padding-left: 0.5%;
  padding-right: 0.5%;
}
thead {
  font-size: 1.08rem;
  font-weight: bold;
}
tr td:first-child {
  width: 7%;
  border-right: 1px solid $highlight;
}
tr td:nth-child(2) {
  width: 8%;
  border-right: 1px solid $highlight;
}
tr td:nth-child(3) {
  width: 7%;
  border-right: 1px solid $highlight;
}
tr td:nth-child(4) {
  width: 13%;
  border-right: 1px solid $highlight;
}
tr td:nth-child(5) {
  width: 16%;
  border-right: 1px solid $highlight;
}
tr td:nth-child(6) {
  width: 16%;
  border-right: 1px solid $highlight;
}
tr td:nth-child(7) {
  width: 10%;
  border-right: 1px solid $highlight;
}
tr td:nth-child(8) {
  width: 10%;
  border-right: 1px solid $highlight;
}
tr td:nth-child(9) {
  width: 10%;
  border-right: 1px solid $highlight;
}
input,
select {
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  border: 1px solid $highlight;
  font-size: 0.9rem;
  font-family: "Lora", serif;
  box-shadow: 2px 1px 1px $secondary;
}
input::placeholder {
  color: $textLight;
}
a {
  color: $highlight;
  text-decoration: none;
}
</style>