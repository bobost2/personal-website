<template>
  <div class="download-link-element">
    <div class="file-link-info">
      <img v-if="isDirectDownload" src="/icons/download.svg" alt="Download icon" style="width: 35px"/>
      <img v-else src="/icons/link.svg" alt="Link icon" style="width: 35px"/>
      {{ filename }}
    </div>
    <rect-button @click="handleClick">{{ isDirectDownload ? "Download file" : "Open link" }}</rect-button>
  </div>
</template>

<script setup>
import RectButton from "@/components/RectButton.vue";

const props = defineProps({
  isDirectDownload: Boolean,
  filename: String,
  link: String
})

function handleClick() {
  window.open(props.link);
}
</script>

<style scoped>
  .download-link-element {
    width: 100%;
    display: flex;
    align-items: center;
    flex-wrap: wrap;
    justify-content: space-between;
  }

  .file-link-info {
    display: flex;
    align-items: center;
    gap: 15px;
    font-size: 22px;
  }
</style>