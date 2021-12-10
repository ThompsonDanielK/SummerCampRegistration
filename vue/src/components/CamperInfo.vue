<template>
<section>
  <div class="adminButtons" v-if="this.$store.state.user.role == 'admin'" v-show="!showRegistrar">
  <button type="button" v-on:click.prevent="approveRequest()">Approve All</button>
  <button type="button" v-on:click.prevent="rejectRequest()">Reject All</button>
  </div>
  <table>
    <tr class="row">
      <td>First Name:</td>
      <td>
        <div v-show="!showFirst" class="data">
          <p v-if="!pending.firstName">{{ this.camper.firstName }}</p>
          <p v-if="pending.firstName" class="newValue">{{ this.pending.firstName }}</p>
        </div>
        <input type="text" v-model="newData.firstName" v-show="showFirst" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showFirst = true" v-show="!showFirst">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('firstName')" v-show="showFirst" v-bind:disabled="!newData.firstName">Save</button>
        <button type="button" v-on:click.prevent="newData.firstName = ''; showFirst = false" v-show="showFirst">Cancel</button>
      </td>
    </tr>
    <tr class="row">
      <td>Last Name:</td>
      <td>
        <div v-show="!showLast"  class="data">
          <p v-if="!pending.lastName"> {{ this.camper.lastName }}</p>
          <p v-if="pending.lastName" class="newValue">{{ this.pending.lastName }}</p>
        </div>
        <input type="text" v-model="newData.lastName" v-show="showLast" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showLast = true" v-show="!showLast">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('lastName')" v-show="showLast" v-bind:disabled="!newData.lastName">Save</button>
        <button type="button" v-on:click.prevent="newData.lastName = ''; showLast = false" v-show="showLast">Cancel</button>
      </td>
    </tr>
    <tr class="row">
      <td>Registrar:</td>
      <td>
        <div v-show="!showRegistrar"  class="data">
          <p v-if="!pending.registrar"> {{ this.camper.registrar }}</p>
          <p v-if="pending.registrar" class="newValue">{{ this.pending.registrar }}</p>
        </div>
        <input type="text" v-model="newData.registrar" v-show="showRegistrar" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showRegistrar = true" v-show="!showRegistrar">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('registrar')" v-show="showRegistrar" v-bind:disabled="!newData.registrar">Save</button>
        <button type="button" v-on:click.prevent="newData.registrar = ''; showRegistrar = false" v-show="showRegistrar">Cancel</button>
      </td>
    </tr>
    <tr class="row">
      <td>Age:</td>
      <td>{{ camper.age }}</td>
      <td></td>
    </tr>
    <tr class="row">
      <td>Date of Birth:</td>
      <td>
        <div v-show="!showDOB"  class="data">
          <p v-if="!pending.dob">{{ this.convertedDateTime }}</p>
          <p v-if="pending.dob" class="newValue">{{ this.newConvertedDateTime }}</p>
        </div>
        <input type="date" v-model="newData.dob" v-show="showDOB" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showDOB = true" v-show="!showDOB">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('dob')" v-show="showDOB" v-bind:disabled="!newData.dob">Save</button>
        <button type="button" v-on:click.prevent="newData.dob = ''; showDOB = false" v-show="showDOB">Cancel</button>
      </td>
    </tr>
    <tr class="row">
      <td>Payment Status:</td>
      <td>
        <div class="data">
        <p v-if="!showPayment && !pending.paymentStatus">{{ this.camper.paymentStatus }}</p>
        <p v-if="pending.paymentStatus && !showPayment" class="newValue">{{ this.pending.paymentStatus }}</p>
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
        <button type="button" v-on:click.prevent="saveChange('payment')" v-show="showPayment" v-bind:disabled="!newData.paymentStatus">Save</button>
        <button type="button" v-on:click.prevent="newData.paymentStatus = ''; showPayment = false" v-show="showPayment">Cancel</button>
      </td>
    </tr>
    <tr class="row">
      <td>Family: </td>
      <td>
        <div v-show="!showFamily"  class="data">
          <p v-if="!pending.familyId">{{ this.camper.familyId }}--{{this.camper.familyName}}</p>
          <p v-if="pending.familyId" class="newValue">{{ this.newFamilyId }}--{{ this.newFamilyName }}</p>
        </div>
        <select v-model="newData.familyId" v-show="showFamily">
          <option v-for="f in $store.state.families" v-bind:key="f.familyId">{{f.familyId}}--{{f.fullName}}</option>
        </select>
      </td>
      <td>
        <button type="button" v-on:click.prevent="showFamily = true" v-show="!showFamily">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('familyId')" v-show="showFamily" v-bind:disabled="!newData.familyId">Save</button>
        <button type="button" v-on:click.prevent="newData.familyId = ''; showFamily = false" v-show="showFamily">Cancel</button>
      </td>
    </tr>
    <tr class="row">
      <td>Allergies:</td>
      <td>
        <ul class="data">
          <li v-show="!showAllergies && !pending.allergies && camper.allergies && camper.allergies != 'None'" v-for="line in this.camper.allergies" v-bind:key="line">
            {{ line }}
          </li>
          <li v-show="!showAllergies && pending.allergies" v-for="line in this.pending.allergies" v-bind:key="line" class="newValue">
            {{ line }}
          </li>
          <div v-show="showAllergies">
          <input type="text" v-model="newData.allergies" />
          </div>
        </ul>
        <p v-show="!showAllergies && !pending.allergies && (!camper.allergies || camper.allergies === 'None')">None</p>
      </td>
      <td>
        <button type="button" v-on:click.prevent="showAllergies = true" v-show="!showAllergies">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('allergies')" v-show="showAllergies" v-bind:disabled="!newData.allergies">Save</button>
        <button type="button" v-on:click.prevent="newData.allergies = ''; showAllergies = false" v-show="showAllergies">Cancel</button>
      </td>
    </tr>
     <tr class="row">
      <td>Medications:</td>
      <td>
        <ul class="data">
          <li v-show="!showMedications && !pending.medications && camper.medications && !camper.medications === 'None'" v-for="line in this.camper.medications" v-bind:key="line">
            {{ line }}
          </li>
          <li v-show="!showMedications && pending.medications" v-for="line in this.pending.medications" v-bind:key="line" class="newValue">
            {{ line }}
          </li>
          <div v-show="showMedications">
          <input type="text" v-model="newData.medications" />
          </div>
        </ul>
      <p v-show="!showMedications && !pending.medications && (!camper.medications || camper.medications === 'None')">None</p>
      </td>
      <td>
        <button type="button" v-on:click.prevent="showMedications = true" v-show="!showMedications">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('medications')" v-show="showMedications" v-bind:disabled="!newData.medications">Save</button>
        <button type="button" v-on:click.prevent="newData.medications = ''; showMedications = false" v-show="showMedications">Cancel</button>
      </td>
    </tr>
    <tr class="row">
      <td>Special Needs:</td>
      <td>
        <ul class="data">
          <li v-show="!showSpecial && !pending.specialNeeds && camper.specialNeeds && !camper.specialNeeds === 'None'" v-for="line in this.camper.specialNeeds" v-bind:key="line">
            {{ line }}
          </li>
          <li v-show="!showSpecial && pending.specialNeeds" v-for="line in this.pending.specialNeeds" v-bind:key="line" class="newValue">
            {{ line }}
          </li>
          <div v-show="showSpecial">
          <input type="text" v-model="newData.specialNeeds" />
          </div>
        </ul>
      <p v-show="!showSpecial && !pending.specialNeeds && (!camper.specialNeeds || camper.specialNeeds === 'None')">None</p>
      </td>
      <td>
        <button type="button" v-on:click.prevent="showSpecial = true" v-show="!showSpecial">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('specialNeeds')" v-show="showSpecial" v-bind:disabled="!newData.specialNeeds">Save</button>
        <button type="button" v-on:click.prevent="newData.specialNeeds = ''; showSpecial = false" v-show="showSpecial">Cancel</button>
      </td>
    </tr>
  </table>
  <button type="submit" v-on:click.prevent="finalizeChanges()">Submit Changes</button>
