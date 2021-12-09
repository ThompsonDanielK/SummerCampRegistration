<template>
  <article>
      <camper-info v-bind:camper="camper"></camper-info>
  </article>
</template>

<script>
import CamperInfo from '../components/CamperInfo.vue'
import CamperService from '../services/CamperService.js'

export default {
    data(){
        return{
            camper: {},
        }
    },
    components: {
        CamperInfo,
    },
    created(){
        CamperService.getCamper(this.$route.params.camperCode).then((response) => {
        console.log('Got camper', response.data)
        this.camper = response.data;
      })
      .catch(response =>
      {
        console.error('Problem getting camper', response)
      })
    },
}
</script>

<style>

</style>