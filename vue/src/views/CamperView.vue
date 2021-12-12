<template>
  <article :key="$store.state.campers.length">
      <camper-info v-bind:camper="camper"></camper-info>
      <camper-change-log> </camper-change-log>
  </article>
</template>

<script>
import CamperChangeLog from '../components/CamperChangeLog.vue'
import CamperInfo from '../components/CamperInfo.vue'

export default {
    computed:{
        camper(){
            return this.$store.state.campers.find(c => c.camperCode == this.$route.params.camperCode)
        },
    },
    components: {
        CamperInfo,
        CamperChangeLog   
    },
    created() {
        this.$store.commit('SET_CAMPER_LIST')
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