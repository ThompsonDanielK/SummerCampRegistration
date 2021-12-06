<template>
  <div class="home">
    <h1>Home</h1>
    <p>Welcome to CAMP!<br>CAMP is a camp camper management program meant to simplify the hassle of adding and managing your summer camp campers.</p>
    <router-link v-bind:to="{ name: 'camper', params: {camperCode: '200001'}}">Camper Example</router-link>
    <router-link v-bind:to="{ name: 'camperList'}">Camper List Example</router-link>
  </div>
</template>

<script>
import CamperService from '../services/CamperService.js'

export default {
  name: "home",
  created(){
    CamperService.getAllCampers()
    .then(response => {
      console.log('Got all campers', response.data)
      this.$store.state.campers = response.data.slice();
    })
    .catch( response => {
      console.error('Problem getting campers list', response)
    })
  }
};
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

.home{
  display: flex;
  flex-direction: column;
  background-color: $textLight;
  border: 2px solid $highlight;
  border-radius: 20px;
  padding: 10px;
}
a{
  margin-top: 5px;
  color: $highlight;
}
</style>
