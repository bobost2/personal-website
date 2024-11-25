<template>
  <div class="outer-container">
    <h1>Who am I?</h1>
    <p>My name is <b>Boris Stefanov</b> and I'm mainly working on web, software and game development. I also have
    some experience with video editing, VFX and 3D modeling.</p>

    <h1>Cool, where can I find your projects?</h1>
    <p>You can see my projects that I've worked on by clicking "Projects" in the navbar or
      by clicking the button here.</p>
    <rect-button @click="goToProjects">Go to "Projects"</rect-button>

    <h1>How can I contact you?</h1>
    <p>To contact me, go to the "About Me" page and you will find all the necessary information there.</p>
    <rect-button @click="goToAboutMe">Go to "About me"</rect-button>
  </div>
</template>

<script setup>
import RectButton from "@/components/RectButton.vue";

import { useRouter } from "vue-router";

const router = useRouter();

function goToProjects() {
  router.push("/projects");
}

function goToAboutMe() {
  router.push("/about-me");
}

</script>

<style scoped>
  .outer-container {
    padding: 20px 30px;
  }

  h1 {
    font-size: 40px;
    margin: 10px 0;
  }

  h1:not(:first-of-type) {
    margin-top: 50px;
  }

  p {
    font-size: 20px;
  }
</style>