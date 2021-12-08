<template>
  <article>
      <add-camper-form />
      <add-family-form v-show="showFamilyForm" />
      <div class ="camperButtons">
      <button type="submit" v-on:click.prevent="saveNewCamper()" v-bind:disabled="!filledForm">Submit</button>
      <button type="button" v-on:click="newcamper = {}; $router.push('/CamperList')">Cancel</button>
      <button type="button" v-on:click.prevent="showFamilyForm = true" v-show="!showFamilyForm">Add New Family</button>
      </div>
  </article>
</template>

<script>
import AddCamperForm from '../components/AddCamperForm.vue'
import AddFamilyForm from '../components/AddFamilyForm.vue'
import CamperService from '../services/CamperService.js'

export default {
    data(){
        return{
            showFamilyForm: false,
        }
    },
    computed: {
        filledForm(){
            let camperFilled = (this.$store.state.CamperFamily.camper.firstName && this.$store.state.CamperFamily.camper.lastName && this.$store.state.CamperFamily.camper.dob)
            let familyFilled = ((!(this.showFamilyForm)) || (this.$store.state.CamperFamily.family.fullName && this.$store.state.CamperFamily.family.address && this.$store.state.CamperFamily.family.city &&
                   this.$store.state.CamperFamily.family.state && this.$store.state.CamperFamily.family.zip && this.$store.state.CamperFamily.family.phone && this.$store.state.CamperFamily.family.email))
            if(camperFilled && familyFilled)
            {
                return true;
            }
            return false;
        }
    },
    components: {
        AddCamperForm,
        AddFamilyForm
    },
    methods: {
        saveNewCamper() {
        CamperService.addCamperFamily(this.$store.state.CamperFamily)
        .then(response => {
            console.log('New camper added', response.data);
            this.$router.push(`/camp/camper/${response.data.camperCode}`);
            this.$store.state.CamperFamily = {};
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
  border: 2px solid $highlight;
  margin: 10px;
  width: 20%;
  text-shadow: 2px 1px 1px black;
  font-weight: bold;
  font-size: 1.2rem;
  font-family: 'Russo One', sans-serif;
}
.camperButtons{
    grid-area: buttons;
    display: flex;
    justify-content: center;
}
button:disabled{
  background-color: $secondary;
  color: black;
  text-shadow: 2px 1px 1px white;
}
</style>