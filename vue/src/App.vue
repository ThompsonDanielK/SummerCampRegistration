<!-- Main Application Template. 
     Note that you have classes from bootstrap available to you.
     See https://getbootstrap.com/docs/4.5/getting-started/introduction/ for reference on bootstrap -->
<template>
  <div id="app" class="container">
    <!-- If you start to get random styling you don't like, remove container from this div -->
    <div class="nav">
      <img src="../images/logo.png" />
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
        >&nbsp;|&nbsp;Register</router-link
      >
      <router-link
        class="nav-item"
        v-bind:to="{ name: 'login' }"
        v-if="!$store.state.token"
      >
        &nbsp;|&nbsp;Login
      </router-link>
      <router-link
        class="nav-item"
        v-bind:to="{ name: 'logout' }"
        v-if="$store.state.token"
      >
        &nbsp;|&nbsp;Logout
      </router-link>
      </div>
    </div>

    <div class="page">
      <router-view />
    </div>
  </div>
</template>

<script>
export default {};
</script>

<!-- Application-Wide Styles go here. 
     Any valid CSS or SCSS is allowable here. See https://sass-lang.com/documentation/style-rules for info on SCSS -->
<style>
.container {
  display: grid;
  grid-template-areas: "nav" "page";
  margin: 0;
  padding: 0;
  max-width: 100%;
}
.page {
  background-color: #a2a2a2;
  color: #52423a;
  grid-area: page;
  padding: 5px 20px;
}
.nav {
  background-color: #7a6d61;
  color: #395136;
  display: flex;
  justify-content: space-evenly;
  align-items: center;
  grid-area: nav;
  padding: 5px 30px;
}
.nav img {
  height: 10rem;
}
#navBar{
  border: 1px solid #395136;
  border-radius: 10px;
  padding: 5px;
}
.nav a {
  color: #395136;
  text-decoration: none;
  font-size: 1.3em;
}
</style>