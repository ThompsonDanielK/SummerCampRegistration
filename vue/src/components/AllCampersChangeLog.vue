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
        <td><input type ="text" v-model="idToFilter"></td>
        <td><input type ="text" v-model="camperToFilter"></td>
        <td><input type ="text" v-model="actionToFilter"></td>
        <td><input type ="text" v-model="fieldToFilter"></td>
        <td><input type ="text" v-model="currentToFilter"></td>
        <td><input type ="text" v-model="newToFilter"></td>
        <td><input type ="text" v-model="userToFilter"></td>
        <td></td>
        <td></td>
        <td><input type ="text" v-model="statusToFilter"></td>
      </tr>
    <tr v-for="update in updates" v-bind:key="update.requestId">
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
      actionToFilter: '',
      fieldToFilter: '',
      currentToFilter: '',
      newToFilter: '',
      userToFilter: '',
      statusToFilter: '',
    };
  },
  computed: {
    filteredUpdates(){
      let updateList = this.updates.slice();
      if(this.idToFilter)
      {
        updateList = updateList.filter(u => u.requestId == this.idToFilter)
      }
      if(this.camperToFilter)
      {
        updateList = updateList.filter(u => u.camperCode == this.camperToFilter)
      }
      if(this.actionToFilter)
      {
        updateList = updateList.filter(u => u.camperCode == this.actionToFilter)
      }
      if(this.fieldToFilter)
      {
        updateList = updateList.filter(u => u.camperCode == this.fieldToFilter)
      }
      if(this.currentToFilter)
      {
        updateList = updateList.filter(u => u.camperCode == this.currentToFilter)
      }
      if(this.newToFilter)
      {
        updateList = updateList.filter(u => u.camperCode == this.newToFilter)
      }
      if(this.userToFilter)
      {
        updateList = updateList.filter(u => u.camperCode == this.userToFilter)
      }
      if(this.statusToFilter)
      {
        updateList = updateList.filter(u => u.camperCode == this.statusToFilter)
      }
      return updateList
    },
  },
  created() {
    UpdateService.getAllUpdates()
      .then((response) => {
        console.log("Got all updates", response.data);
        this.updates = response.data;
        this.$forceUpdate();
      })
      .catch((response) => {
        console.error("Could not get updates", response);
      });
    if (typeof this.updates == Object) {
      if (!this.updates.oldData) {
        this.updates.oldData = "N/A";
      }
      if (
        Date.parse(this.updates.finalizeDate) ==
        Date.parse("0001-01-01T00:00:00")
      ) {
        this.updates.finalizeDate = "N/A";
      }
    } else {
      this.updates.forEach((u) => {
        if (!u.oldData) {
          u.oldData = "N/A";
        }
        if (Date.parse(u.finalizeDate) == Date.parse("0001-01-01T00:00:00")) {
          u.finalizeDate = "N/A";
        }
      });
    }
  },
};
</script>

<style lang="scss" scoped>
@import "../styles/colors.scss";

table {
  display: block;
  margin: 2% 0;
  padding: 0 2%;
  font-size: 0.85rem;
}
thead {
  border: 1px solid $highlight;
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
  font-size: 1.08rem;
  font-weight: bold;
}
a {
  color: $highlight;
  text-decoration: none;
}
</style>