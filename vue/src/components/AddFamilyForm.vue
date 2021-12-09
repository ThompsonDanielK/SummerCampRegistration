<template>
  <section>
      <h1>New Family</h1>
      <form class="form-group">
      <div id="fullName">
      <label for="fullName">Full Name:</label>
      <input v-model="$store.state.CamperFamily.family.fullName" name="fullName" type="text" />
      </div>
      <div id="streetAddress">
      <label for="streetAddress">Street Address:</label>
      <input v-model="$store.state.CamperFamily.family.address" name="streetAddress" type="text" />
      </div>
      <div id="city">
      <label for="city">City:</label>
      <input v-model="$store.state.CamperFamily.family.city" name="city" type="text" />
      </div>
      <div id="state">
      <label for="state">State:</label>
      <select name="state" v-model="$store.state.CamperFamily.family.state">
          <option v-for="state in states" v-bind:key="state">
            {{ state }}
          </option>
        </select>
      </div>
      <div id="zip">
      <label for="zip">Zip Code:</label>
      <input v-model.number="$store.state.CamperFamily.family.zip" name="zip" type="text" />
      </div>
      <div id="phone">
      <label for="phone">Phone Number:</label>
      <input v-model="$store.state.CamperFamily.family.phoneNumber" name="phone" type="text" />
      </div>
      <div id="email">
      <label for="email">Email:</label>
      <input v-model.number="$store.state.CamperFamily.family.emailAddress" name="email" type="text" />
      </div>
    </form>
  </section>
</template>

<script>
import FamilyService from '../services/FamilyService.js'

export default {
    data() {
        return{
            families: [],
    states: ["AL", "AK", "AR", "AZ", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA",
               "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH",
               "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VA",
               "VT", "WA", "WI", "WV", "WY"],
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
  font-weight: bold;
  font-size: 1.2rem;
  font-family: 'Russo One', sans-serif;
}
</style>