<template>
  <div class="project-container">
    <header>
      <h1>
        {{ project?.name }}
      </h1>
      <RectButton @click="returnToProjects">
        Return to projects
      </RectButton>
    </header>

    <div class="info-box">
      <div class="status">
        Type: <IconText :icon="project?.type.iconPath" :text="project?.type.name"/>
      </div>
      <div>Date started: {{ project?.dateStarted.split("T")[0] }}</div>
      <div>Date ended: {{ project?.dateEnded.split("T")[0] }}</div>
      <div class="status">
        Status: <IconText :icon="project?.status.iconPath" :text="project?.status.name"/>
      </div>

      <div class="status">
        Technologies used:
        <IconText v-for="technology in project?.technologies" :key="technology.id" :icon="technology.iconPath"
                  :text="technology.name" is-badge/>
      </div>

      <div class="status">
        Tags:
        <IconText v-for="tag in project?.tags" :key="tag.id" :text="tag.name" is-badge/>
        <label v-if="project?.tags.length === 0">No tags</label>
      </div>
    </div>

    <h2>
      About this project:
    </h2>
    <p>
      {{ project?.longDescription }}
    </p>

    <h2>
      Media:
    </h2>
    <p>
      No media available.
    </p>

    <h2>
      Links and downloads (Still hardcoded):
    </h2>
    <div class="info-box" style="margin-top: 20px">
      <div v-if="!hasLinksOrDownloads">
        No links or downloads available.
      </div>
      <link-download v-if="hasLinksOrDownloads" is-direct-download filename="Sample archive with text file (sample.zip)" link="/sample.zip" />
      <link-download v-if="hasLinksOrDownloads" filename="GitHub project" link="https://github.com/bobost2/personal-website" />
    </div>

    <h2>
      Add a comment:
    </h2>

    <CreateComment :project-id="projectId" :sendCommentFunc="sendComment"/>

    <h2>
      Comments:
    </h2>
    <p v-if="comments.length === 0">
      No comments available.
    </p>
    <div v-else>
      <Comment v-for="comment in comments" :comment="comment"/>
    </div>
  </div>
</template>

<script setup>
import { useRoute, useRouter } from 'vue-router'
import { ref, onMounted } from 'vue';
import RectButton from "@/components/RectButton.vue";
import IconText from "@/components/project-components/IconText.vue";
import LinkDownload from "@/components/project-components/LinkDownload.vue";
import CreateComment from "@/components/project-components/CreateComment.vue";
import Comment from "@/components/project-components/Comment.vue";

const route = useRoute();
const router = useRouter();

const comments = ref([]);
const projectId = route.params.id;
const project = ref(null);

function returnToProjects() {
  router.push('/projects');
}

function sendComment(comment, user) {
  comments.value.unshift({ contents: comment, username: user, time: new Date() });
  console.log(comments);
}

const hasLinksOrDownloads = true;

onMounted(() => {
  if (isNaN(projectId)) {
    router.push('/projects');
    return;
  }

  fetch(`https://localhost:7001/Project/GetProject?id=${projectId}`)
      .then(response => response.json())
      .then(data => {
        if (data === null || data.code === "projectNotFound") {
          router.push('/projects');
        }
        project.value = data;
      }).catch(() => {
        router.push('/projects');
      });

  fetch(`https://localhost:7001/Project/GetProjectComments?id=${projectId}`)
      .then(response => response.json())
      .then(data => {
        comments.value = data;
      });
});
</script>

<style scoped>
  .project-container {
    padding: 20px 30px;
  }

  header {
    display: flex;
    flex-wrap: wrap;
    align-items: center;
    justify-content: space-between;
    margin-bottom: 20px;
    gap: 20px;
  }

  .info-box {
    border: 2px solid var(--font-color);
    padding: 20px;
    font-size: 20px;
    display: flex;
    gap: 20px;
    align-items: center;
    justify-content: space-evenly;
    flex-wrap: wrap;
    margin-bottom: 20px;
  }

  .status {
    display: inline-flex;
    flex-wrap: wrap;
    align-items: center;
    gap: 5px;
  }

  h1 {
    font-size: 40px;
    margin: 10px 0;
  }

  h2 {
    font-size: 30px;
    margin-top: 40px;
  }

  p {
    font-size: 20px;
  }
</style>