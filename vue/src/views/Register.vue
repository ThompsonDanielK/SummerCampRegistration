<template>
  <div id="register" class="text-center">
    <form class="form-register" @submit.prevent="register">
      <h1 class="h3 mb-3 font-weight-normal">Create Account</h1>
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
      <div class="form-group">
        <input
          type="text"
          id="username"
          class="form-control"
          placeholder="Username"
          v-model="user.username"
          required
          autofocus
        />
      </div>
      <div class="form-group">
        <input
          type="password"
          id="password"
          class="form-control"
          placeholder="Password"
          v-model="user.password"
          required
        />
      </div>
      <div class="form-group">
        <input
          type="password"
          id="confirmPassword"
          class="form-control"
          placeholder="Confirm Password"
          v-model="user.confirmPassword"
          required
        />
      </div>
      <div class="form-group">
        <router-link :to="{ name: 'login' }">Have an account?</router-link>
      </div>
      <button class="btn" type="submit">Create Account</button>
    </form>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "register",
  data() {
    return {
      user: {
        username: "",
        password: "",
        confirmPassword: "",
        role: "user",
      },
      registrationErrors: false,
      registrationErrorMsg: "There were problems registering this user.",
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = "Password & Confirm Password do not match.";
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                name: "login",
                query: { registration: "success" },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = "Bad Request: Validation Errors";
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = "There were problems registering this user.";
    },
  },
};
</script>

<style scoped lang="scss">
@import "../styles/colors.scss";

#register {
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
.form-group {
  display: flex;
  justify-content: center;
}
.form-group input,
button,
a {
  margin: 10px;
  background-color: $textDark;
  color: $textLight;
  border: 2px solid $highlight;
  border-radius: 5px;
}
input::-webkit-input-placeholder {
  color: $textLight;
  font-family: "Lora", serif;
}
a {
  width: 70%;
  text-align: center;
}
button, a {
  font-size: 1.2em;
  font-family: "Lora", serif;
  padding: 1%;
  margin: 1% auto;
}
.btn{
  margin: 0 15%;
  width: 70%;
}
</style>
