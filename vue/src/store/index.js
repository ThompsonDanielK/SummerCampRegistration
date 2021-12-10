import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import CamperService from '../services/CamperService.js'
import FamilyService from '../services/FamilyService.js'

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

if(currentToken && currentToken != 'undefined') {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}

export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {}, // If a user is an admin, their user.role will be 'admin'
    families: [],
    campers: [],
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
      localStorage.setItem('user',JSON.stringify(user));
    },
    SET_CAMPER_LIST(state)
    {
      CamperService.getAllCampers()
      .then((response) => {
        state.campers = response.data;
        console.log('Got all campers', state.campers)
        FamilyService.getAllFamilies()
        .then(response => {
        state.families = response.data;
        console.log('Got all families', state.families)
        state.campers.forEach( c=> {
          let family = state.families.find(f => f.familyId == c.familyId)
          c.familyName = family.fullName;});
        })
        .catch(response => {
        console.error('Problem getting all families', response)
        });
        state.campers.forEach(c => {
        let birthYear = new Date(c.dob).getFullYear()
        let currentYear = new Date().getFullYear();
        c.age = currentYear - birthYear;
        c.paymentStatus = c.paymentStatus?'Paid':'Unpaid';
        c.activeStatus = c.activeStatus?'Active':'Inactive';
        c.missingData = [];
          if(!c.lastName)
          {
            c.missingData.push('Last Name');
          }
          if(!c.firstName)
          {
            c.missingData.push('First Name');
          }
          if(!c.camperCode)
          {
            c.missingData.push('Camper Code');
          }
          if(!c.dob)
          {
            c.missingData.push('Date of Birth');
          }
          if(!c.age)
          {
            c.missingData.push('Age');
          }
          if(!c.paymentStatus)
          {
            c.missingData.push('Payment Status');
          }
          if(!c.registrar)
          {
            c.missingData.push('Registrar');
          }
          if(!c.familyId)
          {
            c.missingData.push('Family');
          }
          if(!c.activeStatus)
          {
            c.missingData.push('Active Status');
          }
          if(!c.missingData)
          {
            c.missingData.push('None');
          }
          })
        })
      .catch((response) => {
      console.error("Problem getting all campers", response);
      });
    },
    LOGOUT(state) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      state.token = '';
      state.user = {};
      axios.defaults.headers.common = {};
    },
    ADD_CAMPER(state, camper)
    {
      state.campers.push(camper);
    },
    DELETE_CAMPER(state, camperId)
    {
      state.campers.splice(state.campers.indexOf(camperId), 1);
    },
  }
})
