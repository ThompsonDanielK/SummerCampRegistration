<template>
  <article>
      <h1>{{camper.Name}}</h1>
  </article>
</template>

<script>
import CamperService from "../services/CamperService.js"

export default {
    data(){
        return{
            camper: {
                id: '',
                name: '',
                
            }
        }
    },
created(){
    CamperService.getCamper(this.$route.params.camper.id)
    .then(response =>{

    })
}
}
</script>

<style>

</style>