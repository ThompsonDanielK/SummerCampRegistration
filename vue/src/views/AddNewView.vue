<template>
  <article>
    <add-camper-form v-bind:camper="camper" />
    <add-family-form
      v-show="showFamilyForm"
      v-bind:family="family"
      v-bind:disabled="camper.familyId"
    />
    <div class="camperButtons">
      <button
        type="submit"
        v-on:click.prevent="saveNewCamper()"
        v-bind:disabled="!filledForm"
      >
        Submit
      </button>
      <router-link v-bind:to="{ name: 'camperList' }" id="cancel"
        >Cancel</router-link
      >
      <button
        type="button"
        v-on:click.prevent="showFamilyForm = true; camper.familyId = ''"
        v-show="!showFamilyForm"
      >
        Add New Family
      </button>
    </div>
  </article>
</template>

<script>
import AddCamperForm from "../components/AddCamperForm.vue";
import AddFamilyForm from "../components/AddFamilyForm.vue";
import CamperService from "../services/CamperService.js";
import FamilyService from "../services/FamilyService.js";

export default {
  data() {
    return {
      showFamilyForm: false,
      families: [],
      camper: {},
      family: {},
    };
  },
  computed: {
    filledForm() {
      let camperFilled = this.camper.firstName && this.camper.dob;
      let familyFilled =
        this.family.fullName &&
        this.family.address &&
        this.family.city &&
        this.family.state &&
        this.family.zip &&
        (this.family.phoneNumber || this.family.emailAddress);
      return camperFilled && ((!this.showFamilyForm && this.camper.familyId) || familyFilled);
    },
  },
  components: {
    AddCamperForm,
    AddFamilyForm,
  },
  methods: {
    createUpdate(type) {
      let update = {
        action: "ADD",
        requestor: this.$store.state.user.username,
      };
      if (type == "family") {
        update.fieldToBeChanged = "family";
        update.familyId = this.family.familyId;
        update.newData = this.family.fullName;
        update.oldData = "";
      } else {
        update.fieldToBeChanged = "camper";
        if (this.camper.camperCode) {
          update.camperCode = parseInt(this.camper.camperCode);
        } else {
          update.camperCode = this.camper.camperCode;
        }
        update.newData = this.camper.firstName + " " + this.camper.lastName;
        update.oldData = "";
      }
      return update;
    },
    saveNewCamper() {
      if (!this.camper.familyId) {
        FamilyService.addFamily(this.family)
          .then((response) => {
            this.family.familyId = response.data;
            this.camper.familyId = response.data;
            let update = this.createUpdate("family");
            FamilyService.logChanges(update)
              .then((response) => {
                console.log("Family update created", response.data);
                CamperService.addCamper(this.camper)
                  .then((response) => {
                    console.log("New camper added", response.data);
                    this.camper.camperCode = response.data;
                    update = this.createUpdate("camper");
                    CamperService.logChanges(update)
                      .then((response) => {
                        console.log("Camper update created", response.data);
                        this.$router.push(`/camper/${this.camper.camperCode}`);
                      })
                      .catch((response) => {
                        console.error(
                          "Problem logging new camper update",
                          response
                        );
                      });
                  })
                  .catch((response) => {
                    console.error(
                      "Problem logging new family update",
                      response
                    );
                  });
              })
              .catch((response) => {
                console.error("Problem adding new camper", response);
              });
          })
          .catch((response) => {
            console.error("Problem adding family", response);
          });
      } else {
        this.camper.familyId = parseInt(this.camper.familyId);
        CamperService.addCamper(this.camper).then((response) => {
          console.log("New camper added", response.data);
          this.camper.camperCode = response.data;
          let update = this.createUpdate("camper");
          CamperService.logChanges(update)
            .then((response) => {
              console.log("Camper update created", response.data);
              this.$router.push(`/camper/${this.camper.camperCode}`);
            })
            .catch((response) => {
              console.error("Problem logging new camper update", response);
            });
        });
      }
    },
  },
  created() {
    this.camper.registrar = this.$store.state.user.username;
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

button,
#cancel {
  background-color: $textDark;
  color: $textLight;
  border-radius: 10px;
  border: 2px solid $highlight;
  margin: 10px;
  width: 30%;
  text-shadow: 2px 1px 1px black;
  font-weight: bold;
  font-size: 1.2rem;
  text-decoration: none;
  text-align: center;
  font-family: "Lora", serif;
}
.camperButtons {
  grid-area: buttons;
  display: flex;
  justify-content: center;
  align-items: center;
}
button:disabled {
  background-color: $secondary;
  color: black;
  text-shadow: 2px 1px 1px white;
}
</style>