<template>
  <div class="frame">
    <!-- Imagine hovering on the image to play a gif of the project -->
    <!-- That would be really cool -->
    <div class="image-placeholder">No Image</div>
    <div class="title">Title</div>
    <div class="date">25 Nov 2024 - 25 Nov 2025</div>
    <div class="description">Description</div>
  </div>
</template>

<script setup>

</script>

<style scoped>
  .frame {
    border: 2px solid var(--font-color);
    border-radius: 5px;
    width: 300px;
    height: 300px;
    display: flex;
    flex-direction: column;
  }

  .image-placeholder {
    background: var(--font-color-3);
    height: 170px;
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 30px;
  }

  .title {
    font-size: 20px;
    margin: 5px 10px;
    font-weight: bold;
  }

  .date {
    margin: 0 10px;
    font-size: 20px;
  }

  .description {
    margin: 5px 10px;
    font-size: 20px;
    overflow: hidden;
    max-height: 50px;
  }
</style>