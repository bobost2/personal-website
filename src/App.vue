<template>
  <Navbar/>
  <router-view></router-view>
</template>

<script setup>
import Navbar from "@/components/Navbar.vue";
</script>

<style>
@font-face {
  font-family: 'berlin-sans-fb';
  src: url('/fonts/BRLNSR.woff2') format('woff2');
  font-weight: normal;
  font-style: normal;
}

@font-face {
  font-family: 'berlin-sans-fb-demi';
  src: url('/fonts/BRLNSDB.woff2') format('woff2');
  font-weight: normal;
  font-style: normal;
}

@font-face {
  font-family: 'berlin-sans-fb-bold';
  src: url('/fonts/BRLNSB.woff2') format('woff2');
  font-weight: bold;
  font-style: normal;
}

@font-face {
  font-family: 'walter-turncoat-regular';
  src: url('/fonts/WalterTurncoat-Regular.woff2') format('woff2');
  font-weight: normal;
  font-style: normal;
}

:root {
  --background-color: #282828;
  --font-color: #939393; /* Base font color */
  --font-color-2: #717171; /* Hover font color */
  --font-color-3: #616161; /* Active font color */
}

body {
  background: var(--background-color);
  color: var(--font-color);
  margin: 0;
  font-family: "berlin-sans-fb", sans-serif;
}
</style>
