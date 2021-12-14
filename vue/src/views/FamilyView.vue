<template>
  <article>
    <family-info v-bind:family="family" />
    <family-change-log v-bind:updates="updates" />
  </article>
</template>

<script>
import FamilyChangeLog from "../components/FamilyChangeLog.vue";
import FamilyInfo from "../components/FamilyInfo.vue";
import FamilyService from "../services/FamilyService.js";
import UpdateService from "../services/UpdateService.js";
import CamperService from "../services/CamperService.js";

export default {
  data() {
    return {
      updates: [],
    };
  },
  components: {
    FamilyInfo,
    FamilyChangeLog,
  },
  computed: {
    family() {
      return this.$store.state.families.find(
        (f) => f.familyId == this.$route.params.familyId
      );
    },
  },
  created() {
    CamperService.getAllCampers()
      .then((response) => {
        console.log("Got all campers", response.data);
        this.$store.commit("SET_CAMPERS", response.data);
        FamilyService.getAllFamilies()
          .then((response) => {
            console.log("Got family", response.data);
            this.$store.commit("SET_FAMILY_LIST", response.data);
            this.$forceUpdate();
          })
          .catch((response) => {
            console.error("Problem getting family", response);
          });
      })
      .catch((response) => {
        console.error("Problem getting all campers", response);
      });
    UpdateService.getUpdatesByFamilyId(this.$route.params.familyId)
      .then((response) => {
        console.log("Got all updates", response.data);
        this.updates = response.data;
        this.$forceUpdate();
      })
      .catch((response) => {
        console.error("Could not get updates", response);
      });
  },
};
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

article {
  background-color: $secondary;
  padding: 2%;
  border-radius: 20px;
  border: 2px solid $highlight;
  box-shadow: 3px 2px 1px $textLight;
}
</style>