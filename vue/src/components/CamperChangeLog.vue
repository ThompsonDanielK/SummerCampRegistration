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
      <tr v-for="update in updates" v-bind:key="update">
        <td><router-link v-bind:to="{ name:'camper', params: {camperCode: update.camperCode}}">{{ update.requestId }}</router-link></td>
        <td>{{update.camperCode}}</td>
        <td>{{update.action}}</td>
        <td>{{update.fieldToBeChanged}}</td>
        <td>{{update.oldData}}</td>
        <td>{{update.newData}}</td>
        <td>{{update.requestor}}</td>
        <td>{{update.requestDate}}</td>
        <td>{{update.finalizeDate}}</td>
        <td>{{update.status}}</td>
      </tr>
  </table>
</template>

<script>
import UpdateService from '../services/UpdateService.js'

export default {
    data(){
        return{
            updates: [],
        }
    },
created(){
    UpdateService.getUpdatesByCamperCode(this.$route.params.camperCode)
    .then(response => {
        console.log('Got all updates', response.data);
        this.updates = response.data;
        this.$forceUpdate();
    })
    .catch(response => {
        console.error('Could not get updates', response)
    });
    this.updates.forEach( u => {
        if(!u.oldData)
        {
            u.oldData = 'N/A';
        }
        if(!u.finalizeDate)
        {
            u.finalizeDate = 'N/A';
        }
    })
},
}
</script>

<style lang="scss" scoped>
@import "../styles/colors.scss";

table{
    display: flex;
    justify-content: center;
    margin: 2% 0;
}
td{
    margin: 0px;
    padding: 0px;
    border: 1px solid $highlight
}
tr{
    border: 1px solid $highlight;
}
</style>