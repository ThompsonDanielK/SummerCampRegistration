<template>
  <article>
      <add-camper-form v-bind:camper="CamperFamily.camper" />
      <add-family-form v-show="showFamilyForm" v-bind:family="CamperFamily.family" v-bind:disabled="CamperFamily.camper.familyId" />
      <div class ="camperButtons">
      <button type="submit" v-on:click.prevent="saveNewCamper()" v-bind:disabled="!filledForm">Submit</button>
      <router-link v-bind:to="{name: 'camperList'}" id="cancel">Cancel</router-link>
      <button type="button" v-on:click.prevent="showFamilyForm = true" v-show="!showFamilyForm">Add New Family</button>
      </div>
  </article>
</template>

<script>
import AddCamperForm from '../components/AddCamperForm.vue'
import AddFamilyForm from '../components/AddFamilyForm.vue'
import CamperService from '../services/CamperService.js'
import FamilyService from '../services/FamilyService.js'

export default {
    data(){
        return{
            showFamilyForm: false,
            families: [],
            CamperFamily: {
                camper: {},
                family: {},
            },
        }
    },
    computed: {
        filledForm(){
             let camperFilled = this.CamperFamily.camper.firstName && this.CamperFamily.camper.dob;
             let familyFilled = this.CamperFamily.family.fullName && this.CamperFamily.family.address && this.CamperFamily.family.city && this.CamperFamily.family.state && this.CamperFamily.family.zip && (this.CamperFamily.family.phoneNumber || this.CamperFamily.family.emailAddress)
             return camperFilled && (!this.showFamilyForm || familyFilled)
         },
    },
    components: {
        AddCamperForm,
        AddFamilyForm
    },
    methods: {
        saveNewCamper() {
            if(this.CamperFamily.camper.familyId)
            {
                this.CamperFamily.camper.familyId= parseInt(this.CamperFamily.camper.familyId)
            }
            CamperService.addCamperFamily(this.CamperFamily)
        .then(response => {
            console.log('New camper added', response.data);
            this.$store.state.CamperFamily = {};
            this.$router.push(`/camp/camper/${response.data.camperCode}`);
        })
        .catch(response => {
            console.error('Problem adding new camper', response);
        })
    },
    },
    created(){
        FamilyService.getAllFamilies()
        .then(response => {
            console.log('Got all families', response.data);
            this.families = response.data;
        })
        .catch(response => {
            console.error('Problem getting all families', response)
        });
    }
}
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

button, #cancel {
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  border: 2px solid $highlight;
  margin: 10px;
  width: 30%;
  text-shadow: 2px 1px 1px black;
  font-weight: bold;
  font-size: 1.2rem;
  text-decoration: none;
  text-align: center;
  font-family: 'Lora', serif;
}
.camperButtons{
    grid-area: buttons;
    display: flex;
    justify-content: center;
    align-items: center;
}
button:disabled{
  background-color: $secondary;
  color: black;
  text-shadow: 2px 1px 1px white;
}
</style>