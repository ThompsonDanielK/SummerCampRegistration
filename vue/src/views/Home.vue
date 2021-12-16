<template>
  <div class="home">
    <section id="summary">
    <h1>C.A.M.P.</h1>
    <p>
      Welcome to C.A.M.P. - <br />The Camp Attendee Management Program.<br />
      </p>
      <p>
      We're here to help you keep track of the campers who fill the trails at your summer camp.
    </p>
    </section>
    <section class='links' id="listCamper">
    <router-link v-bind:to="{ name: 'camperList' }">
    <img src="../../images/anderson-schmig-f7b6WX_p1N0-unsplash.jpg" />
    <h2>Maintain a List of Campers</h2>
    </router-link>
    <span>Here you can enroll new campers to your roster, view and edit their information, see the history of those changes, and even send an email to their parent(s) and/or guardian(s).</span>
    </section>
    <section class='links' id="listFamily">
    <router-link v-bind:to="{ name: 'familyList' }">
    <img src="../../images/anna-samoylova-w55SpMmoPgE-unsplash.jpg" />
    <h2>Maintain a List of Families</h2>
    </router-link>
    <span>See general information about those families and the associated campers. You may also enroll new campers from this screen as well.</span>
    </section>
    <section class='links' id="historyCamper">
    <router-link v-bind:to="{ name: 'historyCamper' }">
    <img src="../../images/artem-kniaz-DqgMHzeio7g-unsplash.jpg" />
    <h2>View Camper Change Log</h2>
    </router-link>
    <span>As your users make changes to campers' information, you can see a list of those changes here.  View historical approved & rejected changes here as well as changes pending approval.  With one click, go to those changes needing your attention.</span>
    </section>
    <section class='links' id="historyFamily">
    <router-link v-bind:to="{ name: 'historyFamily' }">
    <img src="../../images/mi-pham-xtd3zYWxEs4-unsplash.jpg" />
    <h2>View Family Change Log</h2>
    </router-link>
    <span>As your users make changes to families' information, you can see a list of those changes here.  View historical approved & rejected changes here as well as changes pending approval.  With one click, go to those changes needing your attention.</span>
    </section>
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
  display: grid;
  grid-template-areas:
  'summary summary'
  'listCamper listFamily'
  'historyCamper historyFamily';
  grid-gap: 5% 5%;
  font-size: 1.3rem;
  text-shadow: 1px 0px 1px black;
}
section{
  margin-bottom: 5%;
  background-color: $textLight;
  border: 2px solid $highlight;
  border-radius: 40px;
  padding: 1%;
  box-shadow: 2px 2px 1px rgb(82, 81, 81);
  margin: 0 auto;
}
.links{
  width: 92%;
  padding: 2% 3%;
}
img{
  width: 350px;
  height: 250px;
  border-radius: 4px;
  box-shadow: 3px 2px 1px rgb(82, 81, 81);
}
#summary{
  grid-area: summary;
  width: 45%;
  text-align: center;
}
#listCamper{
  text-align: left;
  grid-area: listCamper;
  
}
#listFamily{
  text-align: right;
  grid-area: listFamily;
}
#historyCamper{
  text-align: left;
  grid-area: historyCamper;
}
#historyFamily{
  text-align: right;
  grid-area: historyFamily;
}
a {
  margin-top: 5px;
  color: $highlight;
}
</style>