</section>
</template>

<script>
import UpdateService from '../services/UpdateService.js'
import CamperService from '../services/CamperService.js'

export default {
  data() {
    return {
      newData: {},
      pending: {},
      showFirst: false,
      showLast: false,
      showAllergies: false,
      showMedications: false,
      showPayment: false,
      showDOB: false,
      showFamily: false,
      showSpecial: false,
      showRegistrar: false,
      requestId: 0,
            };
  },
  props:{
    camper: Object,
  },
  computed:{
    newFamilyId(){
      let newFamily = this.newData.familyId.split('--');
      return parseInt(newFamily[0]);
    },
    newFamilyName(){
      let newFamily = this.newData.familyId.split('--');
      return newFamily[1];
    },
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
  },
  methods: {
    saveChange(formName) {
      switch (formName) {
        case "firstName":
          this.showFirst = false;
          break;
        case "lastName":
          this.showLast = false;
          break;
          case "dob":
          this.showDOB = false;
          break;
        case "allergies":
          this.pending.allergies = this.newData.allergies.split(',')
          this.showAllergies = false;
          break;
        case "medications":
          this.pending.medications = this.newData.medications.split(',')
          this.showMedications = false;
          break;
        case "familyId":
          this.showFamily = false;
          break;
        case "specialNeeds":
          this.pending.specialNeeds = this.newData.specialNeeds.split(',');
          this.showSpecial = false;
          break;
        case "payment":
          this.showPayment = false;
          break;
        case "registrar":
          this.showRegistrar = false;
      }
    },
    setCamper(){
      this.pending.firstName? this.camper.firstName = this.newData.firstName: '';
      this.pending.lastName? this.camper.lastName = this.newData.lastName: '';
      this.pending.allergies? this.camper.allergies = this.newData.allergies: '';
      this.pending.medications? this.camper.medications = this.newData.medications: '';
      this.pending.familyId? this.camper.familyId = this.newFamilyId: '';
      this.pending.specialNeeds? this.camper.specialNeeds = this.newData.specialNeeds: '';
      if(this.newData.paymentStatus) 
      {
        this.pending.paymentStatus == 'Unpaid'? this.camper.paymentStatus = false : this.camper.paymentStatus = true;
      }
      this.pending.registrar? this.camper.registrar = this.newData.registrar: '';
      this.pending.dob? this.camper.dob = this.newData.dob: '';
    },
    convertToPending(data)
    {
      switch(data.fieldToBeChanged)
      {
        case"first_name":
        this.pending.firstName = data.newData;
        break;
        case"last_name":
        this.pending.lastName = data.newData;
        break;
        case"dob":
        this.pending.dob = data.newData;
        break;
        case"registrar":
        this.pending.registrar = data.newData;
        break;
        case"allergies":
        this.pending.allergies = data.newData;
        break;
        case"medications":
        this.pending.medications = data.newData;
        break;
        case"family_id":
        this.pending.familyId = data.newData;
        break;
        case"special_needs":
        this.pending.specialNeeds = data.newData;
        break;
        case"payment_status":
        data.newData? this.pending.paymentStatus = 'Paid': this.pending.paymentStatus = 'Unpaid';
        break;
      }
    },
    finalizeChanges(){
      this.setCamper();
      CamperService.updateCamper(this.camper)
      .then(response => {
        console.log('Updated camper info', response.data);
        this.$store.hotUpdate();
      })
      .catch(response => {
        console.error('Cannot finalize changes', response);
      })
    },
    approveRequest(){
      UpdateService.approveRequest(this.requestId)
      .then(response => {
        console.log('Request Approved', response.data)
      })
      .catch(response => {
        console.error('Problem approving request', response);
      })
    }
  },
  created(){
    UpdateService.getUpdatesByCamperCode(this.$route.params.camperCode)
    .then(response => {
      console.log('Got updates', response.data);
      response.data.forEach(u => this.convertToPending(u));
      this.requestId = response.data.requestId;
      this.$forceUpdate();
    })
    .catch(response => {
      console.error('Problem getting updates', response)
    })
  }
};
</script>

<style lang="scss" scoped>
@import "../styles/colors.scss";

table {
  display: flex;
  flex-direction: column;
  width: 120%;
}
button {
  background-color: $textDark;
  border: none;
  color: $textLight;
  margin: 0% 2%;
  border-radius: 5px;
  border: 2px solid $highlight;
  text-shadow: 2px 1px 1px black;
  font-size: 1rem;
  font-family: 'Lora', serif;
  box-shadow: 1px 0.5px 0px $textLight;
}
button:disabled{
  background-color: $secondary;
  color: white;
}
tr{
  display: flex;
  align-items: center;
  padding: 0% 2%;
  height: 66px;
}
.row{
  padding: 0% 2%;
  width: 80%;
}
td{
  width: 70%;
}
input, select, textarea{
  font-family: 'Lora', serif;
  border: 1px dotted $highlight;
  border-radius: 10px;
  background-color: $textDark;
  color: $textLight;
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
.adminButtons{
  display: flex;
  justify-content: flex-end;
  margin: 2%;
}
tr > td:nth-child(3)
{
  width: 30%
}
</style>