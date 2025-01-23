<template>
  <div class="comment-body">
    <header><b>{{comment.user}}</b> commented on <b>
      {{new Date(comment.date).toLocaleDateString() + " " +
      new Date(comment.date).toLocaleTimeString()}}
    </b>:</header>
    <div class="comment-contents">{{comment.comment}}</div>
  </div>
</template>

<script setup>
defineProps({
  comment: {}
})
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