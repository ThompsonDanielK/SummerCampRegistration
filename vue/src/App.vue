<!-- Main Application Template. 
     Note that you have classes from bootstrap available to you.
     See https://getbootstrap.com/docs/4.5/getting-started/introduction/ for reference on bootstrap -->
<template>
  <div id="app" class="container">
    <!-- If you start to get random styling you don't like, remove container from this div -->
    <nav-bar id='navBar' />
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

.container {
  display: flex;
  flex-direction: column;
  margin: 10px 50px;
}
.container .page {
  color: $main;
  padding: 0px 100px;
  color: $textDark;
  align-items: flex-start;
}
</style>