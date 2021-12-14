<template>
  <div class="home">
    <h1>Home</h1>
    <p>
      Welcome to CAMP!<br />CAMP is a camp camper management program meant to
      simplify the hassle of adding and managing your summer camp campers.
    </p>
    <router-link
      v-bind:to="{ name: 'camper', params: { camperCode: '200001' } }"
      >Camper Example</router-link
    >
    <router-link v-bind:to="{ name: 'camperList' }"
      >Camper List Example</router-link
    >
    <router-link v-bind:to="{ name: 'familyList' }"
      >Family List Example</router-link
    >
    <router-link v-bind:to="{ name: 'history' }"
      >Camper Change History</router-link
    >
  </div>
</template>

<script>
export default {
  name: "home",
  created() {
    console.log(this.$store.state.user.username);
  },
};
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

.home {
  display: flex;
  flex-direction: column;
  background-color: $textLight;
  border: 2px solid $highlight;
  border-radius: 20px;
  padding: 10px;
  box-shadow: 3px 2px 1px $secondary;
}
a {
  margin-top: 5px;
  color: $highlight;
}
</style>
