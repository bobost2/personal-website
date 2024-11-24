<script setup>
import { useRoute } from 'vue-router'

const route = useRoute()
const projectId = route.params.id;
</script>

<template>
  <h1>
    Project Page
  </h1>
  {{ projectId }}
</template>

<style scoped>

</style>