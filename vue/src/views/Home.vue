<template>
  <div class="home">
    <h1>Home</h1>
    <p>Welcome to CAMP!<br>CAMP is a camp attendee management program meant to simplify the hassle of adding and managing your summer camp attendees.</p>
    <router-link v-bind:to="{ name: 'attendee', params: {attendeeId: '2'}}">Camper Example</router-link>
    <router-link v-bind:to="{ name: 'attendeeList'}">Attendee List Example</router-link>
  </div>
</template>

<script>
export default {
  name: "home",
  created(){
    if(!this.$store.state.user.token)
    {
      this.$route.push('/login')
    }
  }
};
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

.home{
  display: flex;
  flex-direction: column;
  background-color: $textLight;
  border: 2px solid $highlight;
  border-radius: 20px;
  padding: 10px;
}
a{
  margin-top: 5px;
  color: $highlight;
}
</style>
