<template>
  <section>
    <all-family-change-log id="history"></all-family-change-log>
  </section>
</template>

<script>
import AllFamilyChangeLog from "../components/AllFamilyChangeLog.vue";
export default {
  components: { AllFamilyChangeLog },
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