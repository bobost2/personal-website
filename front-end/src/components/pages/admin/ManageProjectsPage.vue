<template>
<nav>
  <h1>Admin Panel</h1>

  <div class="nav-button-group">
    <rect-button @click="manageProjects">Manage Projects</rect-button>
    <rect-button @click="logout">Logout</rect-button>
  </div>
</nav>

<div style="padding: 20px; height: 100%" v-if="!createProjectWindow">
  <div class="manage-proj-title">
    <h1 style="margin: 0">Manage Projects:</h1>
    <rect-button @click="createProject">Create Project</rect-button>
  </div>
  <div class="projects-container">
    <admin-project-item v-for="project in projects" :key="project.id" :id="project.id" :name="project.name"
                        :description="project.description" :delete-func="deleteProject"/>
  </div>
</div>
<create-project-subpage v-else :return-to-projects="manageProjects"/>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router";
import RectButton from "@/components/RectButton.vue";
import AdminProjectItem from "@/components/pages/admin/manage-project-components/AdminProjectItem.vue";
import CreateProjectSubpage from "@/components/pages/admin/manage-project-components/CreateProjectSubpage.vue";

const router = useRouter();
const createProjectWindow = ref(false);
const projects = ref([]);

fetch("https://localhost:7001/User/CheckAuth", {
  method: "GET",
  headers: {
    Authorization: "Bearer " + localStorage.getItem("token"),
  }
})
  .then((response) => {
    if (response.status === 200) {
      console.log("User is authenticated");
    } else {
      console.log("User is not authenticated");
      router.push("/admin/login");
   }
});

function createProject() {
  createProjectWindow.value = true;
}

function deleteProject(projectId) {
  fetch("https://localhost:7001/Project/DeleteProject?id=" + projectId, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + localStorage.getItem("token"),
    },
  })
    .then((response) => {
      if (response.status === 200) {
        fetchProjects();
      }
    });
}

function logout() {
  localStorage.removeItem("token");
  router.push("/");
}

function fetchProjects() {
  fetch("https://localhost:7001/Project/GetProjects")
      .then(response => response.json())
      .then(data => {
        projects.value = data;
      });
}

function manageProjects() {
  createProjectWindow.value = false;
  fetchProjects();
}

onMounted(() => {
  fetchProjects();
});
</script>

<style scoped>
  nav {
    background: var(--navbar-color);
    box-shadow: 0 4px 4px #00000063;
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 5px 20px;
  }

  .nav-button-group {
    gap: 20px;
    display: flex;
  }

  .manage-proj-title {
    display: flex;
    width: 100%;
    justify-content: space-between;
    align-items: center;
  }

  .projects-container {
    height: calc(100% - 70px);
    border: 2px var(--font-color) solid;
    margin-top: 20px;
    overflow-y: auto;
  }
</style>