import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */
const currentToken = localStorage.getItem('token')

let currentUser = null;
if ((localStorage.getItem('user') && localStorage.getItem('user') != 'undefined')) {
  currentUser = JSON.parse(localStorage.getItem('user'));
}

if (currentToken && currentToken != 'undefined') {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}

export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {}, // If a user is an admin, their user.role will be 'admin'
    families: [],
    campers: [],
    filteredCampers: [],
    states: ["AL", "AK", "AR", "AZ", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA",
      "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH",
      "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VA",
      "VT", "WA", "WI", "WV", "WY"],
  },
  mutations: {
    SET_AUTH_TOKEN(state, token) {
      state.token = token;
      localStorage.setItem('token', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_USER(state, user) {
      state.user = user;
      localStorage.setItem('user', JSON.stringify(user));
    },
    SET_CAMPER_LIST(state, data) {
      state.campers = data;
      state.campers.forEach(c => {
        let family = state.families.find(f => f.familyId == c.familyId)
        c.familyName = family.fullName;
        let birthYear = new Date(c.dob).getFullYear()
        let currentYear = new Date().getFullYear();
        c.age = currentYear - birthYear;
        if (!c.medications[0]) {
          c.medications = 'None';
        }
        else {
          c.medications = c.medicationsList;
        }
        if (!c.allergies[0]) {
          c.allergies = 'None';
        }
        else {
          c.allergies = c.allergiesList;
        }
        if (!c.specialNeeds[0]) {
          c.specialNeeds = 'None';
        }
        else {
          c.specialNeeds = c.specialNeedsList;
        }
        c.missingData = [];
        if (!c.lastName) {
          c.missingData.push('Last Name');
        }
        if (!c.firstName) {
          c.missingData.push('First Name');
        }
        if (!c.camperCode) {
          c.missingData.push('Camper Code');
        }
        if (!c.dob) {
          c.missingData.push('Date of Birth');
        }
        if (!c.age) {
          c.missingData.push('Age');
        }
        if (c.paymentStatus == undefined) {
          c.missingData.push('Payment Status');
        }
        if (!c.registrar) {
          c.missingData.push('Registrar');
        }
        if (!c.familyId) {
          c.missingData.push('Family');
        }
        if (c.activeStatus == undefined) {
          c.missingData.push('Active Status');
        }
        if (!c.missingData) {
          c.missingData.push('None');
        }
      });
    },
    SET_FILTERED_CAMPERS(state, data){
      state.filteredCampers = data;
    },
    SET_CAMPERS(state, data) {
      state.campers = data;
    },
    SET_FAMILY_LIST(state, data) {
      state.families = data;
      state.families.forEach(f => {
        f.campers = state.campers.filter(c => c.familyId == f.familyId)
      })
    },
    LOGOUT(state) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      state.token = '';
      state.user = {};
      axios.defaults.headers.common = {};
    },
    ADD_CAMPER(state, camper) {
      state.campers.push(camper);
    },
    DELETE_CAMPER(state, camperId) {
      state.campers.splice(state.campers.indexOf(camperId), 1);
    },
  }
})
