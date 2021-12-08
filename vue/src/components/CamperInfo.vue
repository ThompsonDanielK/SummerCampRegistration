<template>
<section>
  <table>
    <tr id="firstName">
      <td>First Name:</td>
      <td>
        <div v-show="!showFirst" class="data">
          {{ this.camper.firstName }}
          <p v-show="newData.firstName" class="newValue">{{ this.newData.firstName }}</p>
        </div>
        <input type="text" v-model="newData.firstName" v-show="showFirst" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('firstName')" v-show="!showFirst">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('firstName')" v-show="showFirst">Save</button>
        <button type="button" id="firstCancel" v-on:click.prevent="newData.firstName = ''; showFirst = false" v-show="showFirst">Cancel</button>
      </td>
    </tr>
    <tr id="lastName">
      <td>Last Name:</td>
      <td>
        <div v-show="!showLast"  class="data">
          {{ this.camper.lastName }}
          <p v-show="newData.lastName" class="newValue">{{ this.newData.lastName }}</p>
        </div>
        <input type="text" v-model="newData.lastName" v-show="showLast" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('lastName')" v-show="!showLast">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('lastName')" v-show="showLast">Save</button>
        <button type="button" id="lastCancel" v-on:click.prevent="newData.lastName = ''; showLast = false" v-show="showLast">Cancel</button>
      </td>
    </tr>
    <tr id="age">
      <td>Age:</td>
      <td>{{ this.age }}</td>
      <td></td>
    </tr>
    <tr id="dob">
      <td>Date of Birth:</td>
      <td>
        <div v-show="!showDOB"  class="data">
          {{ this.convertedDateTime }}
          <p v-show="newData.dob" class="newValue">{{ this.newConvertedDateTime }}</p>
        </div>
        <input type="date" v-model="newData.dob" v-show="showDOB" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('dob')" v-show="!showDOB">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('dob')" v-show="showDOB">Save</button>
        <button type="button" id="dobCancel" v-on:click.prevent="newData.dob = ''; showDOB = false" v-show="showDOB">Cancel</button>
      </td>
    </tr>
    <tr id="familyId">
      <td>Family: </td>
      <td>
        <div v-show="!showFamily"  class="data">
          {{ this.camper.familyId }}
          <p v-show="newData.familyId" class="newValue">{{ this.newData.familyId }}</p>
        </div>
        <input type="text" v-model="newData.familyId" v-show="showFamily" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('familyId')" v-show="!showFamily">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('familyId')" v-show="showFamily">Save</button>
        <button type="button" id="lastCancel" v-on:click.prevent="newData.familyId = ''; showFamily = false" v-show="showFamily">Cancel</button>
      </td>
    </tr>
    <tr id="allergies">
      <td>Allergies:</td>
      <td>
        <ul class="data">
          <li v-show="!showAllergies" v-for="line in this.camper.allergies" v-bind:key="line">
            {{ line }}
          </li>
          <li v-show="!showAllergies && newData.allergies" v-for="line in this.newData.allergies" v-bind:key="line" class="newValue">
            {{ line }}
          </li>
          <div v-show="showAllergies">
          <small>Seperate items by commas (,) with no spaces</small>
          <input type="text" v-model="newData.allergies" />
          </div>
        </ul>
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('allergies')" v-show="!showAllergies">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('allergies')" v-show="showAllergies">Save</button>
        <button type="button" id="allergiesCancel" v-on:click.prevent="newData.allergies = ''; showAllergies = false" v-show="showAllergies">Cancel</button>
      </td>
    </tr>
     <tr id="medications">
      <td>Medications:</td>
      <td>
        <ul class="data">
          <li v-show="!showMedications" v-for="line in this.camper.medications" v-bind:key="line">
            {{ line }}
          </li>
          <li v-show="!showMedications && newData.medications" v-for="line in this.newData.medications" v-bind:key="line" class="newValue">
            {{ line }}
          </li>
          <div v-show="showMedications">
          <small>Seperate items by commas (,) with no spaces</small>
          <input type="text" v-model="newData.medications" />
          </div>
        </ul>
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('medications')" v-show="!showMedications">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('medications')" v-show="showMedications">Save</button>
        <button type="button" id="medicationsCancel" v-on:click.prevent="newData.medications = ''; showMedications = false" v-show="showMedications">Cancel</button>
      </td>
    </tr>
    <tr id="info">
      <td>Special Needs:</td>
      <td>
        <ul class="data">
          <li v-show="!showSpecial" v-for="line in this.camper.specialNeeds" v-bind:key="line">
            {{ line }}
          </li>
          <li v-show="!showSpecial && newData.specialNeeds" v-for="line in this.newData.specialNeeds" v-bind:key="line" class="newValue">
            {{ line }}
          </li>
          <div v-show="showSpecial">
          <small>Seperate items by commas (,) with no spaces</small>
          <input type="text" v-model="newData.specialNeeds" />
          </div>
        </ul>
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('specialNeeds')" v-show="!showSpecial">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('specialNeeds')" v-show="showSpecial">Save</button>
        <button type="button" id="specialCancel" v-on:click.prevent="newData.specialNeeds = ''; showSpecial = false" v-show="showSpecial">Cancel</button>
      </td>
    </tr>
  </table>
  <button type="submit" v-on:click.prevent="finalizeChanges()" v-bind:disabled="!this.newData">Submit Changes</button>
</section>
</template>

