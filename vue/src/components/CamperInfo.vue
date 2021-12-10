<template>
<section>
  <table>
    <tr class="row">
      <td>First Name:</td>
      <td>
        <div v-show="!showFirst" class="data">
          <span v-if="!pending.firstName">{{ this.camper.firstName }}</span>
          <span v-if="pending.firstName" class="newValue">{{ this.pending.firstName }}</span>
        </div>
        <input type="text" v-model="newData.firstName" v-show="showFirst" />
      </td>
      <td class="editButtons">
        <button type="button" v-on:click.prevent="showFirst = true" v-show="!showFirst" v-bind:disabled="pending.firstName">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('firstName')" v-show="showFirst" v-bind:disabled="!newData.firstName">Save</button>
        <button type="button" v-on:click.prevent="newData.firstName = ''; showFirst = false" v-show="showFirst">Cancel</button>
      </td>
    </tr>
    <tr class="row">
      <td>Last Name:</td>
      <td>
        <div v-show="!showLast"  class="data">
          <span v-if="!pending.lastName"> {{ this.camper.lastName }}</span>
          <span v-if="pending.lastName" class="newValue">{{ this.pending.lastName }}</span>
        </div>
        <input type="text" v-model="newData.lastName" v-show="showLast" />
      </td>
      <td class="editButtons">
        <button type="button" v-on:click.prevent="showLast = true" v-show="!showLast" v-bind:disabled="pending.lastName">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('lastName')" v-show="showLast" v-bind:disabled="!newData.lastName">Save</button>
        <button type="button" v-on:click.prevent="newData.lastName = ''; showLast = false" v-show="showLast">Cancel</button>
      </td>
    </tr>
    <tr class="row">
      <td>Registrar:</td>
      <td>
        <div v-show="!showRegistrar"  class="data">
          <span v-if="!pending.registrar"> {{ this.camper.registrar }}</span>
          <span v-if="pending.registrar" class="newValue">{{ this.pending.registrar }}</span>
        </div>
        <input type="text" v-model="newData.registrar" v-show="showRegistrar" />
      </td>
      <td class="editButtons">
        <button type="button" v-on:click.prevent="showRegistrar = true" v-show="!showRegistrar" v-bind:disabled="pending.registrar">Edit</button>
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
          <span v-if="!pending.dob">{{ this.convertedDateTime }}</span>
          <span v-if="pending.dob" class="newValue">{{ this.pendingConvertedDateTime }}</span>
        </div>
        <input type="date" v-model="newData.dob" v-show="showDOB" />
      </td>
      <td class="editButtons">
        <button type="button" v-on:click.prevent="showDOB = true" v-show="!showDOB" v-bind:disabled="pending.dob">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('dob')" v-show="showDOB" v-bind:disabled="!newData.dob">Save</button>
        <button type="button" v-on:click.prevent="newData.dob = ''; showDOB = false" v-show="showDOB">Cancel</button>
      </td>
    </tr>
    <tr class="row">
      <td>Payment Status:</td>
      <td>
        <div class="data">
        <span v-if="!showPayment && !pending.paymentStatus">{{ this.camper.paymentStatus }}</span>
        <span v-if="pending.paymentStatus && !showPayment" class="newValue">{{ this.pending.paymentStatus }}</span>
        </div>
        <div v-show="showPayment">
        <label for="unpaid">Unpaid</label>
        <input type="radio" id="unpaid" name="unpaid" value="Unpaid" v-model="newData.paymentStatus">
        <label for="paid">Paid</label>
        <input type="radio" id="paid" name="paid" value="Paid" v-model="newData.paymentStatus">
      </div>
      </td>
      <td class="editButtons">
        <button type="button" v-on:click.prevent="showPayment = true;" v-show="!showPayment" v-bind:disabled="pending.paymentStatus">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('payment')" v-show="showPayment" v-bind:disabled="!newData.paymentStatus">Save</button>
        <button type="button" v-on:click.prevent="newData.paymentStatus = ''; showPayment = false" v-show="showPayment">Cancel</button>
      </td>
    </tr>
    <tr class="row">
      <td>Active Status:</td>
      <td>
        <div class="data">
        <span v-if="!showActive && !pending.activeStatus">{{ this.camper.activeStatus }}</span>
        <span v-if="pending.activeStatus && !showActive" class="newValue">{{ this.pending.activeStatus }}</span>
        </div>
        <div v-show="showActive">
        <label for="Active">Active</label>
        <input type="radio" id="active" name="active" value="Active" v-model="newData.activeStatus">
        <label for="Inactive">Inactive</label>
        <input type="radio" id="Inactive" name="Inactive" value="Inactive" v-model="newData.activeStatus">
      </div>
      </td>
      <td class="editButtons">
        <button type="button" v-on:click.prevent="showActive = true;" v-show="!showActive" v-bind:disabled="pending.activeStatus">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('active')" v-show="showActive" v-bind:disabled="!newData.activeStatus">Save</button>
        <button type="button" v-on:click.prevent="newData.activeStatus = ''; showActive = false" v-show="showActive">Cancel</button>
      </td>
    </tr>
    <tr class="row">
      <td>Family: </td>
      <td>
        <div v-show="!showFamily"  class="data">
          <span v-if="!pending.familyId">{{ this.camper.familyId }}--{{this.camper.familyName}}</span>
          <span v-if="pending.familyId" class="newValue">{{ this.newFamilyId }}--{{ this.newFamilyName }}</span>
        </div>
        <select v-model="newData.familyId" v-show="showFamily">
          <option v-for="f in $store.state.families" v-bind:key="f.familyId">{{f.familyId}}--{{f.fullName}}</option>
        </select>
      </td>
      <td class="editButtons">
        <button type="button" v-on:click.prevent="showFamily = true" v-show="!showFamily" v-bind:disabled="pending.familyId">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('familyId')" v-show="showFamily" v-bind:disabled="!newData.familyId">Save</button>
        <button type="button" v-on:click.prevent="newData.familyId = ''; showFamily = false" v-show="showFamily">Cancel</button>
      </td>
    </tr>
    <tr class="row">
      <td>Allergies:</td>
      <td>
        <ul class="data" v-if="this.camper.allergies">
          <li v-show="!showAllergies && !pending.allergies && camper.allergies" v-for="line in allergies" v-bind:key="line">
            {{ line }}
          </li>
          <li v-show="!showAllergies && pending.allergies" v-for="line in this.pending.allergies" v-bind:key="line" class="newValue">
            {{ line }}
          </li>
          <div v-show="showAllergies">
          <input type="text" v-model="newData.allergies" />
          </div>
        </ul>
        <span v-show="!showAllergies && !pending.allergies && !camper.allergies">None</span>
      </td>
      <td class="editButtons">
        <button type="button" v-on:click.prevent="showAllergies = true" v-show="!showAllergies" v-bind:disabled="pending.allergies">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('allergies')" v-show="showAllergies" v-bind:disabled="!newData.allergies">Save</button>
        <button type="button" v-on:click.prevent="newData.allergies = ''; showAllergies = false" v-show="showAllergies">Cancel</button>
      </td>
    </tr>
     <tr class="row">
      <td>Medications:</td>
      <td>
        <ul class="data" v-if="this.camper.medications">
          <li v-show="!showMedications && !pending.medications && camper.medications" v-for="line in medications" v-bind:key="line">
            {{ line }}
          </li>
          <li v-show="!showMedications && pending.medications" v-for="line in this.pending.medications" v-bind:key="line" class="newValue">
            {{ line }}
          </li>
          <div v-show="showMedications">
          <input type="text" v-model="newData.medications" />
          </div>
        </ul>
      <span v-show="!showMedications && !pending.medications && !camper.medications">None</span>
      </td>
      <td class="editButtons">
        <button type="button" v-on:click.prevent="showMedications = true" v-show="!showMedications" v-bind:disabled="pending.medications">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('medications')" v-show="showMedications" v-bind:disabled="!newData.medications">Save</button>
        <button type="button" v-on:click.prevent="newData.medications = ''; showMedications = false" v-show="showMedications">Cancel</button>
      </td>
    </tr>
    <tr class="row">
      <td>Special Needs:</td>
      <td>
        <ul class="data" v-if="this.camper.specialNeeds">
          <li v-show="!showSpecial && !pending.specialNeeds && camper.specialNeeds" v-for="line in specialNeeds" v-bind:key="line">
            {{ line }}
          </li>
          <li v-show="!showSpecial && pending.specialNeeds" v-for="line in this.pending.specialNeeds" v-bind:key="line" class="newValue">
            {{ line }}
          </li>
          <div v-show="showSpecial">
          <input type="text" v-model="newData.specialNeeds" />
          </div>
        </ul>
      <span v-show="!showSpecial && !pending.specialNeeds && !camper.specialNeeds">None</span>
      </td>
      <td class="editButtons">
        <button type="button" v-on:click.prevent="showSpecial = true" v-show="!showSpecial" v-bind:disabled="pending.specialNeeds">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('specialNeeds')" v-show="showSpecial" v-bind:disabled="!newData.specialNeeds">Save</button>
        <button type="button" v-on:click.prevent="newData.specialNeeds = ''; showSpecial = false" v-show="showSpecial">Cancel</button>
      </td>
    </tr>
  </table>
  <div class="adminButtons" v-if="this.$store.state.user.role == 'admin'">
  <button type="button" v-on:click.prevent="approveRequest()">Approve Changes</button>
  <button type="button" v-on:click.prevent="rejectRequest()">Reject Changes</button>
  <button type="submit" v-on:click.prevent="finalizeChanges()" v-if="this.$store.state.user.role != 'admin' || newDataAdded">Submit Changes</button>
  </div>
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
      showActive: false,
      requestId: 0,
            };
  },
  props:{
    camper: Object,
  },
  computed:{
    newDataAdded()
    {
      return this.newData.firstName || this.newData.lastName || this.newData.dob || this.newData.registrar || 
      this.newData.paymentStatus || this.newData.activeStatus || this.newData.familyId || this.newData.allergies || this.newData.medications || this.newData.specialNeeds
    },
    allergies(){
      if(typeof(this.camper.allergies) == String)
      {
        return this.camper.allergies.split('')
      }
        return this.camper.allergies;
    },
    medications (){
      if(typeof(this.camper.medications) == String)
      {
        return this.camper.medications.split('')
      }
        return this.camper.medications;
    },
    specialNeeds (){
      if(typeof(this.camper.specialNeeds) == String)
      {
        return this.camper.specialNeeds.split('')
      }
        return this.camper.specialNeeds;
    },
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
    pendingConvertedDateTime() {
      if(this.pending.dob)
      {
        let dob = new Date(this.pending.dob);
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
          this.pending.firstName = this.newData.firstName;
          this.showFirst = false;
          break;
        case "lastName":
          this.pending.lastName = this.newData.lastName;
          this.showLast = false;
          break;
          case "dob":
          this.pending.dob = this.newData.dob;
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
          this.pending.familyId = this.newData.familyId;
          this.showFamily = false;
          break;
        case "specialNeeds":
          this.pending.specialNeeds = this.newData.specialNeeds.split(',');
          this.showSpecial = false;
          break;
        case "payment":
          this.pending.paymentStatus = this.newData.paymentStatus;
          this.showPayment = false;
          break;
        case "registrar":
          this.pending.registrar = this.newData.registrar;
          this.showRegistrar = false;
          break;
        case "active":
          this.pending.activeStatus = this.newData.activeStatus;
          this.showActive = false;
          break;
      }
    },
    setCamper(){
      this.pending.firstName? this.camper.firstName = this.newData.firstName: '';
      this.pending.lastName? this.camper.lastName = this.newData.lastName: '';
      if(this.newData.allergies)
      {
        this.pending.allergies? this.camper.allergies = this.newData.allergies.toString(): this.camper.allergies = '';
      }
      else if (this.camper.allergies)
      {
        this.camper.allergies = this.camper.allergies.toString();
      }
     if(this.camper.medications)
      {
        this.pending.medications? this.camper.medications = this.newData.medications.toString(): this.camper.medications = '';
      }
      else if (this.camper.medications)
      {
        this.camper.medications = this.camper.medications.toString();
      }
      this.pending.familyId? this.camper.familyId = this.newFamilyId: '';
      if(this.camper.specialNeeds)
      {
      this.pending.specialNeeds? this.camper.specialNeeds = this.newData.specialNeeds.toString(): this.camper.specialNeeds = '';
      }
      else if (this.camper.specialNeeds)
      {
        this.camper.specialNeeds = this.camper.specialNeeds.toString();
      }
      this.camper.paymentStatus == this.pending.paymentStatus;
      this.camper.paymentStatus == 'Unpaid'? this.camper.paymentStatus = false : this.camper.paymentStatus = true;
      this.pending.registrar? this.camper.registrar = this.newData.registrar: '';
      this.pending.dob? this.camper.dob = this.newData.dob: '';
      this.camper.activeStatus == this.pending.activeStatus;
      this.pending.activeStatus == 'Inactive'? this.camper.activeStatus = false : this.camper.activeStatus = true;
    },
    convertToPending(data)
    {
      this.requestId = data.requestId;
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
        data.newData? this.pending.allergies = data.newData.split(','): this.pending.allergies;
        break;
        case"medications":
        data.newData? this.pending.medications = data.newData.split(','): this.pending.medications = '';
        break;
        case"family_id":
        this.pending.familyId = data.newData;
        break;
        case"special_needs":
        data.newData? this.pending.specialNeeds = data.newData.split(','): this.pending.specialNeeds = '';
        break;
        case"active_status":
        data.newData? this.pending.activeStatus = 'Active': this.pending.activeStatus = 'Inactive';
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
        this.pending = {};
      })
      .catch(response => {
        console.error('Problem approving request', response);
      })
    },
    rejectRequest(){
      UpdateService.rejectRequest(this.requestId)
      .then(response => {
        console.log('Request rejected', response.data)
      })
      .catch(response => {
        console.error('Problem rejecting request', response);
      })
    }
  },
  created(){
    this.$store.commit('SET_CAMPER_LIST');
    this.$forceUpdate();
    UpdateService.getUpdatesByCamperCode(this.$route.params.camperCode)
    .then(response => {
      console.log('Got updates', response.data);
      response.data.forEach(u => this.convertToPending(u));
    })
    .catch(response => {
      console.error('Problem getting updates', response)
    });
  }
};
</script>

<style lang="scss" scoped>
@import "../styles/colors.scss";

table {
  display: flex;
  flex-direction: column;
  width: 120%;
  margin: 1%;
  font-size: 130%;
}
// .adminButtons{
//   display: flex;
//   justify-content: flex-end;
//   margin: 2%;
// }
button {
  background-color: $textDark;
  border: none;
  color: $textLight;
  margin: 0% 2%;
  border-radius: 5px;
  border: 2px solid $highlight;
  text-shadow: 2px 1px 1px black;
  font-size: 1em;
  font-family: 'Lora', serif;
  box-shadow: 1px 0.5px 0px $textLight;
  width: 30%;
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
  font-size: 120%;
}
input, select{
  font-family: 'Lora', serif;
  border: 1px dotted $highlight;
  border-radius: 10px;
  background-color: $textDark;
  color: $textLight;
  width: 60%;
  font-size: 1em;
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
tr > td:nth-child(3)
{
  width: 30%;
}
tr > td:nth-child(2)
{
  margin-left: 2%;
  text-align: justify;
}
tr > td:first-child
{
  width: 40%
}
ul{
  padding: 2%;
}
.editButtons{
  display: flex;
}
.editButtons button{
  width: 100%;
}
</style>