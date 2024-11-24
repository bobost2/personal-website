import { createRouter, createWebHistory } from 'vue-router';

import HomePage from "@/components/pages/HomePage.vue";
import AboutMePage from "@/components/pages/AboutMePage.vue";
import ProjectsPage from "@/components/pages/ProjectsPage.vue";
import InitiativesPage from "@/components/pages/InitiativesPage.vue";
import ProjectPage from "@/components/pages/ProjectPage.vue";
import NotFound from "@/components/pages/NotFound.vue";

const routes = [
    { path: '/', component: HomePage, name: 'home' },
    { path: '/projects', component: ProjectsPage, name: 'projects' },
    { path: '/projects/:id', component: ProjectPage, name: 'project' },
    { path: '/initiatives', component: InitiativesPage, name: 'initiatives' },
    { path: '/about-me', component: AboutMePage, name: 'about-me' },
    { path: '/:pathMatch(.*)*', component: NotFound, name: 'not-found' },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;