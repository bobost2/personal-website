<template>
  <a @click="navigate" :class="[
      {'nav-button-desktop' : buttonStyle === 'desktop'},
      {'nav-button-mobile' : buttonStyle === 'mobile'},
      {'nav-button-mobile-active' : isSelected && buttonStyle === 'mobile'},
      {'nav-button-anim' : !isSelected }]">
    {{ text }}
    <div :class="[
        {'underline' : buttonStyle === 'desktop'},
        {'underline-anim' : !isSelected && buttonStyle === 'desktop'}]"/>
  </a>
</template>


<script setup>
import { useRouter } from "vue-router";

const router = useRouter();

const props = defineProps({
  text: String,
  path: String,
  isSelected: Boolean,
  buttonStyle: String
})

function navigate() {
  router.push(props.path);
}

</script>


<!--suppress CssUnusedSymbol-->
<style scoped>
  .underline {
    height: 2px;
    border-radius: 4px;
    background: var(--font-color);
  }

  .underline-anim {
    width: 0;
  }

  .nav-button-desktop {
    font-size: 18px;
    font-weight: bolder;
    font-family: "walter-turncoat-regular", sans-serif;
    text-decoration: none;
    cursor: default;
  }

  .nav-button-mobile {
    padding: 10px;
    font-size: 25px;
    font-weight: bolder;
    font-family: "walter-turncoat-regular", sans-serif;
    text-decoration: none;
    cursor: default;
  }

  .nav-button-mobile-active {
    background: rgba(0, 0, 0, 0.15);
  }

  .nav-button-anim {
    cursor: pointer;
  }
  
  .nav-button-anim:hover {
    animation: nav-button-hover 0.5s forwards;
  }

  .nav-button-anim:not(:hover) {
    animation: nav-button-release 0.5s forwards;
  }

  .nav-button-anim:active {
    color: var(--font-color-3) !important;
  }

  @keyframes nav-button-hover {
    from {
      color: var(--font-color);
    }
    to {
      color: var(--font-color-2);
    }
  }

  @keyframes nav-button-release {
    from {
      color: var(--font-color-2);
    }
    to {
      color: var(--font-color);
    }
  }

  @keyframes nav-button-underline {
    from {
      width: 0;
      background: var(--font-color);
    }
    to {
      width: 100%;
      background: var(--font-color-2);
    }
  }

  @keyframes nav-button-underline-release {
    from {
      width: 100%;
      background: var(--font-color-2);
    }
    to {
      width: 0;
      background: var(--font-color);
    }
  }

  .nav-button-desktop:hover .underline-anim {
    animation: nav-button-underline 0.5s forwards;
  }

  .nav-button-desktop:not(:hover) .underline-anim {
    animation: nav-button-underline-release 0.5s forwards;
  }
</style>