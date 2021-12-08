<template>
  <!-- <img src="image"> -->
  <table>
    <tr id="firstName">
      <td>First Name:</td>
      <td>
        <div v-show="!showFirst" class="data">
          {{ this.camper.firstName }}
          <p v-show="newData.firstName">{{ this.newData.firstName }}</p>
        </div>
        <input type="text" v-model="newData.firstName" v-show="showFirst" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('firstName')" v-show="!showFirst">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('firstName')" v-show="showFirst">Save</button>
        <button type="button" id="firstCancel" v-on:click.prevent="newData.firstName = camper.firstName; showFirst = false" v-show="showFirst">Cancel</button>
      </td>
    </tr>
    <tr id="lastName">
      <td>Last Name:</td>
      <td>
        <div v-show="!showLast"  class="data">
          {{ this.camper.lastName }}
          <p v-show="newData.lastName">{{ this.newData.lastName }}</p>
        </div>
        <input type="text" v-model="newData.lastName" v-show="showLast" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('lastName')" v-show="!showLast">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('lastName')" v-show="showLast">Save</button>
        <button type="button" id="lastCancel" v-on:click.prevent="newData.lastName = camper.lastName; showLast = false" v-show="showLast">Cancel</button>
      </td>
    </tr>
    <tr id="dob">
      <td>Dat of Birth:</td>
      <td>
        <div v-show="!showDOB"  class="data">
          {{ this.camper.dob }}
          <p v-show="newData.dob">{{ this.newData.dob }}</p>
        </div>
        <input type="text" v-model="newData.dob" v-show="showDOB" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('dob')" v-show="!showDOB">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('dob')" v-show="showDOB">Save</button>
        <button type="button" id="dobCancel" v-on:click.prevent="newData.dob = camper.dob; showDOB = false" v-show="showDOB">Cancel</button>
      </td>
    </tr>
    <tr id="allergies">
      <td>Allergies:</td>
      <td>
        <ul class="data">
          <li v-show="!showAllergies" v-for="line in this.camper.allergies" v-bind:key="line">
            {{ line }}
          </li>
          <li v-show="!showAllergies && newData.allergies" v-for="line in this.newData.allergies" v-bind:key="line">
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
        <button type="button" id="allergiesCancel" v-on:click.prevent="newData.allergies = camper.allergies; showAllergies = false" v-show="showAllergies">Cancel</button>
      </td>
    </tr>
     <tr id="medications">
      <td>Medications:</td>
      <td>
        <ul class="data">
          <li v-show="!showMedications" v-for="line in this.camper.medications" v-bind:key="line">
            {{ line }}
          </li>
          <li v-show="!showMedications && newData.medications" v-for="line in this.newData.medications" v-bind:key="line">
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
        <button type="button" id="medicationsCancel" v-on:click.prevent="newData.medications = camper.medications; showMedications = false" v-show="showMedications">Cancel</button>
      </td>
    </tr>
    <tr id="info">
      <td>Special Needs:</td>
      <td>
        <ul class="data">
          <li v-show="!showSpecial" v-for="line in this.camper.specialNeeds" v-bind:key="line">
            {{ line }}
          </li>
          <li v-show="!showSpecial && newData.specialNeeds" v-for="line in this.newData.specialNeeds" v-bind:key="line">
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
        <button type="button" id="specialCancel" v-on:click.prevent="newData.specialNeeds = camper.specialNeeds; showSpecial = false" v-show="showSpecial">Cancel</button>
      </td>
    </tr>
    <tr id="familyId">
      <td>Family: </td>
      <td>
        <div v-show="!showFamily"  class="data">
          {{ this.camper.familyId }}
          <p v-show="newData.familyId">{{ this.newData.familyId }}</p>
        </div>
        <input type="text" v-model="newData.familyId" v-show="showFamily" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('familyId')" v-show="!showFamily">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('familyId')" v-show="showFamily">Save</button>
        <button type="button" id="lastCancel" v-on:click.prevent="newData.familyId = camper.familyId; showFamily = false" v-show="showFamily">Cancel</button>
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
      newData: {},
      camperToAdd: {},
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
        this.request.camperToAdd.fieldToBeChanged = formName;
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
          this.camperToAdd.newValue = this.newData.allergies;
          this.camperToAdd.oldValue = this.camper.allergies;
          this.showAllergies = false;
          break;
        case "medications":
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
      this.setcamper();
      CamperService.updateCamper(this.camper)
      .then(response => {
        console.log('Updated camper info', response.data);
        this.logChanges();
        this.$router.push('/');
      })
      .catch(response => {
        console.error('Cannot finalize changes', response);
      })
    },
    logChanges(){
      this.request.action = 'update'
       CamperService.logChanges(this.request)
        .then(response => {
          console.log('Logged changes', response.data);
        })
        .catch(response => {
          console.warn('Problem logging changes', response);
        })
    },
    setCamper(){
      if(this.newData.firstName)
      {
        this.camper.firstName = this.newData.firstName;
      }
      if(this.newData.lastName)
      {
        this.camper.larstName = this.newData.lastName;
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
    //   CamperService.getCamper(this.$route.params.camperCode).then((response) => {
    //     console.log('Got camper', response.data)
    //     this.newData = response.data;
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