<script setup>

</script>

<template>
  <div class="container">
    <div class="inside-container">
      <h1>404</h1>
      <h2>Not Found</h2>
      <h4>Somehow you ended up on this page that contains literally nothing.
        Feel free to return to an existing page by clicking something in the navbar.</h4>
    </div>
  </div>
</template>

<style scoped>
  .container {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100%;
    text-align: center;
  }

  .inside-container {
    display: flex;
    align-items: center;
    flex-direction: column;
    width: 50%;
  }

  h1 {
    font-size: 50px;
    margin: 2px 0;
  }

  h2 {
    font-size: 40px;
    margin: 2px 0;
  }

  h4 {
    font-weight: lighter;
    font-size: 20px;
    margin: 5px 0;
  }
</style>