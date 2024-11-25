<template>
  <nav class="desktop-nav">
    <img width="200px" height="50px" src="/LogoLandscape.svg" alt="Logo" />

    <div class="button-container">
      <NavbarButton button-style="desktop" text="Home" path="/" :is-selected="route.name === 'home'" />
      <NavbarButton button-style="desktop" text="Projects" path="/projects" :is-selected="route.name === 'projects'" />
<!--      <NavbarButton button-style="desktop" text="Initiatives" path="/initiatives" :is-selected="route.name === 'initiatives'" />-->
      <NavbarButton button-style="desktop" text="About Me" path="/about-me" :is-selected="route.name === 'about-me'" />
    </div>

    <NavbarHamburgerButton @click="openHamburgerMenu = !openHamburgerMenu" class="hamburger-button"/>
  </nav>

  <transition name="menu-pop">
    <nav v-if="openHamburgerMenu" class="mobile-nav">
      <NavbarButton button-style="mobile" text="Home" path="/" :is-selected="route.name === 'home'" />
      <NavbarButton button-style="mobile" text="Projects" path="/projects" :is-selected="route.name === 'projects'" />
<!--      <NavbarButton button-style="mobile" text="Initiatives" path="/initiatives" :is-selected="route.name === 'initiatives'" />-->
      <NavbarButton button-style="mobile" text="About Me" path="/about-me" :is-selected="route.name === 'about-me'" />
    </nav>
  </transition>
</template>

<script setup>
import NavbarButton from "@/components/navbar-components/NavbarButton.vue";
import NavbarHamburgerButton from "@/components/navbar-components/NavbarHamburgerButton.vue";

import { ref } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();

const openHamburgerMenu = ref(false);

</script>

<!--suppress CssUnusedSymbol-->
<style scoped>
  .desktop-nav {
    padding: 10px 20px 10px 10px;
    background: var(--navbar-color);
    display: flex;
    justify-content: space-between;
    align-items: center;
    box-shadow: 0 4px 4px #00000063;
    position: sticky;
    top: 0;
    z-index: 10;
  }

  .mobile-nav {
    background: var(--navbar-color);
    box-shadow: 0 4px 4px #00000063;
    display: flex;
    flex-direction: column;
    align-items: stretch;
    transform-origin: top;
    animation: popMobileNav 0.5s;
  }

  .button-container {
    display: flex;
    gap: 20px;
  }

  .hamburger-button {
    display: none;
  }

  .menu-pop-enter-active {
    animation: popMobileNav 0.5s;
  }

  .menu-pop-leave-to {
    animation: popMobileNavReverse 0.5s;
  }

  @keyframes popMobileNav {
    0% {
      transform: scaleY(0);
      opacity: 0;
    }

    30% {
      opacity: 0.1;
    }

    100% {
      transform: scaleY(1);
      opacity: 1;
    }
  }

  @keyframes popMobileNavReverse {
    0% {
      transform: scaleY(1);
      opacity: 1;
    }

    70% {
      opacity: 0.1;
    }

    100% {
      transform: scaleY(0);
      opacity: 0;
    }
  }

  @media (max-width: 620px) {
    .button-container {
      display: none;
    }

    .hamburger-button {
      display: block;
    }
  }

  @media (min-width: 620px) {
    .mobile-nav {
      display: none;
    }
  }
</style>