<template>
  <textarea placeholder="Enter your comment here" v-model="comment"/>
  <div class="name-submit-group">
    <input placeholder="Enter your name here" v-model="user"/>
    <rect-button @click="sendComment">Submit</rect-button>
  </div>
</template>

<script setup>
import RectButton from "@/components/RectButton.vue";
import { ref } from 'vue';

const props = defineProps({
  sendCommentFunc: Function
})

const comment = ref('');
const user = ref('');

function sendComment() {
  props.sendCommentFunc(comment.value, user.value);

  comment.value = '';
  user.value = '';
}

</script>

<style scoped>
  input, textarea {
    background: none;
    color: var(--font-color);
    border: 2px solid var(--font-color);
    font-size: 20px;
    font-family:"berlin-sans-fb", sans-serif;
    padding: 10px;
  }

  input:focus, textarea:focus {
    outline: none;
    border-color: var(--font-color-3);
    box-shadow: 0 0 5px var(--font-color-3);
  }

  textarea {
    width: calc(100% - 24px);
    min-height: 40px;
    height: 100px;
    resize: vertical;
  }

  .name-submit-group {
    margin-top: 5px;
    display: flex;
    width: 100%;
    justify-content: flex-end;
    flex-wrap: wrap;
  }
</style>