<template>
  <section>
    <all-campers-change-log id="history"></all-campers-change-log>
  </section>
</template>

<script>
import AllCampersChangeLog from "../components/AllCampersChangeLog.vue";
export default {
  components: { AllCampersChangeLog },
};
</script>

<style lang="scss" scoped>
@import "../styles/colors.scss";

section{
  background-color: $textLight;
  padding: 1%;
  border-radius: 20px;
  border: 2px solid $textDark;
}
</style>