<template>
  <div class="home">
    <h1>Home</h1>
    <p>Welcome to CAMP!<br>CAMP is a camp camper management program meant to simplify the hassle of adding and managing your summer camp campers.</p>
    <router-link v-bind:to="{ name: 'camper', params: {camperCode: '200001'}}">Camper Example</router-link>
    <router-link v-bind:to="{ name: 'camperList'}">Camper List Example</router-link>
    <router-link v-bind:to="{ name:'familyList' }">Family List Example</router-link>
  </div>
</template>

<script>
import CamperService from '../services/CamperService.js'
import FamilyService from '../services/FamilyService.js'

export default {
  name: "home",
  created(){
    CamperService.getAllCampers()
      .then((response) => {
        this.$store.commit('SET_CAMPER_LIST', response.data);
        this.$store.commit('SET_CAMPER_AGE');
        this.$store.commit('SET_CAMPER_PAYMENT_STATUS');
        this.$store.commit('SET_CAMPER_MISSING_DATA');
        this.$store.commit('SET_CAMPER_ACTIVE_STATUS');
        FamilyService.getAllFamilies()
            .then(response => {
              this.$store.commit('SET_FAMILY_LIST', response.data);
              this.$store.commit('SET_CAMPER_FULL_NAME');
            })
            .catch(response => {
                console.error('Problem getting all families', response)
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
