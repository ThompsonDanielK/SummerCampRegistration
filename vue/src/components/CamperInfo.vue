<template>
  <section>
    <table>
      <tr class="row">
        <td>First Name:</td>
        <td>
          <div v-show="!showFirst" class="data">
            <span v-if="!pending.firstName">{{ this.camper.firstName }}</span>
            <span v-if="pending.firstName" class="newValue">{{
              this.pending.firstName
            }}</span>
          </div>
          <input type="text" v-model="newData.firstName" v-show="showFirst" />
        </td>
        <td class="editButtons">
          <button
            type="button"
            v-on:click.prevent="
              showFirst = true;
              newData.firstName = camper.firstName;
            "
            v-show="!showFirst"
            v-bind:disabled="pending.firstName"
            v-if="!($store.state.user.role == 'admin' && pending.firstName)"
          >
            Edit
          </button>
          <button
            type="button"
            v-on:click="rejectRequest('first_name')"
            v-if="$store.state.user.role == 'admin' && pending.firstName"
          >
            Reject
          </button>
          <button
            type="submit"
            v-on:click="saveChange('first_name')"
            v-show="showFirst"
          >
            Save
          </button>
          <button
            type="button"
            v-on:click.prevent="
              newData.firstName = '';
              showFirst = false;
            "
            v-show="showFirst"
          >
            Cancel
          </button>
        </td>
      </tr>
      <tr class="row">
        <td>Last Name:</td>
        <td>
          <div v-show="!showLast" class="data">
            <span v-if="!pending.lastName"> {{ this.camper.lastName }}</span>
            <span v-if="pending.lastName" class="newValue">{{
              this.pending.lastName
            }}</span>
          </div>
          <input type="text" v-model="newData.lastName" v-show="showLast" />
        </td>
        <td class="editButtons">
          <button
            type="button"
            v-on:click.prevent="
              showLast = true;
              newData.lastName = camper.lastName;
            "
            v-show="!showLast"
            v-bind:disabled="pending.lastName"
            v-if="!($store.state.user.role == 'admin' && pending.lastName)"
          >
            Edit
          </button>
          <button
            type="button"
            v-on:click="rejectRequest('last_name')"
            v-if="$store.state.user.role == 'admin' && pending.lastName"
          >
            Reject
          </button>
          <button
            type="submit"
            v-on:click="saveChange('last_name')"
            v-show="showLast"
            v-bind:disabled="!newData.lastName"
          >
            Save
          </button>
          <button
            type="button"
            v-on:click.prevent="
              newData.lastName = '';
              showLast = false;
            "
            v-show="showLast"
          >
            Cancel
          </button>
        </td>
      </tr>
      <tr class="row">
        <td>Registrar:</td>
        <td>
          <div v-show="!showRegistrar" class="data">
            <span v-if="!pending.registrar"> {{ this.camper.registrar }}</span>
            <span v-if="pending.registrar" class="newValue">{{
              this.pending.registrar
            }}</span>
          </div>
          <input
            type="text"
            v-model="newData.registrar"
            v-show="showRegistrar"
          />
        </td>
        <td class="editButtons">
          <button
            type="button"
            v-on:click.prevent="showRegistrar = true"
            v-show="!showRegistrar"
            v-bind:disabled="pending.registrar"
            v-if="!($store.state.user.role == 'admin' && pending.registrar)"
          >
            Edit
          </button>
          <button
            type="button"
            v-on:click="rejectRequest('registrar')"
            v-if="$store.state.user.role == 'admin' && pending.registrar"
          >
            Reject
          </button>
          <button
            type="submit"
            v-on:click="saveChange('registrar')"
            v-show="showRegistrar"
            v-bind:disabled="!newData.registrar"
          >
            Save
          </button>
          <button
            type="button"
            v-on:click.prevent="
              newData.registrar = '';
              showRegistrar = false;
            "
            v-show="showRegistrar"
          >
            Cancel
          </button>
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
          <div v-show="!showDOB" class="data">
            <span v-if="!pending.dob">{{ this.convertedDateTime }}</span>
            <span v-if="pending.dob" class="newValue">{{
              this.pendingConvertedDateTime
            }}</span>
          </div>
          <input type="date" v-model="newData.dob" v-show="showDOB" />
        </td>
        <td class="editButtons">
          <button
            type="button"
            v-on:click.prevent="
              showDOB = true;
              newData.dob = camper.dob;
            "
            v-show="!showDOB"
            v-bind:disabled="pending.dob"
            v-if="!($store.state.user.role == 'admin' && pending.dob)"
          >
            Edit
          </button>
          <button
            type="button"
            v-on:click="rejectRequest('dob')"
            v-if="$store.state.user.role == 'admin' && pending.dob"
          >
            Reject
          </button>
          <button
            type="submit"
            v-on:click="saveChange('dob')"
            v-show="showDOB"
            v-bind:disabled="!newData.dob"
          >
            Save
          </button>
          <button
            type="button"
            v-on:click.prevent="
              newData.dob = '';
              showDOB = false;
            "
            v-show="showDOB"
          >
            Cancel
          </button>
        </td>
      </tr>
      <tr class="row">
        <td>Payment:</td>
        <td>
          <div class="data">
            <span
              v-if="
                !showPayment &&
                !camper.paymentStatus &&
                pending.paymentStatus == undefined
              "
              >Unpaid</span
            >
            <span
              v-if="
                camper.paymentStatus &&
                !showPayment &&
                pending.paymentStatus == undefined
              "
              >Paid</span
            >
            <span
              v-if="
                !showPayment &&
                !pending.paymentStatus &&
                pending.paymentStatus != undefined
              "
              class="newValue"
              >Unpaid</span
            >
            <span v-if="pending.paymentStatus && !showPayment" class="newValue"
              >Paid</span
            >
          </div>
          <div v-show="showPayment">
            <label for="unpaid">Paid</label>
            <input
              type="checkbox"
              id="unpaid"
              v-model="newData.paymentStatus"
            />
          </div>
        </td>
        <td class="editButtons">
          <button
            type="button"
            v-on:click.prevent="
              showPayment = true;
              newData.paymentStatus = camper.paymentStatus;
            "
            v-show="!showPayment"
            v-bind:disabled="pending.paymentStatus != undefined"
            v-if="!($store.state.user.role == 'admin' && pending.paymentStatus)"
          >
            Edit
          </button>
          <button
            type="button"
            v-on:click="rejectRequest('payment_status')"
            v-if="$store.state.user.role == 'admin' && pending.paymentStatus"
          >
            Reject
          </button>
          <button
            type="submit"
            v-on:click="saveChange('payment_status')"
            v-show="showPayment"
            v-bind:disabled="newData.paymentStatus == camper.paymentStatus"
          >
            Save
          </button>
          <button
            type="button"
            v-on:click.prevent="
              newData.paymentStatus = '';
              showPayment = false;
            "
            v-show="showPayment"
          >
            Cancel
          </button>
        </td>
      </tr>
      <tr class="row">
        <td>Status:</td>
        <td>
          <div class="data">
            <span
              v-if="
                !showActive &&
                pending.activeStatus == undefined &&
                camper.activeStatus
              "
              >Active</span
            >
            <span
              v-if="
                !showActive &&
                pending.activeStatus == undefined &&
                !camper.activeStatus
              "
              >Inactive</span
            >
            <span v-if="pending.activeStatus && !showActive" class="newValue"
              >Active</span
            >
            <span
              v-if="
                !pending.activeStatus &&
                !showActive &&
                pending.activeStatus != undefined
              "
              class="newValue"
              >Inactive</span
            >
          </div>
          <div v-show="showActive">
            <label for="Active">Active</label>
            <input type="checkbox" id="active" v-model="newData.activeStatus" />
          </div>
        </td>
        <td class="editButtons">
          <button
            type="button"
            v-on:click.prevent="showActive = true"
            v-show="!showActive"
            v-bind:disabled="pending.activeStatus != undefined"
            v-if="!($store.state.user.role == 'admin' && pending.activeStatus)"
          >
            Edit
          </button>
          <button
            type="button"
            v-on:click="rejectRequest('active_status')"
            v-if="$store.state.user.role == 'admin' && pending.activeStatus"
          >
            Reject
          </button>
          <button
            type="submit"
            v-on:click="saveChange('active_status')"
            v-show="showActive"
            v-bind:disabled="newData.activeStatus == camper.activeStatus"
          >
            Save
          </button>
          <button
            type="button"
            v-on:click.prevent="
              newData.activeStatus = '';
              showActive = false;
            "
            v-show="showActive"
          >
            Cancel
          </button>
        </td>
      </tr>
      <tr class="row">
        <td>Family:</td>
        <td>
          <div v-show="!showFamily" class="data">
            <router-link
              v-bind:to="{
                name: 'family',
                params: { familyId: camper.familyId },
              }"
              v-if="!pending.familyId"
              >{{ this.camper.familyId }}--{{
                this.camper.familyName
              }}</router-link
            >
            <router-link
              v-bind:to="{ name: 'family', params: { familyId: newFamilyId } }"
              v-if="pending.familyId"
              class="newValue"
              >{{ this.newFamilyId }}--{{ this.newFamilyName }}</router-link
            >
          </div>
          <select v-model="newData.familyId" v-show="showFamily">
            <option v-for="f in $store.state.families" v-bind:key="f.familyId">
              {{ f.familyId }}--{{ f.fullName }}
            </option>
          </select>
        </td>
        <td class="editButtons">
          <button
            type="button"
            v-on:click.prevent="showFamily = true"
            v-show="!showFamily"
            v-bind:disabled="pending.familyId"
            v-if="!($store.state.user.role == 'admin' && pending.familyId)"
          >
            Edit
          </button>
          <button
            type="button"
            v-on:click="rejectRequest('family_id')"
            v-if="$store.state.user.role == 'admin' && pending.familyId"
          >
            Reject
          </button>
          <button
            type="submit"
            v-on:click="saveChange('family_id')"
            v-show="showFamily"
            v-bind:disabled="!newData.familyId"
          >
            Save
          </button>
          <button
            type="button"
            v-on:click.prevent="
              newData.familyId = '';
              showFamily = false;
            "
            v-show="showFamily"
          >
            Cancel
          </button>
        </td>
      </tr>
      <tr class="row">
        <td>Allergies:</td>
        <td>
          <ul
            class="data"
            v-if="!showAllergies && (allergies != 'None' || pending.allergies)"
          >
            <li
              v-show="!showAllergies && !pending.allergies"
              v-for="line in allergies"
              v-bind:key="line"
            >
              {{ line }}
            </li>
            <li
              v-show="!showAllergies && pending.allergies"
              v-for="line in pending.allergies"
              v-bind:key="line"
              class="newValue"
            >
              {{ line }}
            </li>
          </ul>
          <span
            v-show="
              !showAllergies &&
              allergies == 'None' &&
              !pending.allergies &&
              pending.allergies != ''
            "
            >None</span
          >
          <span
            class="newValue"
            v-show="
              !showAllergies && allergies == 'None' && pending.allergies == ''
            "
            >None</span
          >
          <div v-show="showAllergies">
            <input type="text" v-model="newData.allergies" />
          </div>
        </td>
        <td class="editButtons">
          <button
            type="button"
            v-on:click.prevent="showAllergies = true"
            v-show="!showAllergies"
            v-bind:disabled="pending.allergies"
            v-if="!($store.state.user.role == 'admin' && pending.allergies)"
          >
            Edit
          </button>
          <button
            type="button"
            v-on:click="rejectRequest('allergies')"
            v-if="$store.state.user.role == 'admin' && pending.allergies"
          >
            Reject
          </button>
          <button
            type="submit"
            v-on:click="saveChange('allergies')"
            v-show="showAllergies"
          >
            Save
          </button>
          <button
            type="button"
            v-on:click.prevent="
              newData.allergies = [];
              showAllergies = false;
            "
            v-show="showAllergies"
          >
            Cancel
          </button>
        </td>
      </tr>
      <tr class="row">
        <td>Medications:</td>
        <td>
          <ul
            class="data"
            v-if="
              !showMedications &&
              (medications != 'None' || pending.medications) &&
              typeof medications != String
            "
          >
            <li
              v-show="!showMedications && !pending.medications"
              v-for="line in medications"
              v-bind:key="line"
            >
              {{ line }}
            </li>
            <li
              v-show="!showMedications && pending.medications"
              v-for="line in pending.medications"
              v-bind:key="line"
              class="newValue"
            >
              {{ line }}
            </li>
          </ul>
          <span
            v-if="
              medications == 'None' &&
              !pending.medications &&
              pending.medications != '' &&
              !showMedications
            "
            >None</span
          >
          <span
            class="newValue"
            v-show="
              !showMedications &&
              medications == 'None' &&
              pending.medications == ''
            "
            >None</span
          >
          <div v-show="showMedications">
            <input type="text" v-model="newData.medications" />
          </div>
        </td>
        <td class="editButtons">
          <button
            type="button"
            v-on:click.prevent="showMedications = true"
            v-show="!showMedications"
            v-bind:disabled="pending.medications"
            v-if="!($store.state.user.role == 'admin' && pending.medications)"
          >
            Edit
          </button>
          <button
            type="button"
            v-on:click="rejectRequest('medications')"
            v-if="$store.state.user.role == 'admin' && pending.medications"
          >
            Reject
          </button>
          <button
            type="submit"
            v-on:click="saveChange('medications')"
            v-show="showMedications"
          >
            Save
          </button>
          <button
            type="button"
            v-on:click.prevent="
              newData.medications = [];
              showMedications = false;
            "
            v-show="showMedications"
          >
            Cancel
          </button>
        </td>
      </tr>
      <tr class="row">
        <td>Special Needs:</td>
        <td>
          <ul
            class="data"
            v-if="
              !showSpecial &&
              (specialNeeds != 'None' || pending.specialNeeds) &&
              typeof specialNeeds != String
            "
          >
            <li
              v-show="!showSpecial && !pending.specialNeeds"
              v-for="line in specialNeeds"
              v-bind:key="line"
            >
              {{ line }}
            </li>
            <li
              v-show="!showSpecial && pending.specialNeeds"
              v-for="line in pending.specialNeeds"
              v-bind:key="line"
              class="newValue"
            >
              {{ line }}
            </li>
          </ul>
          <span
            v-if="
              specialNeeds == 'None' &&
              !pending.specialNeeds &&
              pending.specialNeeds != '' &&
              !showSpecial
            "
            >None</span
          >
          <span
            class="newValue"
            v-show="
              !showSpecial &&
              specialNeeds == 'None' &&
              pending.specialNeeds == ''
            "
            >None</span
          >
          <div v-show="showSpecial">
            <input type="text" v-model="newData.specialNeeds" />
          </div>
        </td>
        <td class="editButtons">
          <button
            type="button"
            v-on:click.prevent="showSpecial = true"
            v-show="!showSpecial"
            v-bind:disabled="pending.specialNeeds"
            v-if="!($store.state.user.role == 'admin' && pending.specialNeeds)"
          >
            Edit
          </button>
          <button
            type="button"
            v-on:click="rejectRequest('special_needs')"
            v-if="$store.state.user.role == 'admin' && pending.specialNeeds"
          >
            Reject
          </button>
          <button
            type="submit"
            v-on:click="saveChange('special_needs')"
            v-show="showSpecial"
          >
            Save
          </button>
          <button
            type="button"
            v-on:click.prevent="
              newData.specialNeeds = [];
              showSpecial = false;
            "
            v-show="showSpecial"
          >
            Cancel
          </button>
        </td>
      </tr>
    </table>
    <h2>Additional Information</h2>
    <table id="adHoc">
      <tr>
        <td></td>
        <td></td>
        <td>
          <button
            id="newNotes"
            type="button"
            v-on:click.prevent="showNotes = true"
            v-show="!showNotes"
            v-bind:disabled="pending.notes"
            v-if="!($store.state.user.role == 'admin' && pending.notes)"
          >
            Add Additional Info
          </button>
        </td>
      </tr>
      <tr v-for="note in camper.notes" v-bind:key="note.noteId">
        <td>{{ note.parameter }}</td>
        <td>{{ note.value }}</td>
        <td></td>
      </tr>
      <tr>
        <td v-if="showNotes"><input type="text" v-model="note.parameter" placeholder="Parameter" /></td>
        <td v-if="showNotes"><input type="text" v-model="note.value" placeholder="Value" /></td>
        <td>
          <span class="editButtons">
            <button
              type="button"
              v-on:click="rejectRequest('ad_hoc_notes')"
              v-if="$store.state.user.role == 'admin' && pending.notes"
            >
              Reject
            </button>
            <button
              type="submit"
              v-on:click="saveChange('ad_hoc_notes')"
              v-show="showNotes"
              v-bind:disabled="!(note.parameter && note.value)"
            >
              Save
            </button>
            <button
              type="button"
              v-on:click.prevent="
                this.note = {};
                showNotes = false;
              "
              v-show="showNotes"
            >
              Cancel
            </button>
          </span>
        </td>
      </tr>
    </table>
    <span class="adminButtons" v-if="this.$store.state.user.role == 'admin'">
      <button
        type="submit"
        v-bind:disabled="!requests[0]"
        v-on:click="approveRequests()"
      >
        Approve All
      </button>
      <button
        type="submit"
        v-bind:disabled="!requests[0]"
        v-on:click="rejectAllRequests()"
      >
        Reject All
      </button>
    </span>
  </section>
