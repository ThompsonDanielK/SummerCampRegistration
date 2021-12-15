<template>
  <table>
    <thead>
      <tr>
        <td>ID</td>
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
      <td>{{ update.action }}</td>
      <td>{{ update.fieldToBeChanged }}</td>
      <td>{{ update.oldData }}</td>
      <td>{{ update.newData }}</td>
      <td>{{ update.requestor }}</td>
      <td>{{ update.requestDate }}</td>
      <td>{{ update.finalizedDate }}</td>
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
    UpdateService.getUpdatesByFamilyId(this.$route.params.familyId)
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
  margin: 2% 0;
  padding: 0 2%;
  font-size: 0.85rem;
  display: flex;
  justify-content: center;
}
td:first-child {
  width: 7%;
  border-right: 1px solid $highlight;
}
td:nth-child(2) {
  width: 7%;
  border-right: 1px solid $highlight;
}
td:nth-child(3) {
  width: 13%;
  border-right: 1px solid $highlight;
}
td:nth-child(4) {
  width: 12%;
  border-right: 1px solid $highlight;
}
td:nth-child(5) {
  width: 14%;
  border-right: 1px solid $highlight;
}
td:nth-child(6) {
  width: 12%;
  border-right: 1px solid $highlight;
}
td:nth-child(7) {
  width: 12%;
  border-right: 1px solid $highlight;
}
td:nth-child(8) {
  width: 12%;
  border-right: 1px solid $highlight;
}
td:last-child {
  width: 8%;
}
tr {
  border: 1px solid $highlight;
}
thead {
  font-weight: bold;
}
</style>