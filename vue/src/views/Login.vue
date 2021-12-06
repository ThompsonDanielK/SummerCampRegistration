<template>
  <div id="login" class="text-center">
    <form class="form-signin" @submit.prevent="login">
      <h1 class="h3 mb-3 font-weight-normal">Please Sign In</h1>
      <div class="alert alert-danger" role="alert" v-if="invalidCredentials">
        Invalid username and password!
      </div>

      <div class="alert alert-danger" role="alert" v-if="networkError">
        Network error!
      </div>

      <div
        class="alert alert-success"
        role="alert"
        v-if="this.$route.query.registration">
        Thank you for registering, please sign in.
      </div>
      <div class="form-group">
        <input
          type="text"
          id="username"
          class="form-control"
          placeholder="Username"
          v-model="user.username"
          required
          autofocus />
      </div>
      <div class="form-group">
        <input
          type="password"
          id="password"
          class="form-control"
          placeholder="Password"
          v-model="user.password"
          required />
      </div>
      <div class="form-group">
        <router-link :to="{ name: 'register' }">Need an account?</router-link>
      </div>
      <div class="form-group">
      <button class="btn btn-primary" type="submit">Sign in</button>
      </div>
    </form>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login",
  components: {},
  data() {
    return {
      user: {
        username: "",
        password: "",
      },
      invalidCredentials: false,
      networkError: false,
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then((response) => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push({ name: "home" });
          }
        })
        .catch((error) => {
          const response = error.response;

          if (response == null || response.status === 500) {
            this.networkError = true;
          }

          else if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    },
  },
};
</script>

<style scoped lang="scss">
@import '../styles/colors.scss';

#login{
  color: $textDark;
  padding: 10px;
  border-radius: 10px;
  background-color: $textLight;
  display: flex;
  justify-content: center;
  width: 50%;
  margin: 0 20%;
  border: 2px solid $highlight;
}
.form-group{
  display: flex;
  justify-content: center;
}
.form-group input, button, a{
  margin: 10px;
  background-color: $textDark;
  color: $textLight;
  border: 2px solid $highlight;
  border-radius: 5px;
}
input::-webkit-input-placeholder{
  color: white;
  font-family: 'Russo One', sans-serif;
}
a{
  width: 70%;
  text-align: center;
}
button{
  font-size: 1.2em;
  font-family: 'Russo One', sans-serif;
}
</style>
