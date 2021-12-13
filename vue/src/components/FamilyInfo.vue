<template>
  <section>
  <table>
    <tr id="fullName">
      <td>Full Name:</td>
      <td>
        <span v-show="!showFull && !pending.fullName" class="data">{{ this.family.fullName }}</span>
        <span v-show="!showFull && pending.fullName" class ="newValue">{{this.pending.fullName}}</span>
        <input type="text" v-model="newData.fullName" v-show="showFull" />
      </td>
      <td class="editButtons">
        <button type="button" v-on:click.prevent="showFull = true; newData.fullName = family.fullName" v-show="!showFull" v-bind:disabled="pending.fullName" v-if="!($store.state.user.role == 'admin' && pending.fullName)">Edit</button>
        <button type="button" v-on:click="rejectRequest('fullName')" v-if="$store.state.user.role == 'admin' && pending.fullName">Reject</button>
        <button type="submit" v-on:click="saveChange('fullName')" v-show="showFull" v-bind:disabled="!newData.fullName">Save</button>
        <button type="button" v-on:click.prevent="newData.fullName = []; showFull = false" v-show="showFull">Cancel</button>
      </td>
    </tr>
    <tr id="city">
      <td>City:</td>
      <td>
        <span v-show="!showCity && !pending.city" class="data">{{ this.family.city }}</span>
        <span v-show="!showCity && pending.city" class="newValue">{{ this.pending.city }}</span>
        <input type="text" v-model="newData.city" v-show="showCity" />
      </td>
      <td class="editButtons">
        <button type="button" v-on:click.prevent="showCity = true; newData.city = family.city" v-show="!showCity" v-bind:disabled="pending.city" v-if="!($store.state.user.role == 'admin' && pending.city)">Edit</button>
        <button type="button" v-on:click="rejectRequest('city')" v-if="$store.state.user.role == 'admin' && pending.city">Reject</button>
        <button type="submit" v-on:click="saveChange('city')" v-show="showCity" v-bind:disabled="!newData.city">Save</button>
        <button type="button" v-on:click.prevent="newData.city = []; showCity = false" v-show="showCity">Cancel</button>
      </td>
    </tr>
    <tr id="state">
      <td>State:</td>
      <td>
        <span v-show="!showState && !pending.state" class="data">{{ this.family.state }}</span>
        <span v-show="!showState && pending.state" class="newValue">{{ this.pending.state }}</span>
        <select v-model="newData.state" v-show="showState">
          <option v-for="state in $store.state.states" v-bind:key="state">
            {{ state }}
          </option>
        </select>
      </td>
      <td class="editButtons">
        <button type="button" v-on:click.prevent="showState = true; newData.state = family.state" v-show="!showState" v-bind:disabled="pending.state" v-if="!($store.state.user.role == 'admin' && pending.state)">Edit</button>
        <button type="button" v-on:click="rejectRequest('state')" v-if="$store.state.user.role == 'admin' && pending.state">Reject</button>
        <button type="submit" v-on:click="saveChange('state')" v-show="showState" v-bind:disabled="!newData.state">Save</button>
        <button type="button" v-on:click.prevent="newData.state = []; showState = false" v-show="showState">Cancel</button>
      </td>
    </tr>
    <tr id="zip">
      <td>Zip:</td>
      <td>
        <span v-show="!showZip && !pending.zip" class="data">{{ this.family.zip }}</span>
        <span v-show="!showZip && pending.zip" class="newValue">{{ this.pending.zip }}</span>
        <input type="text" v-model.number="newData.zip" v-show="showZip" />
      </td>
      <td class="editButtons">
        <button type="button" v-on:click.prevent="showZip = true; newData.zip = family.zip" v-show="!showZip" v-bind:disabled="pending.zip" v-if="!($store.state.user.role == 'admin' && pending.zip)">Edit</button>
        <button type="button" v-on:click="rejectRequest('zip')" v-if="$store.state.user.role == 'admin' && pending.zip">Reject</button>
        <button type="submit" v-on:click="saveChange('zip')" v-show="showZip" v-bind:disabled="!newData.zip">Save</button>
        <button type="button" v-on:click.prevent="newData.zip = []; showZip = false" v-show="showZip">Cancel</button>
      </td>
    </tr>
    <tr id="phoneNumber">
      <td>Phone Number:</td>
      <td>
        <span v-show="!showPhone && !pending.phoneNumber" class="data">{{ this.family.phoneNumber }}</span>
        <span v-show="!showPhone && pending.phoneNumber" class="newValue">{{ this.pending.phoneNumber }}</span>
        <input type="text" v-model="newData.phoneNumber" v-show="showPhone" />
      </td>
      <td class="editButtons">
        <button type="button" v-on:click.prevent="showPhone = true; newData.phoneNumber = family.phoneNumber" v-show="!showPhone" v-bind:disabled="pending.phoneNumber" v-if="!($store.state.user.role == 'admin' && pending.phoneNumber)">Edit</button>
        <button type="button" v-on:click="rejectRequest('phone_number')" v-if="$store.state.user.role == 'admin' && pending.phoneNumber">Reject</button>
        <button type="submit" v-on:click="saveChange('phone_number')" v-show="showPhone" v-bind:disabled="!newData.phoneNumber">Save</button>
        <button type="button" v-on:click.prevent="newData.phoneNumber = []; showPhone = false" v-show="showPhone">Cancel</button>
      </td>
    </tr>
    <tr id="emailAddress">
      <td>Email:</td>
      <td>
        <span v-show="!showEmail && !pending.emailAddress" class="data">{{ this.family.emailAddress }}</span>
        <span v-show="!showEmail && pending.emailAddress" class="newValue">{{ this.pending.emailAddress }}</span>
        <input type="text" v-model.number="newData.emailAddress" v-show="showEmail" />
      </td>
      <td class="editButtons">
        <button type="button" v-on:click.prevent="showEmail = true; newData.emailAddress = family.emailAddress" v-show="!showEmail" v-bind:disabled="pending.emailAddress" v-if="!($store.state.user.role == 'admin' && pending.emailAddress)">Edit</button>
        <button type="button" v-on:click="rejectRequest('email_address')" v-if="$store.state.user.role == 'admin' && pending.emailAddress">Reject</button>
        <button type="submit" v-on:click="saveChange('email_address')" v-show="showEmail" v-bind:disabled="!newData.emailAddress">Save</button>
        <button type="button" v-on:click.prevent="newData.emailAddress = []; showEmail = false" v-show="showEmail">Cancel</button>
      </td>
    </tr>
    <tr id="campers">
      <td>Campers:</td>
      <td>
        <div class="data">
          <ul>
            <li v-for="camper in family.campers" v-bind:key="camper.camperCode">
              <router-link v-bind:to="{name: 'camper', params: {camperCode: camper.camperCode}}">{{camper.firstName}} {{camper.lastName}}</router-link>
            </li>
          </ul>
        </div>
      </td>
    </tr>
  </table>
  <span class="adminButtons" v-if="this.$store.state.user.role == 'admin'">
    <button type="submit" v-on:click="approveRequests()">Approve All</button>
    <button type="submit" v-on:click="rejectAllRequests()">Reject All</button>
  </span>
