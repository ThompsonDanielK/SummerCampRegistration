<template>
  <section>
      <h1>New Camper</h1>
      <form class="form-group">
      <div id="firstName">
      <label for="firstName">First Name:</label>
      <input v-model="newCamper.firstName" name="firstName" type="text" />
      </div>
      <div id="lastName">
      <label for="lastName">Last Name:</label>
      <input v-model="newCamper.lastName" name="lastName" type="text" />
      </div>
      <div id="allergies">
      <label for="allergies">Allergies:</label>
      <input v-model="newCamper.allergies" name="allergies" type="text" />
      </div>
      <div id="medications">
      <label for="medications">Medications:</label>
      <input v-model="newCamper.medications" name="medications" type="text" />
      </div>
      <div id="specialNeeds">
      <label for="specialNeeds">Special Needs:</label>
      <input v-model="newCamper.specialNeeds" name="specialNeeds" type="text" />
      </div>
      <div  id="family">
      <label for="familyId">Family:</label>
      <select v-model="newCamper.familyId" name="familyId" v-for="f in families" v-bind:key="f.familyId">
          <option>{{f.familyId}} -- {{f.lastName}}</option>
      </select>
      </div>
    </form>
  </section>
</template>

<script>
import FamilyService from '../services/FamilyService.js'

export default {
    data() {
        return{
            newCamper: {},
            families: [],
        }
    },
    computed: {
    filledForm() {
      return (
        this.newCamper.firstName &&
        this.newCamper.lastName &&
        this.newCamper.familyId
      );
    }
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

section{
  display: flex;
  flex-direction: column;
  align-items: center;
  margin: 10px;
  background-color: $secondary;
  padding: 10px;
  border-radius: 10px;
  border: 2px solid black;
}
h1{
  margin: 10px;
}
.form-group {
  width: 100%;
  display: grid;
  grid-template-areas: 
  'firstName lastName'
  'allergies medications'
  'specialNeeds familyList';
}
#firstName{
    grid-area: firstName;
}
#lastName{
    grid-area: lastName;
}
#allergies{
    grid-area: allergies;
}
#medications{
    grid-area: medications;
}
#family{
    grid-area: familyList;
}
#specialNeeds{
    grid-area: specialNeeds;
}
div{
  display: flex;
  justify-content: space-between;
  padding: 10px;
}
input,
select {
  margin: 10px;
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  padding-left: 10px;
  border: 1px solid $highlight;
  width: 50%;
}
</style>