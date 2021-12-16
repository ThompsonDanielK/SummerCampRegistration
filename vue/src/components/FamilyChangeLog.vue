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
      <td>{{ update.finalizeDate }}</td>
      <td>{{ update.status }}</td>
    </tr>
  </table>
</template>

<script>
export default {
  props: {
    updates: Array,
  },
};
</script>

<style lang="scss" scoped>
@import "../styles/colors.scss";

table {
  margin: 2% 0;
  padding: 0 2%;
  font-size: 0.85rem;
  width: 100%;
}
td:first-child {
  width: 7%;
  border-right: 1px solid $highlight;
  border-left: 1px solid $highlight;
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
  width: 10%;
  border-right: 1px solid $highlight;
}
td:nth-child(7) {
  width: 14%;
  border-right: 1px solid $highlight;
}
td:nth-child(8) {
  width: 14%;
  border-right: 1px solid $highlight;
}
td:last-child {
  width: 10%;
  border-right: 1px solid $highlight;
}
td {
  padding-left: 0.5%;
}
thead {
  font-weight: bold;
}
</style>