<template>
  <textarea placeholder="Enter your comment here" v-model="comment"/>
  <div class="name-submit-group">
    <label v-if="isThereError" class="error-text">{{errorText}}</label>
    <input placeholder="Enter your name here" v-model="user"/>
    <rect-button @click="sendComment">Submit</rect-button>
  </div>
</template>

<script setup>
import RectButton from "@/components/RectButton.vue";
import { ref } from 'vue';

const props = defineProps({
  projectId: Number,
  sendCommentFunc: Function
})

const comment = ref('');
const user = ref('');

const isThereError = ref(false);
const errorText = ref('');

function sendComment() {
  isThereError.value = false;

  if (comment.value === '' || user.value === '') {
    isThereError.value = true;
    errorText.value = 'Please fill in all of the fields.';
    return;
  }

  fetch('https://localhost:7001/Project/SendComment', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      projectId: props.projectId,
      name: user.value,
      comment: comment.value
    })
  }).then(response => {
    if (response.status === 200) {
      props.sendCommentFunc(comment.value, user.value);
      comment.value = '';
      user.value = '';
    } else {
      isThereError.value = true;
      errorText.value = 'An error occurred while sending the comment.';
    }
  });
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

  .error-text {
    color: #ff7d7d;
    font-size: 20px;
    margin-right: 10px;
  }
</style>