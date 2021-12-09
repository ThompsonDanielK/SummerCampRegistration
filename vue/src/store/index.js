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

if(currentToken && currentToken != 'undefined') {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}

export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {}, // If a user is an admin, their user.role will be 'admin'
    families: [],
    campers: [],
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
    SET_FAMILY_LIST(state, families)
    {
      state.families = families;
      console.log('Got all families', state.families)
    },
    SET_CAMPER_LIST(state, campers)
    {
      state.campers = campers;
      console.log('Got all families', state.campers)
    },
    SET_CAMPER_AGE(state)
    {
      state.campers.forEach(c => {
        let birthYear = new Date(c.dob).getFullYear()
        let currentYear = new Date().getFullYear();
        c.age = currentYear - birthYear;
      })
      console.log("Set all camper's age", state.campers)
    },
    SET_CAMPER_FULL_NAME(state)
    {
      state.campers.forEach(c => {
      let family = state.families.find(f => f.familyId == c.familyId)
      c.familyName = family.fullName;
      })
      console.log("Set all camper's family name", state.campers)
    },
    SET_CAMPER_PAYMENT_STATUS(state)
    {
      state.campers.forEach(c => c.paymentStatus = c.paymentStatus?'Paid':'Unpaid')
      console.log("Set all camper's payment status", state.campers)
    },
    SET_CAMPER_ACTIVE_STATUS(state)
    {
      state.campers.forEach(c => c.activeStatus = c.activeStatus?'Active':'Inactive')
      console.log("Set all camper's active status", state.campers)
    },
    SET_CAMPER_MISSING_DATA(state)
    {
      state.campers.forEach(c => {
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
      console.log("Set all camper's missing info", state.campers)
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
