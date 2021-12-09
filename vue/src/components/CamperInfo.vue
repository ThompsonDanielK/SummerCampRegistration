<template>
<section>
  <table>
    <tr id="firstName">
      <td>First Name:</td>
      <td>
        <div v-show="!showFirst" class="data">
          <p v-if="!newData.firstName">{{ this.camper.firstName }}</p>
          <p v-if="newData.firstName" class="newValue">{{ this.newData.firstName }}</p>
        </div>
        <input type="text" v-model="newData.firstName" v-show="showFirst" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showFirst = true" v-show="!showFirst">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('firstName')" v-show="showFirst">Save</button>
        <button type="button" v-on:click.prevent="newData.firstName = ''; showFirst = false" v-show="showFirst">Cancel</button>
      </td>
    </tr>
    <tr id="lastName">
      <td>Last Name:</td>
      <td>
        <div v-show="!showLast"  class="data">
          <p v-if="!newData.lastName"> {{ this.camper.lastName }}</p>
          <p v-if="newData.lastName" class="newValue">{{ this.newData.lastName }}</p>
        </div>
        <input type="text" v-model="newData.lastName" v-show="showLast" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showLast = true" v-show="!showLast">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('lastName')" v-show="showLast">Save</button>
        <button type="button" v-on:click.prevent="newData.lastName = ''; showLast = false" v-show="showLast">Cancel</button>
      </td>
    </tr>
    <tr id="registrar">
      <td>Registrar:</td>
      <td>
        <div v-show="!showRegistrar"  class="data">
          <p v-if="!newData.registrar"> {{ this.camper.registrar }}</p>
          <p v-if="newData.registrar" class="newValue">{{ this.newData.registrar }}</p>
        </div>
        <input type="text" v-model="newData.registrar" v-show="showRegistrar" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showRegistrar = true" v-show="!showRegistrar">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('registrar')" v-show="showRegistrar">Save</button>
        <button type="button" v-on:click.prevent="newData.registrar = ''; showRegistrar = false" v-show="showRegistrar">Cancel</button>
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
          <p v-if="!newData.dob">{{ this.convertedDateTime }}</p>
          <p v-if="newData.dob" class="newValue">{{ this.newConvertedDateTime }}</p>
        </div>
        <input type="date" v-model="newData.dob" v-show="showDOB" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showDOB = true" v-show="!showDOB">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('dob')" v-show="showDOB">Save</button>
        <button type="button" v-on:click.prevent="newData.dob = ''; showDOB = false" v-show="showDOB">Cancel</button>
      </td>
    </tr>
    <tr id="payment">
      <td>Payment Status:</td>
      <td>
        <div class="data">
        <p v-if="!showPayment && !newData.paymentStatus">{{ this.camper.paymentStatus }}</p>
        <p v-if="newData.paymentStatus && !showPayment" class="newValue">{{ this.newData.paymentStatus }}</p>
        </div>
        <div v-show="showPayment">
        <label for="unpaid">Unpaid</label>
        <input type="radio" id="unpaid" name="unpaid" value="Unpaid" v-model="newData.paymentStatus" checked>
        <label for="paid">Paid</label>
        <input type="radio" id="paid" name="paid" value="Paid" v-model="newData.paymentStatus">
      </div>
      </td>
      <td>
        <button type="button" v-on:click.prevent="showPayment = true;" v-show="!showPayment">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('payment')" v-show="showPayment">Save</button>
        <button type="button" v-on:click.prevent="newData.paymentStatus = ''; showPayment = false" v-show="showPayment">Cancel</button>
      </td>
    </tr>
    <tr id="familyId">
      <td>Family: </td>
      <td>
        <div v-show="!showFamily"  class="data">
          <p v-if="!newData.familyId">{{ this.camper.familyId }}</p>
          <p v-if="newData.familyId" class="newValue">{{ this.newData.familyId }}</p>
        </div>
        <input type="text" v-model="newData.familyId" v-show="showFamily" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showFamily = true" v-show="!showFamily">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('familyId')" v-show="showFamily">Save</button>
        <button type="button" v-on:click.prevent="newData.familyId = ''; showFamily = false" v-show="showFamily">Cancel</button>
      </td>
    </tr>
    <tr id="allergies">
      <td>Allergies:</td>
      <td>
        <ul class="data">
          <li v-show="!showAllergies && !newData.allergies && camper.allergies && camper.allergies != 'None'" v-for="line in this.camper.allergies" v-bind:key="line">
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
        <p v-show="!showAllergies && !newData.allergies && (!camper.allergies || camper.allergies === 'None')">None</p>
      </td>
      <td>
        <button type="button" v-on:click.prevent="showAllergies = true" v-show="!showAllergies">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('allergies')" v-show="showAllergies">Save</button>
        <button type="button" v-on:click.prevent="newData.allergies = ''; showAllergies = false" v-show="showAllergies">Cancel</button>
      </td>
    </tr>
     <tr id="medications">
      <td>Medications:</td>
      <td>
        <ul class="data">
          <li v-show="!showMedications && !newData.medications && camper.medications && !camper.medications === 'None'" v-for="line in this.camper.medications" v-bind:key="line">
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
      <p v-show="!showMedications && !newData.medications && (!camper.medications || camper.medications === 'None')">None</p>
      </td>
      <td>
        <button type="button" v-on:click.prevent="showMedications = true" v-show="!showMedications">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('medications')" v-show="showMedications">Save</button>
        <button type="button" v-on:click.prevent="newData.medications = ''; showMedications = false" v-show="showMedications">Cancel</button>
      </td>
    </tr>
    <tr id="info">
      <td>Special Needs:</td>
      <td>
        <ul class="data">
          <li v-show="!showSpecial && !newData.specialNeeds && camper.specialNeeds && !camper.specialNeeds === 'None'" v-for="line in this.camper.specialNeeds" v-bind:key="line">
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
      <p v-show="!showSpecial && !newData.specialNeeds && (!camper.specialNeeds || camper.specialNeeds === 'None')">None</p>
      </td>
      <td>
        <button type="button" v-on:click.prevent="showSpecial = true" v-show="!showSpecial">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('specialNeeds')" v-show="showSpecial">Save</button>
        <button type="button" v-on:click.prevent="newData.specialNeeds = ''; showSpecial = false" v-show="showSpecial">Cancel</button>
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
      showPayment: false,
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
        case "payment":
          this.camperToAdd.newValue = this.newData.paymentStatus;
          this.camperToAdd.oldValue = this.camper.paymentStatus;
          this.showPayment = false;
          break;
        case "registrar":
          this.camperToAdd.newValue = this.newData.registrar;
          this.camperToAdd.oldValue = this.camper.paymentStatus;
          this.showRegistrar = false;
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
      if(this.newData.paymentStatus)
      {
        this.camper.paymentStatus = this.newData.paymentStatus;
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
  background-color: $secondary;
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
tr{
  height: 74px;
}
td{
    padding: 0% 2%;
    width: 50px;
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