</template>

<script>
import UpdateService from "../services/UpdateService.js";
import CamperService from "../services/CamperService.js";

export default {
  data() {
    return {
      newData: {},
      pending: {},
      updateToSend: {},
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
      showNotes: false,
      requests: [],
      note: {},
    };
  },
  props: {
    camper: Object,
  },
  computed: {
    allergies() {
      if (typeof this.camper.allergies == "string") {
        return this.camper.allergies.split(",");
      }
      return this.camper.allergies;
    },
    medications() {
      if (typeof this.camper.medications == "string") {
        return this.camper.medications.split(",");
      }
      return this.camper.medications;
    },
    specialNeeds() {
      if (typeof this.camper.specialNeeds == "string") {
        return this.camper.specialNeeds.split(",");
      }
      return this.camper.specialNeeds;
    },
    newFamilyId() {
      let newFamily = this.newData.familyId.split("--");
      return parseInt(newFamily[0]);
    },
    newFamilyName() {
      let newFamily = this.newData.familyId.split("--");
      return newFamily[1];
    },
    convertedDateTime() {
      let dob = new Date(this.camper.dob);
      let month = dob.getMonth() + 1;
      let day = dob.getDate();
      let year = dob.getFullYear();
      let formattedDob = `${month}/${day}/${year}`;
      return formattedDob;
    },
    newConvertedDateTime() {
      if (this.newData.dob) {
        let dob = new Date(this.newData.dob);
        let month = dob.getMonth() + 1;
        let day = dob.getDate();
        let year = dob.getFullYear();
        let formattedDob = `${month}/${day}/${year}`;
        return formattedDob;
      }
      return "";
    },
    pendingConvertedDateTime() {
      if (this.pending.dob) {
        let dob = new Date(this.pending.dob);
        let month = dob.getMonth() + 1;
        let day = dob.getDate();
        let year = dob.getFullYear();
        let formattedDob = `${month}/${day}/${year}`;
        return formattedDob;
      }
      return "";
    },
  },
  methods: {
    saveChange(formName) {
      this.updateToSend.fieldToBeChanged = formName;
      switch (formName) {
        case "first_name":
          this.updateToSend.oldData = this.camper.firstName;
          this.updateToSend.newData = this.newData.firstName;
          this.pending.firstName = this.newData.firstName;
          this.camper.firstName = this.newData.firstName;
          this.showFirst = false;
          break;
        case "last_name":
          this.updateToSend.oldData = this.camper.lastName;
          this.updateToSend.newData = this.newData.lastName;
          this.pending.lastName = this.newData.lastName;
          this.camper.lastName = this.newData.lastName;
          this.showLast = false;
          break;
        case "dob":
          this.updateToSend.oldData = this.camper.dob;
          this.updateToSend.newData = this.newData.dob;
          this.pending.dob = this.newData.dob;
          this.camper.dob = this.newData.dob;
          this.showDOB = false;
          break;
        case "allergies":
          if (this.newData.allergies == undefined) {
            this.newData.allergies = "None";
          }
          this.updateToSend.oldData = this.camper.allergies.toString();
          this.updateToSend.newData = this.newData.allergies.toString();
          if (this.pending.allergies != "None") {
            this.pending.allergies = this.newData.allergies.split(",");
          }
          this.camper.allergies = this.newData.allergies;
          this.showAllergies = false;
          break;
        case "medications":
          if (this.newData.medications == undefined) {
            this.newData.medications = "None";
          }
          this.updateToSend.oldData = this.camper.medications.toString();
          this.updateToSend.newData = this.newData.medications.toString();
          if (this.pending.medications != "None") {
            this.pending.medications = this.newData.medications.split(",");
          }
          this.camper.medications = this.newData.medications;
          this.showMedications = false;
          break;
        case "family_id":
          this.updateToSend.oldData = this.camper.familyId;
          this.updateToSend.newData = this.newData.familyId;
          this.pending.familyId = this.newData.familyId;
          this.camper.familyId = this.newFamilyId;
          this.showFamily = false;
          break;
        case "special_needs":
          if (this.newData.specialNeeds == undefined) {
            this.newData.specialNeeds = "None";
          }
          this.updateToSend.oldData = this.camper.specialNeeds.toString();
          this.updateToSend.newData = this.newData.specialNeeds.toString();
          if (this.pending.specialNeeds != "None") {
            this.pending.specialNeeds = this.newData.specialNeeds.split(",");
          }
          this.camper.specialNeeds = this.newData.specialNeeds;
          this.showSpecial = false;
          break;
        case "payment_status":
          this.updateToSend.oldData = this.camper.paymentStatus;
          this.updateToSend.newData = this.newData.paymentStatus;
          this.pending.paymentStatus = this.newData.paymentStatus;
          this.camper.paymentStatus = this.pending.paymentStatus;
          this.showPayment = false;
          break;
        case "registrar":
          this.updateToSend.oldData = this.camper.registrar;
          this.updateToSend.newData = this.newData.registrar;
          this.pending.registrar = this.newData.registrar;
          this.camper.registrar = this.newData.registrar;
          this.showRegistrar = false;
          break;
        case "active_status":
          this.updateToSend.oldData = this.camper.activeStatus;
          this.updateToSend.newData = this.newData.activeStatus;
          this.pending.activeStatus = this.newData.activeStatus;
          this.camper.activeStatus = this.pending.activeStatus;
          this.showActive = false;
          break;
        case "ad_hoc_notes":
          if(this.camper.notes != undefined)
          {
            this.updateToSend.oldData = this.camper.notes.toString();
          }
          else
          {
            this.updateToSend.oldData = '';
          }
          this.updateToSend.newData = this.note.parameter + ',' + this.note.value;
          this.pending.notes = this.note;
          if(this.camper.notes != undefined)
          {
            this.camper.notes.push(this.note);
          }
          else
          {
            this.camper.notes = [];
            this.camper.notes.push(this.note);
          }
          this.showNotes = false;
          break;
      }
      this.camper.allergies = this.camper.allergies.toString();
      this.camper.medications = this.camper.medications.toString();
      this.camper.specialNeeds = this.camper.specialNeeds.toString();
      this.camper.notes = this.camper.notes.toString();
      this.logChanges(formName);
    },
    convertToPending(data) {
      if (data.status == "Pending") {
        this.requests.push(data.requestId);
        switch (data.fieldToBeChanged) {
          case "first_name":
            this.pending.firstName = data.newData;
            break;
          case "last_name":
            this.pending.lastName = data.newData;
            break;
          case "dob":
            this.pending.dob = data.newData;
            break;
          case "registrar":
            this.pending.registrar = data.newData;
            break;
          case "allergies":
            data.newData
              ? (this.pending.allergies = data.newData.split(","))
              : (this.pending.allergies = "");
            break;
          case "medications":
            data.newData
              ? (this.pending.medications = data.newData.split(","))
              : (this.pending.medications = "");
            break;
          case "family_id":
            this.pending.familyId = data.newData;
            break;
          case "special_needs":
            data.newData
              ? (this.pending.specialNeeds = data.newData.split(","))
              : (this.pending.specialNeeds = "");
            break;
          case "active_status":
            this.pending.activeStatus = data.newData;
            break;
          case "payment_status":
            this.pending.paymentStatus = data.newData;
            break;
          case "parameter":
            this.pending.notes.parameter = data.newData;
            break;
          case "value":
            this.pending.notes.parameter = data.newData;
            break;
        }
      }
    },
    createUpdate() {
      this.updateToSend.action = "Update";
      this.updateToSend.camperCode = parseInt(this.$route.params.camperCode);
      this.updateToSend.requestor = this.$store.state.user.username;
    },
    approveRequests() {
      UpdateService.approveAllRequests(this.requests)
        .then((response) => {
          console.log("All Requests Approved", response.data);
          this.pending = {};
          this.$router.go();
        })
        .catch((response) => {
          console.error("Problem approving request", response);
        });
    },
    rejectRequest(formName) {
      let request = this.requests.find((r) => r.fieldToBeChanged == formName);
      if (!request) {
        console.error("Cannot Find Request", request);
        return "";
      }
      UpdateService.rejectRequest(request.requestId)
        .then((response) => {
          console.log("Request Rejected", response.data);
          this.$router.go();
        })
        .catch((response) => {
          console.error("Problem Rejecting Request", response);
        });
    },
    rejectAllRequests() {
      let requestIds = [];
      this.requests.forEach((r) => requestIds.push(r.requestId));
      UpdateService.rejectAllRequests(requestIds)
        .then((response) => {
          console.log("All Requests Rejected", response.data);
          this.pending = {};
          this.$router.go();
        })
        .catch((response) => {
          console.error("Problem Rejecting Request", response);
        });
    },
    logChanges(formName) {
      this.createUpdate(formName);
      if(formName == 'ad_hoc_notes')
      {
        CamperService.logNotes(this.camper.notes.parameter)
        .then(response => {
          console.log('Logged new note parameter', response.data);
          CamperService.logNotes(this.camper.notes.value)
        .then(response => {
          console.log('Logged new note value', response.data);
        })
        .catch(response => {
          console.error ('Problem logging note', response);
        })
        })
        .catch(response => {
          console.error ('Problem logging note', response);
        })
      }
      else
      {
        CamperService.logChanges(this.updateToSend)
          .then((response) => {
            console.log("Logged changes", response.data);
          })
          .catch((response) => {
            console.warn("Problem logging changes", response);
          });
      }
    },
  },
  created() {
    this.newData = {};
    UpdateService.getUpdatesByCamperCode(this.$route.params.camperCode)
      .then((response) => {
        console.log("Got updates", response.data);
        this.requests = [];
        response.data.forEach((u) => this.convertToPending(u));
        this.$forceUpdate();
      })
      .catch((response) => {
        console.error("Problem getting updates", response);
      });
  },
};
</script>

