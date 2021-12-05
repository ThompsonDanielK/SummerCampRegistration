<template>
    <!-- <img src="image"> -->
  <table>
    <tr id="firstName">
      <td>First Name:</td>
      <td class="data">
          <div v-show="!showFirst">
        {{ this.camper.firstName }}
          </div>
      <input type="text" v-model="this.camper.firstName" v-show="showFirst" />
      </td>
      <td><button
        type="button"
        v-on:click.prevent="showHideForm('firstName')"
        v-show="!showFirst"
      >
        Edit
      </button>
      <button
        type="submit"
        v-on:click.prevent="showFirst = false"
        v-show="showFirst"
      >
        Save
      </button>
      <button
        type="button"
        id="firstCancel"
        v-on:click.prevent="saveChanges(data={ name: firstName, value: this.camper.firstName})"
        v-show="showFirst"
      >
        Cancel
      </button>
      </td>
    </tr>
    <tr id="lastName">
      <td>Last Name:</td>
      <td class="data">
          <div v-show="!showLast">
        {{ this.camper.lastName }}
          </div>
      <input type="text" v-model="this.camper.lastName" v-show="showLast" />
      </td>
      <td>
      <button
        type="button"
        v-on:click.prevent="showHideForm('lastName')"
        v-show="!showLast"
      >
        Edit
      </button>
      <button
        type="submit"
        v-on:click.prevent="saveChanges(data={ name: lastName, value: this.camper.lastName})"
        v-show="showLast"
      >
        Save
      </button>
      <button
        type="button"
        id="lastCancel"
        v-on:click.prevent="showLast = false"
        v-show="showLast"
      >
        Cancel
      </button>
      </td>
    </tr>
    <tr id="city">
      <td>City:</td>
      <td class="data">
        <div v-show="!showCity">
        {{ this.camper.city }}
        </div>
      <input type="text" v-model="this.camper.city" v-show="showCity" />
      </td>
      <td>
      <button type="button" v-on:click.prevent="showHideForm('city')" v-show="!showCity">Edit</button>
      <button
        type="submit"
        v-on:click.prevent="saveChanges(data={ name: city, value: this.camper.city})"
        v-show="showCity"
      >
        Save
      </button>
      <button
        type="button"
        id="cityCancel"
        v-on:click.prevent="showCity = false"
        v-show="showCity"
      >
        Cancel
      </button>
      </td>
    </tr>
    <tr id="state">
      <td>State:</td>
      <td class="data">
        <div v-show="!showState">
        {{ this.camper.state }}
        </div>
      <select v-model="this.camper.state" v-show="showState">
      <option v-for="state in states" v-bind:key="state">{{state}}</option>
      </select>
      </td>
      <td>
      <button type="button" v-on:click.prevent="showHideForm('state')" v-show="!showState">Edit</button>
      <button
        type="submit"
        v-on:click.prevent="saveChanges(data={ name: state, value: this.camper.state})"
        v-show="showState"
      >
        Save
      </button>
      <button
        type="button"
        id="stateCancel"
        v-on:click.prevent="showState = false"
        v-show="showState"
      >
        Cancel
      </button>
      </td>
    </tr>
    <tr id="zip">
      <td>Zip:</td>
      <td>
          <div v-show="!showZip">
        {{ this.camper.zip }}
          </div>
      <input type="text" v-model="this.camper.zip" v-show="showZip" />
      </td>
      <td>
      <button type="button" v-on:click.prevent="showHideForm('zip')" v-show="!showZip">Edit</button>
      <button
        type="submit"
        v-on:click.prevent="saveChanges(data={ name: zip, value: this.camper.zip})"
        v-show="showZip"
      >
        Save
      </button>
      <button
        type="button"
        id="zipCancel"
        v-on:click.prevent="showZip = false"
        v-show="showZip"
      >
        Cancel
      </button>
      </td>
    </tr>
    <tr id="info">
      <td>Special Needs:</td>
      <td>
      <ul class="data">
        <li v-show="!showSpecial" v-for="line in this.camper.specialInfo" v-bind:key="line">
          {{ line }}
        </li>
        <textarea type="text" v-model="this.camper.specialInfo" v-show="showSpecial" />
      </ul>
      </td>
      <td>
      <button
        type="button"
        v-on:click.prevent="showHideForm('specialInfo')"
        v-show="!showSpecial"
      >
        Edit
      </button>
      <button
        type="submit"
        v-on:click.prevent="saveChanges(data={ name: specialInfo, value: this.camper.specialInfo})"
        v-show="showSpecial"
      >
        Save
      </button>
      <button
        type="button"
        id="specialCancel"
        v-on:click.prevent="showSpecial = false"
        v-show="showSpecial"
      >
        Cancel
      </button>
      </td>
    </tr>
  </table>
</template>

<script>
// import CamperService from "../services/CamperService.js";

export default {
  data() {
    return {
      camper: {
        id: "1",
        firstName: "Caleb",
        lastName: "Gaffney",
        city: "Columbus",
        state: "OH",
        zip: "43210",
        specialInfo: [
          "Needs medicine or will die",
          "Seems nice enough",
          "Can't swim",
        ],
      },
      changes: {
        id: "1",
      },
      showFirst: false,
      showLast: false,
      showCity: false,
      showState: false,
      showZip: false,
      showSpecial: false,
      states: [
          'AL', 'AK', 'AR', 'AZ', 'CA', 'CO', 'CT', 'DE', 'FL', 'GA', 'HI', 'ID', 'IL', 'IN', 'IA', 'KS', 'KY', 'LA', 'MA', 'MD', 'ME', 'MI', 'MN', 'MO', 'MS',
          'MT', 'NC', 'ND', 'NE', 'NH', 'NJ', 'NM', 'NV', 'NY', 'OH', 'OK', 'OR', 'PA', 'RI', 'SC', 'SD', 'TN', 'TX', 'UT', 'VA', 'VT', 'WA', 'WI', 'WV', 'WY'
      ]
    };
  },
  methods: {
    showHideForm(formName, show = true) {
      switch (formName) {
        case "firstName":
          this.showFirst = show;
          break;
        case "lastName":
          this.showLast = show;
          break;
        case "city":
          this.showCity = show;
          break;
        case "state":
          this.showState = show;
          break;
        case "zip":
          this.showZip = show;
          break;
        case "specialInfo":
          this.showSpecial = show;
          break;
      }
    },
    saveChanges(data){
        let fieldToBeChanged = data.name
        this.changes = {
            fieldToBeChanged: data.value, 
        }
        this.showHideForm(fieldToBeChanged, false);
    }
  },
  //   created() {
  //     CamperService.getCamper(this.$route.params.camperId).then((response) => {});
  //   },
};
</script>

<style lang="scss" scoped>
@import "../styles/colors.scss";

table {
  color: $highlight;
  font-size: 1.4rem;
}
button {
  background-color: white;
  border: none;
  color: $textDark;
  text-decoration: underline;
}
table tr{
    padding: 100px;
    margin-top: 10px;
}
table td{
    margin-bottom: 10px;
    max-width: 10%;
}
</style>