<script>
import CamperService from "../services/CamperService.js";

export default {
  data() {
    return {
      camper: {},
      newData: {},
      camperToAdd: {
        fieldToBeChanged: '',
        oldValue: '',
        newValue: '',
      },
      request: {
        changes: []
      },
      showFirst: false,
      showLast: false,
      showAllergies: false,
      showMedications: false,
      showDOB: false,
      showFamily: false,
      showSpecial: false,
      states: ["AL", "AK", "AR", "AZ", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA",
               "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH",
               "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VA",
               "VT", "WA", "WI", "WV", "WY"
              ],
            };
  },
  computed:{
    convertedDateTime() {
      let dob = new Date(this.camper.dob);
      let month = dob.getMonth() + 1;
      let day = dob.getDate();
      let year = dob.getFullYear(); 
      let formattedDob = `${month}/${day}/${year}`
      return formattedDob;
    },
    newConvertedDateTime() {
      if(this.newData.dob)
      {
        let dob = new Date(this.newData.dob);
        let month = dob.getMonth() + 1;
        let day = dob.getDate();
        let year = dob.getFullYear(); 
        let formattedDob = `${month}/${day}/${year}`
        return formattedDob;
      }
      return '';
    },
    age() {
      let birthYear = new Date(this.camper.dob).getFullYear()
      let currentYear = new Date().getFullYear();
      let age = currentYear - birthYear;
      return age;
    }
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
          case "dob":
          this.showDOB = true;
          break;
        case "allergies":
          this.showAllergies = true;
          break;
        case "medications":
          this.showMedications = true;
          break;
        case "familyId":
          this.showFamily = true;
          break;
        case "specialNeeds":
          this.showSpecial = true;
          break;
      }
    },
    saveChange(formName) {
        this.request.camperCode = this.camper.camperCode;
        this.camperToAdd.fieldToBeChanged = formName;
      switch (formName) {
        case "firstName":
          this.camperToAdd.newValue = this.newData.firstName;
          this.camperToAdd.oldValue = this.camper.firstName;
          this.showFirst = false;
          break;
        case "lastName":
          this.camperToAdd.newValue = this.newData.lastName;
          this.camperToAdd.oldValue = this.camper.oldValue;
          this.showLast = false;
          break;
          case "dob":
          this.camperToAdd.newValue = this.newData.dob;
          this.camperToAdd.oldValue = this.camper.dob;
          this.showDOB = false;
          break;
        case "allergies":
          this.newData.allergies = this.newData.allergies.split(',')
          this.camperToAdd.newValue = this.newData.allergies;
          this.camperToAdd.oldValue = this.camper.allergies;
          this.showAllergies = false;
          break;
        case "medications":
          this.newData.medications = this.newData.medications.split(',')
          this.camperToAdd.newValue = this.newData.medications;
          this.camperToAdd.oldValue = this.camper.medications;
          this.showMedications = false;
          break;
        case "familyId":
          this.camperToAdd.newValue = this.newData.familyId;
          this.camperToAdd.oldValue = this.camper.familyId;
          this.showFamily = false;
          break;
        case "specialNeeds":
          this.newData.specialNeeds = this.newData.specialNeeds.split(',');
          this.camperToAdd.newValue = this.newData.specialNeeds;
          this.camperToAdd.oldValue = this.camper.specialNeeds;
          this.showSpecial = false;
          break;
      }
          this.request.changes.push(this.camperToAdd);
          this.camperToAdd = {};
    },
    finalizeChanges(){
      this.setCamper();
      CamperService.updateCamper(this.camper)
      .then(response => {
        console.log('Updated camper info', response.data);
        // this.logChanges();
        this.$router.push('/');
      })
      .catch(response => {
        console.error('Cannot finalize changes', response);
      })
    },
    // logChanges(){
    //   this.request.action = 'update'
    //    CamperService.logChanges(this.request)
    //     .then(response => {
    //       console.log('Logged changes', response.data);
    //     })
    //     .catch(response => {
    //       console.warn('Problem logging changes', response);
    //     })
    // },
    setCamper(){
      if(this.newData.firstName)
      {
        this.camper.firstName = this.newData.firstName;
      }
      if(this.newData.lastName)
      {
        this.camper.lastName = this.newData.lastName;
      }
      if(this.newData.allergies)
      {
        this.camper.allergies = this.newData.allergies;
      }
      if(this.newData.medications)
      {
        this.camper.medications = this.newData.medications;
      }
      if(this.newData.familyId)
      {
        this.camper.familyId = this.newData.familyId;
      }
      if(this.newData.specialNeeds)
      {
        this.camper.specialNeeds = this.newData.specialNeeds;
      }
    }
  },
    created() {
      CamperService.getCamper(this.$route.params.camperCode).then((response) => {
        console.log('Got camper', response.data)
        this.camper = response.data;
      })
      .catch(response =>
      {
        console.error('Problem getting camper', response)
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
  border: 2px solid $highlight;
  text-shadow: 2px 1px 1px black;
  font-size: 1rem;
  font-family: 'Russo One', sans-serif;
}
button:disabled{
  background-color: $secondary;
  color: black;
}
table td .data{
    padding-left: 1%;
}
input, select, textarea{
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
.newValue{
  color: $highlight;
}
button[type='submit']
{
  margin: 1% 35%;
  width: 30%;
}
section{
  display: flex;
  flex-direction: column;
  justify-content: center;
}
</style>