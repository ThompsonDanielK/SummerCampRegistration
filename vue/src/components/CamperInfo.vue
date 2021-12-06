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
    <tr id="city">
      <td>City:</td>
      <td>
        <div v-show="!showCity" class="data">
          {{ this.camper.city }}
        </div>
        <input type="text" v-model="newValue.city" v-show="showCity" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('city')" v-show="!showCity">Edit</button>
        <button type="button" v-on:click.prevent="saveChange('city')" v-show="showCity">Save</button>
        <button type="button" id="cityCancel" v-on:click.prevent="newValue.city = camper.city; showCity = false" v-show="showCity">Cancel</button>
      </td>
    </tr>
    <tr id="state">
      <td>State:</td>
      <td>
        <div v-show="!showState" class="data">
          {{ this.camper.state }}
        </div>
        <select v-model="newValue.state" v-show="showState">
          <option v-for="state in states" v-bind:key="state">
            {{ state }}
          </option>
        </select>
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('state')" v-show="!showState">Edit</button>
        <button type="submit" v-on:click.prevent="saveChange('state')" v-show="showState">Save</button>
        <button type="button" id="stateCancel" v-on:click.prevent="newValue.state = camper.state; showState = false" v-show="showState">Cancel</button>
      </td>
    </tr>
    <tr id="zip">
      <td>Zip:</td>
      <td>
        <div v-show="!showZip" class="data">
          {{ this.camper.zip }}
        </div>
        <input type="text" v-model.number="newValue.zip" v-show="showZip" />
      </td>
      <td>
        <button type="button" v-on:click.prevent="showHideForm('zip')" v-show="!showZip">Edit</button>
        <button type="submit" v-on:click.prevent="saveChange('zip')" v-show="showZip">Save</button>
        <button type="button" id="zipCancel" v-on:click.prevent="newValue.zip = camper.zip; showZip = false" v-show="showZip">Cancel</button>
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
      showCity: false,
      showState: false,
      showZip: false,
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
        case "city":
          this.showCity = true;
          break;
        case "state":
          this.showState = true;
          break;
        case "zip":
          this.showZip = true;
          break;
        case "specialInfo":
          this.showSpecial = true;
          break;
      }
    },
    saveChange(formName) {
        this.changes.camperId = this.newValue.id;
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
        case "city":
          this.changes.city = this.newValue.city;
          this.camper.city = this.newValue.city;
          this.showCity = false;
          break;
        case "state":
          this.changes.state = this.newValue.state;
          this.camper.state = this.newValue.state;
          this.showState = false;
          break;
        case "zip":
          this.changes.zip = this.newValue.zip;
          this.camper.zip = this.newValue.zip;
          this.showZip = false;
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
      CamperService.updateCamper(this.camper)
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
      CamperService.getCamper(this.$route.params.camperId).then((response) => {
        console.log('Got camper', response.data)
        this.newValue = response.data;
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