<template>
  <header class="nav">
    <img id="logo" src="../../images/logo.png" alt="C.A.M.P. Logo" />
    <div id="navBar">
      <router-link class="nav-item" v-bind:to="{ name: 'home' }">
        <i class="fas fa-home"></i>
        <!-- This is a font awesome icon -->
        Home
      </router-link>
      <router-link
        class="nav-item"
        v-bind:to="{ name: 'register' }"
        v-if="!$store.state.token && changedOurMinds"
        >Register</router-link
      >
      <router-link
        class="nav-item"
        v-bind:to="{ name: 'login' }"
        v-if="!$store.state.token"
        >Login
      </router-link>
      <router-link
        class="nav-item"
        v-bind:to="{ name: 'logout' }"
        v-if="$store.state.token"
      >
        Logout
      </router-link>
    </div>
  </header>
</template>

<script>
export default {
  data() {
    return {
      changedOurMinds: false,
    };
  },
};
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

.nav {
  background-color: $secondary;
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
  padding: 0;
  background-image: url("../../images/DanHeader.jpg");
  background-size: 100%;
  background-repeat: no-repeat;
  background-position: center;
}
.nav #logo {
  height: 13%;
  width: 13%;
}
.nav .nav-item {
  display: flex;
  justify-content: space-around;
  color: $textLight;
  text-shadow: 3px 2px 1px black;
  text-decoration: none;
  font-size: 2em;
  border: 2px solid $highlight;
  border-radius: 10px;
  text-align: center;
  background-color: $textDark;
  padding: 0% 5%;
  margin: 0% 2%;
  box-shadow: 1.5px 0.5px 0.5px black;
}
.nav #navBar {
  display: flex;
  padding-right: 20%;
  margin-bottom: 1%;
}
@media only screen and (max-width: 768px) {
  .nav-item {
    font-size: 1.4em;
  }
}
@media only screen and (max-width: 425px) {
  .nav {
    padding: 0;
    margin: 0;
  }
  .nav .nav-item {
    font-size: 1em;
  }
}
</style>