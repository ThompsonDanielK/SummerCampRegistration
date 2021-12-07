<template>
  <!-- <img src="image"> -->
  <table>
    <tr id="firstName">
      <td>First Name:</td>
      <td>
        <div v-show="!showFirst" class="data">
          {{ this.camper.firstName }}
        </div>
        <input type="text" v-model="newValue.firstName" v-show="showFirst" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('firstName')" v-show="!showFirst">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('firstName')" v-show="showFirst">Save</button>
        <button type="button" id="firstCancel" v-on:click.prevent="newValue.firstName = camper.firstName; showFirst = false" v-show="showFirst">Cancel</button>
      </td>
    </tr>
    <tr id="lastName">
      <td>Last Name:</td>
      <td>
        <div v-show="!showLast"  class="data">
          {{ this.camper.lastName }}
        </div>
        <input type="text" v-model="newValue.lastName" v-show="showLast" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('lastName')" v-show="!showLast">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('lastName')" v-show="showLast">Save</button>
        <button type="button" id="lastCancel" v-on:click.prevent="newValue.lastName = camper.lastName; showLast = false" v-show="showLast">Cancel</button>
      </td>
    </tr>
    <tr id="allergies">
      <td>Allergies:</td>
      <td>
        <ul class="data">
          <li v-show="!showAllergies" v-for="line in this.camper.allergies" v-bind:key="line">
            {{ line }}
          </li>
          <div v-show="showAllergies">
          <small>Seperate items by commas (,) with no spaces</small>
          <input type="text" v-model="newValue.allergies" />
          </div>
        </ul>
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('allergies')" v-show="!showAllergies">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('allergies')" v-show="showAllergies">Save</button>
        <button type="button" id="allergiesCancel" v-on:click.prevent="newValue.allergies = camper.allergies; showAllergies = false" v-show="showAllergies">Cancel</button>
      </td>
    </tr>
     <tr id="medications">
      <td>Medications:</td>
      <td>
        <ul class="data">
          <li v-show="!showMedications" v-for="line in this.camper.medications" v-bind:key="line">
            {{ line }}
          </li>
          <div v-show="showMedications">
          <small>Seperate items by commas (,) with no spaces</small>
          <input type="text" v-model="newValue.medications" />
          </div>
        </ul>
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('medications')" v-show="!showMedications">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('medications')" v-show="showMedications">Save</button>
        <button type="button" id="medicationsCancel" v-on:click.prevent="newValue.medications = camper.medications; showMedications = false" v-show="showMedications">Cancel</button>
      </td>
    </tr>
    <tr id="info">
      <td>Special Needs:</td>
      <td>
        <ul class="data">
          <li v-show="!showSpecial" v-for="line in this.camper.specialInfo" v-bind:key="line">
            {{ line }}
          </li>
          <div v-show="showSpecial">
          <small>Seperate items by commas (,) with no spaces</small>
          <input type="text" v-model="newValue.specialInfo" />
          </div>
        </ul>
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('specialInfo')" v-show="!showSpecial">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('specialInfo')" v-show="showSpecial">Save</button>
        <button type="button" id="specialCancel" v-on:click.prevent="newValue.specialInfo = camper.specialInfo; showSpecial = false" v-show="showSpecial">Cancel</button>
      </td>
    </tr>
    <tr id="family">
      <td>Family: </td>
      <td>
        <div v-show="!showFamily"  class="data">
          {{ this.camper.familyId }}
        </div>
        <input type="text" v-model="newValue.familyId" v-show="showFamily" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('family')" v-show="!showFamily">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('family')" v-show="showFamily">Save</button>
        <button type="button" id="lastCancel" v-on:click.prevent="newValue.familyId = camper.familyId; showFamily = false" v-show="showFamily">Cancel</button>
      </td>
    </tr>
  </table>
</template>

<script>
import CamperService from "../services/CamperService.js";

export default {
  data() {
    return {
      camper: {},
      newValue: {},
      changes: {},
      showFirst: false,
      showLast: false,
      showAllergies: false,
      showMedications: false,
      showFamily: false,
      showSpecial: false,
      states: ["AL", "AK", "AR", "AZ", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA",
               "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH",
               "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VA",
               "VT", "WA", "WI", "WV", "WY"
              ],
            };
  },
  methods: {
    showHideForm(formName) {
      switch (formName) {
        case "firstName":
          this.showFirst = true;
          break;
        case "lastName":
          this.showLast = true;
          break;
        case "allergies":
          this.showAllergies = true;
          break;
        case "medications":
          this.showMedications = true;
          break;
        case "family":
          this.showFamily = true;
          break;
        case "specialInfo":
          this.showSpecial = true;
          break;
      }
    },
    saveChange(formName) {
        this.changes.camperCode = this.newValue.id;
      switch (formName) {
        case "firstName":
          this.changes.firstName = this.newValue.firstName;
          this.camper.firstName= this.newValue.firstName;
          this.showFirst = false;
          break;
        case "lastName":
          this.changes.lastName = this.newValue.lastName;
          this.camper.lastName= this.newValue.lastName;
          this.showLast = false;
          break;
        case "allergies":
          this.changes.allergies = this.newValue.allergies;
          this.camper.allergies = this.newValue.allergies;
          this.showAllergies = false;
          break;
        case "medications":
          this.changes.medications = this.newValue.medications;
          this.camper.medications = this.newValue.medications;
          this.showMedications = false;
          break;
        case "family":
          this.changes.family = this.newValue.family;
          this.camper.family = this.newValue.family;
          this.showFamily = false;
          break;
        case "specialInfo":
          this.newValue.specialInfo = this.newValue.specialInfo.split(',');
          this.camper.specialInfo = this.newValue.specialInfo;
          this.changes.specialInfo = this.newValue.specialInfo;
          this.showSpecial = false;
          break;
      }
    },
    finalizeChanges(){
      this.changes.action = 'update'
      CamperService.updateCamper(this.changes)
      .then(response => {
        console.log('Updated camper info', response.data);
        CamperService.logChanges(this.changes)
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
    //   CamperService.getCamper(this.$route.params.camperCode).then((response) => {
    //     console.log('Got camper', response.data)
    //     this.newValue = response.data;
    //     this.camper = response.data;
    //   })
    //   .catch(response =>
    //   {
    //     console.error('Problem getting camper', response)
    //   })
    // },
    this.camper = this.$store.state.campers.find( c => c.camperCode = this.$route.params.camperCode)
    }
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
button:disabled{
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