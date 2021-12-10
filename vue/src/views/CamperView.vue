<template>
  <article :key="$store.state.campers.length">
      <camper-info v-bind:camper="camper"></camper-info>
  </article>
</template>

<script>
import CamperInfo from '../components/CamperInfo.vue'

export default {
    computed:{
        camper(){
            return this.$store.state.campers.find(c => c.camperCode == this.$route.params.camperCode)
        },
    },
    components: {
        CamperInfo,
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