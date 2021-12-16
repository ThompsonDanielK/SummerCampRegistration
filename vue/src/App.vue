<!-- Main Application Template. 
     Note that you have classes from bootstrap available to you.
     See https://getbootstrap.com/docs/4.5/getting-started/introduction/ for reference on bootstrap -->
<template>
  <div id="app" class="container">
    <!-- If you start to get random styling you don't like, remove container from this div -->
    <nav-bar id="navBar" />
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
html {
  background: #3b5f3a;
  background: -moz-linear-gradient(top, #3b5f3a 0%, #c49c79 41%, #43281e 100%);
  background: -webkit-linear-gradient(
    top,
    #3b5f3a 0%,
    #c49c79 41%,
    #43281e 100%
  );
  background: linear-gradient(to bottom, #3b5f3a 0%, #c49c79 41%, #43281e 100%);
  background-attachment: fixed;
  background-repeat: no-repeat;
  height: 100%;
}
.container {
  display: flex;
  flex-direction: column;
  margin: 0;
  padding: 0;
}
.container .page {
  color: $main;
  margin: 2% 10%;
  color: $textDark;
  align-items: center;
  border-radius: 20px;
}
@media only screen and (max-width: 768px) {
  #navBar,
  .container div.page {
    padding: 2% 4% 2% 0%;
    margin: 1% 0%;
    box-shadow: none;
  }
}
@media only screen and (max-width: 425px) {
  #navBar,
  .container,
  .page {
    padding: 0%;
    border-radius: 0;
    margin: 1% 0%;
    box-shadow: none;
  }
}
</style>