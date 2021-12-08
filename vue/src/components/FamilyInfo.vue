<template>
  <table>
    <tr id="fullName">
      <td>Full Name:</td>
      <td>
        <div v-show="!showFull" class="data">
          {{ this.family.fullName }}
        </div>
        <input type="text" v-model="newData.fullName" v-show="showFull" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('fullName')" v-show="!showFull">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('fullName')" v-show="showFull">Save</button>
        <button type="button" id="cityCancel" v-on:click.prevent="newData.fullName = family.fullName; showFull = false" v-show="showFull">Cancel</button>
      </td>
    </tr>
    <tr id="city">
      <td>City:</td>
      <td>
        <div v-show="!showCity" class="data">
          {{ this.family.city }}
        </div>
        <input type="text" v-model="newData.city" v-show="showCity" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('city')" v-show="!showCity">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('city')" v-show="showCity">Save</button>
        <button type="button" id="cityCancel" v-on:click.prevent="newData.city = family.city; showCity = false" v-show="showCity">Cancel</button>
      </td>
    </tr>
    <tr id="state">
      <td>State:</td>
      <td>
        <div v-show="!showState" class="data">
          {{ this.family.state }}
        </div>
        <select v-model="newData.state" v-show="showState">
          <option v-for="state in states" v-bind:key="state">
            {{ state }}
          </option>
        </select>
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('state')" v-show="!showState">Edit</button>
        <button type="submit" v-on:click.prevent="saveChange('state')" v-show="showState">Save</button>
        <button type="button" id="stateCancel" v-on:click.prevent="newData.state = family.state; showState = false" v-show="showState">Cancel</button>
      </td>
    </tr>
    <tr id="zip">
      <td>Zip:</td>
      <td>
        <div v-show="!showZip" class="data">
          {{ this.family.zip }}
        </div>
        <input type="text" v-model.number="newData.zip" v-show="showZip" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('zip')" v-show="!showZip">Edit</button>
        <button type="submit" v-on:click.prevent="saveChange('zip')" v-show="showZip">Save</button>
        <button type="button" id="zipCancel" v-on:click.prevent="newData.zip = family.zip; showZip = false" v-show="showZip">Cancel</button>
      </td>
    </tr>
    <tr id="phoneNumber">
      <td>Phone Number:</td>
      <td>
        <div v-show="!showPhone" class="data">
          {{ this.family.phoneNumber }}
        </div>
        <input type="text" v-model.number="newData.phoneNumber" v-show="showPhone" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('phoneNumber')" v-show="!showPhone">Edit</button>
        <button type="submit" v-on:click.prevent="saveChange('phoneNumber')" v-show="showPhone">Save</button>
        <button type="button" id="zipCancel" v-on:click.prevent="newData.phoneNumber = family.phoneNumber; showPhone = false" v-show="showPhone">Cancel</button>
      </td>
    </tr>
    </table>
</template>

<script>
import FamilyService from '../services/FamilyService.js'

export default {
  data(){
    return{
      family: {},
      showFull: false,
      showEmail: false,
      showCity: false,
      showState: false,
      showZip: false,
      showPhone: false,
      newData: {},
      states: ["AL", "AK", "AR", "AZ", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA",
               "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH",
               "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VA",
               "VT", "WA", "WI", "WV", "WY"],
    }
  },
  methods:{
    showHideForm(formName) {
      switch (formName) {
        case "fullName":
          this.showFull = true;
          break;
        case "email":
          this.showEmail = true;
          break;
        case "city":
          this.showCity = true;
          break;
        case "state":
          this.showState = true;
          break;
        case "zip":
          this.showZip = true;
          break;
        case "phoneNumber":
          this.Phonecial = true;
          break;
      }
    },
    saveChange(formName) {
      this.changes.familyId = this.newData.id;
      switch (formName) {
        case "fullName":
          this.changes.fullName = this.newData.fullName;
          this.family.fullName= this.newData.fullName;
          this.showFull = false;
          break;
        case "city":
          this.changes.city = this.newData.city;
          this.family.city = this.newData.city;
          this.showCity = false;
          break;
        case "state":
          this.changes.state = this.newData.state;
          this.family.state = this.newData.state;
          this.showState = false;
          break;
        case "zip":
          this.changes.zip = this.newData.zip;
          this.family.zip = this.newData.zip;
          this.showZip = false;
          break;
        case "phoneNumber":
          this.family.phoneNumber = this.newData.phoneNumber;
          this.changes.phoneNumber = this.newData.phoneNumber;
          this.showPhone = false;
          break;
        case "email":
          this.changes.email = this.newData.email;
          this.family.email= this.newData.email;
          this.showEmail = false;
          break;
      }
    },
    finalizeChanges(){
      FamilyService.updateCamper(this.family)
      .then(response => {
        console.log('Updated family info', response.data);
        FamilyService.logChanges(this.changes)
        .then(response => {
          console.log('Logged changes', response.data);
          this.changes = {};
        })
        .catch(response => {
          console.warn('Problem logging changes', response);
        })
      })
      .catch(response => {
        console.error('Cannot finalize changes', response);
      })
      }
    },
    created() {
      FamilyService.getFamily(this.$route.params.familyId).then((response) => {
        console.log('Got family', response.data)
        this.family = response.data;
      })
      .catch(response =>
      {
        console.error('Problem getting family', response)
      })
    },
};
</script>

<style lang="scss" scoped>
@import "../styles/colors.scss";

table {
  margin-top: 20px;
  padding: 10px;
  color: $textDark;
  font-size: 1.4rem;
  border: 2px solid $highlight;
  background-color: $textLight;
  width: 100%;
  border-radius: 20px;
}
button {
  background-color: $textDark;
  border: none;
  color: $textLight;
  margin-left: 15px;
  border-radius: 5px;
}
:disabled{
  background-color: $secondary;
  color: black;
}
table tr {
  padding: 10%;
  margin-top: 10px;
}
table td {
  margin-bottom: 10px;
}
table td .data{
    padding-left: 1%;
}
table input, select, textarea{
  font-family: 'Russo One', sans-serif;
  border: 1px dotted $highlight;
  border-radius: 10px;
  padding: 5px;
  background-color: $textDark;
  color: $textLight;
  width: 60%;
}
input::-webkit-input-placeholder{
  color: white;
}
</style>