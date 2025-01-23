<template>
  <button class="hamburger-button">☰</button>
</template>

<style scoped>
  button {
    border: 3px solid var(--font-color);
    border-radius: 6px;
    width: 40px;
    height: 40px;
    font-size: 20px;
    padding-bottom: 5px;
    background: none;
    color: var(--font-color);
    font-weight: bolder;
    cursor: pointer;
  }

  button:hover {
    border: 3px solid var(--font-color-2);
    color: var(--font-color-2);
    background: rgba(0, 0, 0, 0.15);
  }

  button:active {
    border: 3px solid var(--font-color-3);
    color: var(--font-color-3);
    background: rgba(0, 0, 0, 0.3);
  }
</style>