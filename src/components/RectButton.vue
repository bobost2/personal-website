<template>
  <button class="square-button">
    <slot></slot>
  </button>
</template>

<script setup>

</script>

<style scoped>
  .square-button {
    font-size: 20px;
    font-family: berlin-sans-fb, sans-serif;
    color: var(--font-color);
    background: none;
    border: 2px solid var(--font-color);
    padding: 10px 20px;
    font-weight: bolder;
  }

  .square-button:hover {
    background: rgba(255, 255, 255, 0.05);
    cursor: pointer;
  }

  .square-button:active {
    background: rgba(255, 255, 255, 0.1);
  }
</style>