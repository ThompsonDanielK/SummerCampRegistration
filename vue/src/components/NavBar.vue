<template>
  <header class="nav">
      <img id="logo" src="../../images/logo.png" />
      <div id="navBar">
      <router-link class="nav-item" v-bind:to="{ name: 'home' }">
        <i class="fas fa-home"></i>
        <!-- This is a font awesome icon -->
        Home
      </router-link>
      <router-link
        class="nav-item"
        v-bind:to="{ name: 'register' }"
        v-if="!$store.state.token"
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

}
</script>

<style scoped lang="scss">

@import '../styles/colors.scss';

.nav {
  background-color: $secondary;
  display: flex;
  justify-content: space-between;
  align-items: center;
  grid-area: nav;
  padding: 5px 5px;
  margin-top: 20px;
  border-radius: 250px;
  background-image: url("../../images/DanHeader.jpg");
  background-size: 100%;
  background-repeat: no-repeat;
  background-position: center;
}
.nav #logo{
  height: 10em;
  }
.nav a {
  color: $textLight;
  text-shadow: 3px 1px 1px black;
  text-decoration: none;
  font-size: 200%;
  border: 2px solid $textLight;
  border-radius: 10px;
  text-align: center;
  background-color: $textDark;
  padding: 0px 10px;
  margin: 0px 10px;
}
.nav #navBar{
  padding-right: 100px;
}
</style>