<template>
  <article>
      <family-list />
  </article>
</template>

<script>
import FamilyList from "../components/FamilyList.vue"

export default {
  components: { FamilyList },

}
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

article {
  background-color: $textLight;
  padding: 1%;
  border-radius: 20px;
  border: 2px solid $textDark;
  box-shadow: 3px 2px 1px $secondary;
}
</style>