<template>
  <section>
    <button
      type="button"
      v-show="!showEmailForm"
      v-on:click="
        showEmailForm = true;
        getRecipients();
      "
    >
      Send Email to Families
    </button>
    <aside v-show="showEmailForm">
      <h3>Emailing the following families:</h3>
      <span
        v-for="recipient in filteredFamilies"
        v-bind:key="recipient.familyId"
        >{{ recipient.familyName }}</span
      >
      <input
        id="subject"
        type="text"
        v-model="email.subject"
        placeholder="Subject"
      />
      <textarea id="body" v-model="email.body" placeholder="Insert Body Here" />
      <div class="emailButtons">
      <button type="submit" v-on:click.prevent="sendEmail()">Send Email</button>
      <button type="button" v-on:click.prevent="cancelEmail()">Cancel</button>
      </div>
    </aside>
  </section>
</template>

<script>
import EmailService from '../services/EmailService.js'

export default {
  props: {
    filteredCampers: Array,
  },
  data() {
    return {
      showEmailForm: false,
      email: {
        body: "",
        subject: "",
        recipients: [],
      },
      filteredFamilies: [],
    };
  },
  methods: {
    getRecipients() {
      this.email.recipients = [];
      this.setFilteredFamilies();
      this.filteredFamilies.forEach(f => this.email.recipients.push(f.familyId));
    },
    setFilteredFamilies() {
      let familyIds = [];
      let uniqueIds = [];
      this.filteredCampers.forEach((c) => familyIds.push(c.familyId));
      familyIds.forEach((f) => {
        if (!uniqueIds.includes(f)) {
          uniqueIds.push(f);
        }
      });
      let families = [];
      uniqueIds.forEach((f) => {
        let family = {};
        let camper = this.filteredCampers.find((c) => c.familyId == f);
        family.familyId = camper.familyId;
        family.familyName = camper.familyName;
        families.push(family);
      });
      this.filteredFamilies = families;
    },
    sendEmail(){
        EmailService.sendEmail(this.email)
        .then(response => {
            console.log('Email sent', response.data)
            this.email = {};
        })
        .catch(response => {
            console.error('Could not send email', response);
        })
        this.showEmailForm = false;
    },
    cancelEmail(){
        this.email = {};
        this.showEmailForm = false;
    },
  },
};
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

section{
    width: 80%;
    margin: 0% 10%;
}
aside {
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  margin: 1%;
}
input,
button,
textarea {
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  border: 1px solid $highlight;
  font-size: 0.9rem;
  font-family: "Lora", serif;
  box-shadow: 2px 1px 1px $secondary;
  margin: 1%;
}
textarea{
    width: 70%;
}
input::placeholder,
textarea::placeholder {
  color: $textLight;
}
.emailButtons{
    width: 100%;
}
#subject{
    width: 40%;
}
</style>