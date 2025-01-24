<template>
  <div class="frame" @mouseover="startAutoplay" @mouseleave="stopAutoplay" @click="onProjectClick">
    <div class="image-real" v-if="showImage">
      <video v-if="showAnim && hasAnim" class="image-prev vid-prev-anim"
             src="/project-pic-anim-placeholder.mp4" autoplay muted loop/>
      <img v-else class="image-prev" src="/project-pic-placeholder.jpeg" alt="Project image"/>
    </div>
    <div v-else class="image-placeholder">No Image (300x170)</div>
    <div class="title">{{name}}</div>
    <div class="date">{{startDate.split("T")[0]}} - {{endDate.split("T")[0]}}</div>
    <div class="description">{{description}}</div>
  </div>
</template>

<script setup>
  import { ref, defineProps } from 'vue';
  import { useRouter } from 'vue-router';

  const router = useRouter();

  const showImage = true;
  const hasAnim = true;
  const showAnim = ref(false);

  let animTimeout = null;

  const props = defineProps({
    id: Number,
    name: String,
    description: String,
    startDate: Date,
    endDate: Date
  })

  function startAutoplay() {
    if (!hasAnim) return;

    clearTimeout(animTimeout);
    animTimeout = setTimeout(() => {
      showAnim.value = true;
    }, 500);
  }

  function stopAutoplay() {
    if (!hasAnim) return;

    clearTimeout(animTimeout);
    showAnim.value = false;
  }

  function onProjectClick()
  {
    router.push(`/projects/${props.id}`);
  }
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

  .frame:hover {
    border: 2px solid var(--font-color-2);
    animation: frame-hover 0.5s forwards;
    cursor: pointer;
  }

  @keyframes frame-hover {
    from {
      border: 2px solid var(--font-color);
      width: 300px;
      height: 300px;
      background: rgba(0, 0, 0, 0);
    }

    to {
      border: 2px solid var(--font-color-2);
      width: 310px;
      height: 310px;
      background: rgba(0, 0, 0, 0.15);
    }
  }

  .frame:not(:hover) {
    animation: frame-hover-release 0.5s forwards;
  }

  @keyframes frame-hover-release {
    from {
      border: 2px solid var(--font-color-2);
      width: 310px;
      height: 310px;
      background: rgba(0, 0, 0, 0.15);
    }

    to {
      border: 2px solid var(--font-color);
      width: 300px;
      height: 300px;
      background: rgba(0, 0, 0, 0);
    }
  }

  .frame:active {
    background: rgba(0, 0, 0, 0.25) !important;
  }

  .image-placeholder {
    background: var(--font-color-3);
    height: 170px;
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 30px;
  }

  .image-real {
    height: 170px;
    object-fit: cover;
  }

  .image-prev {
    width: 100%;
    height: 170px;
    object-fit: cover;
  }

  .vid-prev-anim {
    animation: video-pop 1s forwards;
  }

  @keyframes video-pop {
    from {
      opacity: 0.3;
    }

    to {
      opacity: 1;
    }
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