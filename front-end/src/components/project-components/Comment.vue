<template>
  <div class="comment-body">
    <header><b>{{comment.username}}</b> commented on <b>
      {{new Date(comment.time).toLocaleDateString() + " " +
      new Date(comment.time).toLocaleTimeString()}}
    </b>:</header>
    <div class="comment-contents">{{comment.contents}}</div>
  </div>
</template>

<script setup>
defineProps({
  comment: {}
})
// Shows them in UTC time, but it's better than nothing.
</script>

<style scoped>
  .comment-body {
    border: 2px solid var(--font-color);
    margin: 20px 0;
    padding: 15px;
  }

  header {
    font-size: 20px;
  }

  .comment-contents {
    margin-top: 10px;
    font-size: 18px;
  }
</style>