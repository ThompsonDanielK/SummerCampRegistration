<template>
  <article>
      <add-camper-form />
      <add-family-form v-show="showFamilyForm" />
      <div class ="camperButtons">
      <button type="submit" v-on:click.prevent="saveNewCamper()">Submit</button>
      <button type="button" v-on:click=" showAddForm = false; newcamper = {};">Cancel</button>
      <button type="button" v-on:click.prevent="showFamilyForm = true">Add New Family</button>
      </div>
  </article>
</template>

<script>
import AddCamperForm from '../components/AddCamperForm.vue'
import AddFamilyForm from '../components/AddFamilyForm.vue'
import CamperService from '../services/CamperService.js'
import FamilyService from '../services/FamilyService'

export default {
    data(){
        return{
            showFamilyForm: false,
        }
    },
    components: {
        AddCamperForm,
        AddFamilyForm
    },
    methods: {
        saveNewCamper() {
        if(this.showFamilyForm){
            FamilyService.addNewFamily(this.newFamily)
            .then
        }
        CamperService.addCamper(this.newCamper)
        .then(response => {
            console.log('New camper added', response.data);
            this.$router.push(`/camp/camper/${response.data.camperCode}`);
        })
        .catch(response => {
            console.error('Problem adding new camper', response);
        })
    },
    }
}
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

button {
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  border: 1px solid $highlight;
  margin: 10px;
  width: 20%;
}
.camperButtons{
    grid-area: buttons;
    display: flex;
    justify-content: center;
}
</style>