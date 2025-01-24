<template>
  <div style="padding: 20px; height: 100%">
    <div class="proj-title">
      <h1 style="margin: 0">Create Project:</h1>
      <rect-button @click="createProject">Create Project</rect-button>
    </div>
    <div>
      <div class="input-group">
        <label for="project-name">Project Name:</label>
        <input type="text" placeholder="Project Name" v-model="projectName">
      </div>
      <div class="input-group">
        <label for="project-type">Project Type:</label>
        <select v-model="projectType">
          <option v-for="type in allTypes" :key="type.id" :value="type.id">{{type.name}}</option>
        </select>
      </div>
      <div class="input-group">
        <label for="project-status">Project Status:</label>
        <select v-model="projectStatus">
          <option v-for="status in allStatuses" :key="status.id" :value="status.id">{{status.name}}</option>
        </select>
      </div>
      <div class="input-group">
        <label for="project-status">Project Technologies (to be updated):</label>
        <select v-model="projectTechnologies">
          <option v-for="tech in allTechnologies" :key="tech.id" :value="tech.id">{{tech.name}}</option>
        </select>
      </div>
      <div class="input-group">
        <label for="project-short-desc">Project Short Description:</label>
        <textarea placeholder="Project Short Description" v-model="projectShortDesc"></textarea>
      </div>
      <div class="input-group">
        <label for="project-long-desc">Project Long Description:</label>
        <textarea placeholder="Project Long Description" v-model="projectLongDesc"></textarea>
      </div>
      <div class="input-group">
        <label for="project-start-date">Project Start Date:</label>
        <input type="datetime-local" v-model="projectStartDate">
      </div>
      <div class="input-group">
        <label for="project-end-date">Project End Date:</label>
        <input type="datetime-local" v-model="projectEndDate">
      </div>
    </div>
  </div>
</template>

<script setup>
import RectButton from "@/components/RectButton.vue";
import {defineProps, onMounted, ref} from 'vue';

const projectName = ref('');
const projectType = ref('');
const projectStatus = ref('');
const projectTechnologies = ref('');
const projectShortDesc = ref('');
const projectLongDesc = ref('');
const projectStartDate = ref('');
const projectEndDate = ref('');

const allStatuses = ref([]);
const allTypes = ref([]);
const allTechnologies = ref([]);

const props = defineProps({
  returnToProjects: Function
})

function createProject() {
  const startDate = new Date(projectStartDate.value);
  const startDateUnix = Math.floor(startDate.getTime() / 1000);

  const endDate = new Date(projectEndDate.value);
  const endDateUnix = Math.floor(endDate.getTime() / 1000);

  fetch("https://localhost:7001/Project/CreateProject", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + localStorage.getItem("token"),
    },
    body: JSON.stringify({
      name: projectName.value,
      typeId: projectType.value,
      statusId: projectStatus.value,
      technologies: [projectTechnologies.value],
      shortDescription: projectShortDesc.value,
      longDescription: projectLongDesc.value,
      dateStarted: startDateUnix,
      dateEnded: endDateUnix
    })
  })
    .then(response => {
      if (response.status === 200) {
        props.returnToProjects();
      }
    });
}

onMounted(() => {
  fetch("https://localhost:7001/Project/GetProjectTypes")
    .then(response => response.json())
    .then(data => {
      allTypes.value = data;
    });

  fetch("https://localhost:7001/Project/GetProjectStatuses")
    .then(response => response.json())
    .then(data => {
      allStatuses.value = data;
    });

  fetch("https://localhost:7001/Project/GetProjectTechnologies")
    .then(response => response.json())
    .then(data => {
      allTechnologies.value = data;
    });
})


</script>

<style scoped>
h1 {
  margin: 0;
}

.proj-title {
  display: flex;
  width: 100%;
  justify-content: space-between;
  align-items: center;
}

.input-group {
  margin: 10px;
  font-size: 20px;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}

.input-group label {
  margin-right: 10px;
}

input, textarea {
  background: none;
  color: var(--font-color);
  border: 2px solid var(--font-color);
  font-size: 20px;
  font-family:"berlin-sans-fb", sans-serif;
  padding: 10px;
  width: 100%;
}
</style>