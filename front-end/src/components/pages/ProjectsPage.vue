<template>
  <div class="outer-container">
    <header>
      <h1>Personal Projects</h1>
      <h3>Here you can find all of my personal projects I'm working on or have worked on.
        Non-personal/company projects are not completely documented or shown due to NDA and privacy reasons.
        To see more details about a project, click on its card. Note that most of the projects are unfinished,
        since they were created either for the reason to learn something new or to experiment with it.</h3>
    </header>
    <div class="filters-outer">
      <div class="filters-placeholder">TO DO - Add project filters here</div>
    </div>
    <div class="projects-container">
      <project-item v-for="project in projects" :key="project.id" :id="project.id" :name="project.name"
                    :description="project.shortDescription" :start-date="project.dateStarted" :end-date="project.dateEnded"/>
    </div>
  </div>
</template>

<script setup>
import ProjectItem from "@/components/project-components/ProjectItem.vue";
import { onMounted, ref } from "vue";

const projects = ref([]);

onMounted(() => {
  fetch("https://localhost:7001/Project/GetProjects")
      .then(response => response.json())
      .then(data => {
        projects.value = data;
      });
});
</script>

<style scoped>
  .outer-container {
    overflow: auto;
  }

  header {
    margin: 30px;
    position: relative;
    z-index: 2;
  }

  h1 {
    font-size: 40px;
    margin: 10px 0;
  }

  h3 {
    font-size: 20px;
    margin: 2px 0;
    font-weight: lighter;
  }

  .filters-outer {
    background: var(--background-color);
    position: sticky;
    top: 0;
    z-index: 3;
    padding: 20px 0;
    margin-top: -20px;
  }

  .filters-placeholder {
    border: 2px var(--font-color) solid;
    margin: 0 30px;
    padding: 20px;
    font-size: 20px;
    font-weight: bold;
  }

  .projects-container {
    margin: 30px;
    display: flex;
    flex-wrap: wrap;
    gap: 40px;
    justify-content: space-around;
  }
</style>