<!-- Main Application Template. 
     Note that you have classes from bootstrap available to you.
     See https://getbootstrap.com/docs/4.5/getting-started/introduction/ for reference on bootstrap -->
<template>
  <div id="app" class="container">
    <!-- If you start to get random styling you don't like, remove container from this div -->
    <nav-bar />
    <div class="page">
      <router-view />
    </div>
    <footer></footer>
  </div>
</template>

<script>
import NavBar from "./components/NavBar.vue";
export default {
  components: {
    NavBar,
  },
};
</script>

<!-- Application-Wide Styles go here. 
     Any valid CSS or SCSS is allowable here. See https://sass-lang.com/documentation/style-rules for info on SCSS -->
<style lang="scss">
@import "./styles/colors.scss";

// Import the bootstrap styles. If you want to override Bootstrap colors, it needs to happen above this line
@import "../node_modules/bootstrap/scss/bootstrap";

.container {
  display: grid;
  grid-template-areas:
    "nav"
    "page";
  max-width: 80%;
}
.page {
  margin: 0px 10% 0px 10%;
  color: $main;
  grid-area: page;
  padding: 5px 5%;
  color: $textDark;
}
</style>