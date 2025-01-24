<template>
  <div class="item-container">
    <h2>{{ name }}</h2>
    <h2>{{ description }}</h2>
    <RectButton @click="deleteProject">Delete</RectButton>
  </div>
</template>

<script setup>
import { defineProps } from 'vue';
import RectButton from "@/components/RectButton.vue";

const props = defineProps({
  id: Number,
  name: String,
  description: String,
  deleteFunc: Function
})

function deleteProject() {
  props.deleteFunc(props.id);
}
</script>

<style scoped>
.item-container {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 5px 20px;

  border-bottom: 2px solid var(--font-color);
}
</style>