</section>
</template>

<script>
import FamilyService from '../services/FamilyService.js'
import UpdateService from '../services/UpdateService.js'

export default {
  props:{
    family: Object,
  },
  data(){
    return{
      updates: [],
      showFull: false,
      showEmail: false,
      showCity: false,
      showState: false,
      showZip: false,
      showPhone: false,
      newData: {},
      pending: {},
      updateToSend: {},
    }
  },
  methods:{
    saveChange(formName) {
      this.updateToSend.fieldToBeChanged = formName;
      switch (formName) {
        case "fullName":
          this.updateToSend.oldData= this.family.fullName;
          this.updateToSend.newData = this.newData.fullName;
          this.pending.fullName = this.newData.fullName;
          this.family.fullName= this.newData.fullName;
          this.showFull = false;
          break;
        case "city":
          this.updateToSend.oldData= this.family.city;
          this.updateToSend.newData = this.newData.city;
          this.pending.city = this.newData.city;
          this.family.city = this.newData.city;
          this.showCity = false;
          break;
        case "state":
          this.updateToSend.oldData= this.family.state;
          this.updateToSend.newData = this.newData.state;
          this.pending.state = this.newData.state;
          this.family.state = this.newData.state;
          this.showState = false;
          break;
        case "zip":
          this.updateToSend.oldData= this.family.zip;
          this.updateToSend.newData = this.newData.zip;
          this.pending.zip = this.newData.zip;
          this.family.zip = this.newData.zip;
          this.showZip = false;
          break;
        case "phone_number":
          this.updateToSend.oldData= this.family.phoneNumber;
          this.updateToSend.newData = this.newData.phoneNumber;
          this.family.phoneNumber = this.newData.phoneNumber;
          this.pending.phoneNumber = this.newData.phoneNumber;
          this.showPhone = false;
          break;
        case "email_address":
          this.updateToSend.oldData= this.family.emailAddress;
          this.updateToSend.newData = this.newData.emailAddress;
          this.pending.emailAddress = this.newData.emailAddress;
          this.family.emailAddress = this.newData.emailAddress;
          this.showEmail = false;
          break;
      }
      this.logChanges()
    },
    convertToPending(data)
    {
      if(data.status == 'Pending')
      {
        this.updates.push(data);
        switch(data.fieldToBeChanged)
        {
          case"full_name":
          this.pending.fullName = data.newData;
          break;
          case"city":
          this.pending.city = data.newData;
          break;
          case"state":
          this.pending.state = data.newData;
          break;
          case"zip":
          this.pending.zip = data.newData;
          break;
          case"phone_number":
          this.pending.phoneNumber = data.newData;
          break;
          case"email_address":
          this.pending.emailAddress = data.newData;
          break;
        }
      }
    },
    approveRequests(){
      let requestIds = []
      this.updates.forEach(r => requestIds.push(r.requestId));
      UpdateService.approveAllFamilyRequests(requestIds)
      .then(response => {
        console.log('All Requests Approved', response.data)
        this.pending = {};
        this.$router.go();
      })
      .catch(response => {
        console.error('Problem approving request', response);
      })
    },
    rejectRequest(formName){
      let request = this.updates.find(r => r.fieldToBeChanged == formName);
      if(!request)
      {
        console.error('Cannot Find Request', request)
        return '';
      }
      UpdateService.rejectFamilyRequest(request.requestId)
      .then(response => {
        console.log('Request Rejected', response.data)
        this.$router.go();
      })
      .catch(response => {
        console.error('Problem Rejecting Request', response);
      })
    },
    rejectAllRequests(){
      let requestIds = []
      this.updates.forEach(r => requestIds.push(r.requestId));
      UpdateService.rejectAllFamilyRequests(requestIds)
      .then(response => {
        console.log('All Requests Rejected', response.data);
        this.pending = {};
        this.$router.go();
      })
      .catch(response => {
        console.error('Problem Rejecting Request', response);
      })
    },
    createUpdate(){
      this.updateToSend.action = 'Update';
      this.updateToSend.familyId = parseInt(this.$route.params.familyId);
      this.updateToSend.requestor = this.$store.state.user.username;
      },
    logChanges(formName){
      this.createUpdate(formName);
       FamilyService.logChanges(this.updateToSend)
        .then(response => {
          console.log('Logged changes', response.data);
        })
        .catch(response => {
          console.warn('Problem logging changes', response);
        })
    }
  },
  created(){
    this.newData = {};
    UpdateService.getUpdatesByFamilyId(this.$route.params.familyId)
    .then(response => {
        console.log('Got all updates', response.data);
        this.updates = response.data;
        this.updates.forEach(u => this.convertToPending(u));
        this.$forceUpdate();
    })
    .catch(response => {
        console.error('Could not get updates', response)
    });
  }
};
</script>

<style lang="scss" scoped>
@import "../styles/colors.scss";

table {
  display: flex;
  flex-direction: column;
  margin: 1%;
  font-size: 130%;
}
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
  width: 100%;
}
button:disabled{
  background-color: $secondary;
  color: white;
}
tr{
  display: flex;
  align-items: center;
  padding: 0% 2%;
  margin: 1% 0%;
  min-height: 66px;
}
.row{
  padding: 0% 2%;
  width: 80%;
}
td{
  font-size: 120%;
  margin: 0% 1%;
  padding: 0% 1%;
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
.newValue{
  color: $highlight;
}
tr > td:nth-child(3)
{
  width: 25%;
}
tr > td:nth-child(2)
{
  margin-left: 2%;
  width: 50%;
  text-align: justify;
}
tr > td:first-child
{
  width: 25%
}
ul{
  padding: 0;
}
.editButtons{
  display: flex;
}
.editButtons button{
  width: 100%;
}
.adminButtons{
  display: inline-flex;
  width: 100%;
  justify-content: center;
  margin: 2% 0%;
}
a{
  color: $textDark;
  text-decoration: none;
}
</style>