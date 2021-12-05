<template>
  <div class="home">
    <h1>Home</h1>
    <p>Your content goes here. Make it look nice.</p>
    <router-link v-bind:to="{ name: 'camper'}"> Campers </router-link>
  </div>
</template>

<script>
export default {
  name: "home",
};
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

a{
  color: $highlight;
}
</style>