<style lang="scss" scoped>
@import "../styles/colors.scss";

table {
  display: block;
  width: 120%;
  margin: 1%;
  font-size: 1.4rem;
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
  font-family: "Lora", serif;
  box-shadow: 1px 0.5px 0px $textLight;
  width: 30%;
}
button:disabled {
  background-color: $secondary;
  color: white;
}
tr {
  display: flex;
  align-items: center;
  padding: 0% 2%;
  margin: 1% 0%;
  min-height: 66px;
}
.row {
  padding: 0% 2%;
  width: 80%;
}
td {
  width: 70%;
  font-size: 120%;
}
input,
select {
  font-family: "Lora", serif;
  border: 1px dotted $highlight;
  border-radius: 10px;
  background-color: $textDark;
  color: $textLight;
  width: 60%;
  font-size: 1em;
}
input::-webkit-input-placeholder {
  color: $textLight;
}
.newValue {
  color: $highlight;
}
tr > td:nth-child(3) {
  width: 20%;
}
tr > td:nth-child(2) {
  width: 40%;
  margin-left: 2%;
  text-align: justify;
}
tr > td:first-child {
  width: 40%;
  margin-right: 2%;
}
ul {
  padding: 1%;
}
.editButtons {
  display: flex;
}
.editButtons button {
  width: 100%;
}
.adminButtons {
  display: inline-flex;
  width: 100%;
  justify-content: center;
  margin: 2% 0%;
}
a {
  color: $textDark;
  text-decoration: none;
}
#adHoc {
  width: 100%;
}
#newNotes {
  width: 100%;
  font-size: 66%;
}
</style>