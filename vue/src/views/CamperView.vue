<template>
  <article>
      <camper-info v-bind:camper="camper"></camper-info>
      <camper-change-log> </camper-change-log>
  </article>
</template>

<script>
import CamperChangeLog from '../components/CamperChangeLog.vue'
import CamperInfo from '../components/CamperInfo.vue'
import FamilyService from '../services/FamilyService.js'
import CamperService from '../services/CamperService.js'

export default {
    data()
    {
        return{
            campers: [],
            families: [],
        }
    },
    computed:{
        camper(){
            return this.campers.find(c => c.camperCode == this.$route.params.camperCode)
        },
    },
    components: {
        CamperInfo,
        CamperChangeLog   
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
          })
          .catch((response) => {
            console.error("Problem getting all families", response);
          });
      })
      .catch((response) => {
        console.error("Problem getting all campers", response);
      });

    },
}
</script>

<style lang="scss">
@import "../styles/colors.scss";
article{
  color: $textDark;
  font-size: 1.2rem;
  border: 2px solid $highlight;
  background-color: $secondary;
  border-radius: 20px;
  box-shadow: 2px 1px 1px 1px $textLight;
}
</style>