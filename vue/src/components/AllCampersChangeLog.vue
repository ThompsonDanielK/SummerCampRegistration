<template>
  <table>
    <thead>
      <tr>
        <td>ID</td>
        <td>Camper</td>
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
    <tr>
      <td><input type="text" v-model="idToFilter" /></td>
      <td><input type="text" v-model="camperToFilter" /></td>
      <td>
        <select v-model="actionToFilter">
          <option>All</option>
          <option>Update</option>
          <option>ADD</option>
        </select>
      </td>
      <td><input type="text" v-model="fieldToFilter" /></td>
      <td><input type="text" v-model="currentToFilter" /></td>
      <td><input type="text" v-model="newToFilter" /></td>
      <td><input type="text" v-model="userToFilter" /></td>
      <td></td>
      <td></td>
      <td>
        <select v-model="statusToFilter">
          <option>All</option>
          <option>Pending</option>
          <option>Approved</option>
          <option>Rejected</option>
        </select>
      </td>
    </tr>
    <tr v-for="update in filteredUpdates" v-bind:key="update.requestId">
      <td>
        <router-link
          v-bind:to="{
            name: 'camper',
            params: { camperCode: update.camperCode },
          }"
          >{{ update.requestId }}</router-link
        >
      </td>
      <td>{{ update.camperCode }}</td>
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
      idToFilter: 0,
      camperToFilter: 0,
      actionToFilter: "All",
      fieldToFilter: "",
      currentToFilter: "",
      newToFilter: "",
      userToFilter: "",
      statusToFilter: "All",
    };
  },
  computed: {
    filteredUpdates() {
      let updateList = this.updates.slice();
      if (this.idToFilter) {
        updateList = updateList.filter((u) =>
          u.requestId.toString().includes(this.idToFilter)
        );
      }
      if (this.camperToFilter) {
        updateList = updateList.filter((u) =>
          u.camperCode.toString().includes(this.camperToFilter)
        );
      }
      if (this.actionToFilter != "All") {
        updateList = updateList.filter((u) =>
          u.action.includes(this.actionToFilter)
        );
      }
      if (this.fieldToFilter) {
        updateList = updateList.filter((u) =>
          u.fieldToBeChanged.includes(this.fieldToFilter)
        );
      }
      if (this.currentToFilter) {
        updateList = updateList.filter((u) =>
          u.oldData.includes(this.currentToFilter)
        );
      }
      if (this.newToFilter) {
        updateList = updateList.filter((u) =>
          u.newData.includes(this.newToFilter)
        );
      }
      if (this.userToFilter) {
        updateList = updateList.filter((u) =>
          u.requestor.includes(this.userToFilter)
        );
      }
      if (this.statusToFilter != "All") {
        updateList = updateList.filter((u) =>
          u.status.includes(this.statusToFilter)
        );
      }
      return updateList;
    },
  },
  created() {
    UpdateService.getAllUpdates()
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
  width: 8%;
  border-right: 1px solid $highlight;
}
tr td:nth-child(4) {
  width: 12%;
  border-right: 1px solid $highlight;
}
tr td:nth-child(5) {
  width: 10%;
  border-right: 1px solid $highlight;
}
tr td:nth-child(6) {
  width: 12%;
  border-right: 1px solid $highlight;
}
tr td:nth-child(7) {
  width: 10%;
  border-right: 1px solid $highlight;
}
tr td:nth-child(8) {
  width: 11%;
  border-right: 1px solid $highlight;
}
tr td:nth-child(9) {
  width: 11%;
  border-right: 1px solid $highlight;
}
tr td:last-child {
  width: 10%;
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