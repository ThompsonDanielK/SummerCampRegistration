<template>
  <section>
    <h1>New Camper</h1>
    <form class="form-group">
      <div id="firstName">
        <label for="firstName">First Name:</label>
        <input v-model="camper.firstName" name="firstName" type="text" />
      </div>
      <div id="lastName">
        <label for="lastName">Last Name:</label>
        <input v-model="camper.lastName" name="lastName" type="text" />
      </div>
      <div id="dob">
        <label for="dob">Date of Birth:</label>
        <input v-model="camper.dob" name="dob" type="date" />
      </div>
      <div id="allergies">
        <label for="allergies">Allergies:</label>
        <textarea v-model="camper.allergies" name="allergies" />
      </div>
      <div id="medications">
        <label for="medications">Medications:</label>
        <textarea v-model="camper.medications" name="medications" />
      </div>
      <div id="specialNeeds">
        <label for="specialNeeds">Special Needs:</label>
        <textarea v-model="camper.specialNeeds" name="specialNeeds" />
      </div>
      <div id="family">
        <label for="familyId">Family:</label>
        <select v-model="camper.familyId" name="familyId">
          <option v-for="f in families" v-bind:key="f.familyId">
            {{ f.familyId }} -- {{ f.fullName }}
          </option>
        </select>
      </div>
    </form>
  </section>
</template>

<script>
import FamilyService from "../services/FamilyService.js";

export default {
  data() {
    return {
      families: [],
    };
  },
  props: {
    camper: Object,
  },
  created() {
    FamilyService.getAllFamilies()
      .then((response) => {
        console.log("Got all families", response.data);
        this.families = response.data;
      })
      .catch((response) => {
        console.error("Problem getting all families", response);
      });
  },
};
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

section {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin: 10px;
  background-color: $secondary;
  padding: 10px;
  border-radius: 10px;
  height: 100%;
}
h1 {
  margin: 10px;
}
.form-group {
  width: 100%;
  display: grid;
  grid-template-areas:
    "firstName lastName"
    "dob allergies"
    "medications specialNeeds"
    "familyList ...";
}
#firstName {
  grid-area: firstName;
}
#lastName {
  grid-area: lastName;
}
#allergies {
  grid-area: allergies;
}
#medications {
  grid-area: medications;
}
#family {
  grid-area: familyList;
}
#specialNeeds {
  grid-area: specialNeeds;
}
#dob {
  grid-area: dob;
}
div {
  display: flex;
  justify-content: space-between;
  padding: 10px;
}
input,
textarea,
select {
  margin: 10px;
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  padding-left: 10px;
  border: 1px solid $highlight;
  width: 50%;
  font-weight: bold;
  font-size: 1.2rem;
  font-family: "Russo One", sans-serif;
}
textarea {
  height: 100%;
}
</style>