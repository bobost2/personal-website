<template>
  <div class="center-dialog">
    <div class="base-dialog">
      <h1 style="margin-bottom: 20px">{{resetPassword ? "Reset Password" : "Admin Login"}}</h1>
      <div class="input-group">
        <label for="username">Username:</label>
        <input type="text" id="username" name="username" v-model="username" required>
      </div>
      <div class="input-group">
        <label for="password">Password:</label>
        <input type="password" id="password" name="password" v-model="password" required>
      </div>
      <div class="input-group" v-if="resetPassword">
        <label for="password">New Password:</label>
        <input type="password" id="password" name="password" v-model="newPassword" required>
      </div>
      <h4 class="error-text" v-if="warningPrompt">{{prompt}}</h4>
      <RectButton style="margin-top: 20px" @click="buttonClick">{{resetPassword ? "Reset Password" : "Login"}}</RectButton>
    </div>
  </div>
</template>

<script setup>
import RectButton from "@/components/RectButton.vue";
import { useRouter } from "vue-router";
import { ref } from 'vue';

const router = useRouter();
const username = ref('');
const password = ref('');
const newPassword = ref('');
const warningPrompt = ref(false);
const prompt = ref('');

const resetPassword = ref(false);

function buttonClick() {
  if (resetPassword.value) {
    tryResetPassword();
  } else {
    tryLogin();
  }
}

function tryLogin() {
  warningPrompt.value = false;

  if (username.value === '' || password.value === '') {
    warningPrompt.value = true;
    prompt.value = 'Please fill in all of the fields.';
    return;
  }

  fetch('https://localhost:7001/User/Login', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      username: username.value,
      password: password.value
    })
  })
    .then(response => {
      const code = response.status;
      return response.json().then(data => ({ code, data }));
    })
    .then(({ code, data }) => {
      console.log(code);
    if (code === 200) {
      localStorage.setItem('token', data.token);
      console.log(data.token);
      router.push("/admin");
    } else {
      switch (data.code) {
        case "resetNeeded":
          resetPassword.value = true;
          break;
        case "passwordInvalid":
          warningPrompt.value = true;
          prompt.value = 'Username or password are invalid.';
          break;
        default:
          warningPrompt.value = true;
          prompt.value = 'An error occurred. Please try again later.';
          break;
      }
    }
    })
    .catch((error) => {
      console.error('Error:', error);
    });
}

function tryResetPassword() {
  warningPrompt.value = false;

  if (username.value === '' || password.value === '' || newPassword.value === '') {
    warningPrompt.value = true;
    prompt.value = 'Please fill in all of the fields.';
    return;
  }

  fetch('https://localhost:7001/User/ResetPassword', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      username: username.value,
      oldPassword: password.value,
      newPassword: newPassword.value
    })
  })
    .then(response => {
      const code = response.status;
      return response.json().then(data => ({ code, data }));
    })
    .then(({ code, data }) => {
      console.log(code);
    if (code === 200) {
      localStorage.setItem('token', data.token);
      console.log(data.token);
      router.push("/admin");
    } else {
      switch (data.code) {
        case "passwordInvalid":
          warningPrompt.value = true;
          prompt.value = 'Username or password are invalid.';
          break;
        case "resetForbidden":
          warningPrompt.value = true;
          prompt.value = 'You are not allowed to reset the password.';
          break;
        default:
          warningPrompt.value = true;
          prompt.value = 'An error occurred. Please try again later.';
          break;
      }
    }
    })
    .catch((error) => {
      console.error('Error:', error);
    });
}

</script>

<style scoped>
.center-dialog {
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}

.base-dialog {
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 400px;
  background: var(--navbar-color);
  padding: 20px;
}

.input-group {
  margin: 10px;
  font-size: 20px;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}

.input-group label {
  margin-right: 10px;
}

input {
  background: none;
  color: var(--font-color);
  border: 2px solid var(--font-color);
  font-size: 20px;
  font-family:"berlin-sans-fb", sans-serif;
  padding: 10px;
  width: 100%;
}

input:focus, textarea:focus {
  outline: none;
  border-color: var(--font-color-3);
  box-shadow: 0 0 5px var(--font-color-3);
}

h1 {
  margin: 0;
}

.error-text {
  color: #ff7d7d;
  margin: 0;
}
</style>