<template>
  <div :class="[{'badge' : isBadge}]">
    <img v-if="icon === 'Frozen'" src="/icons/snowflake.svg" alt="Snowflake icon"/>
    <img v-if="icon === 'Game'" src="/icons/controller.svg" alt="Controller icon"/>
    <img v-if="icon === 'Unreal'" src="/icons/unreal-engine.svg" alt="Unreal Engine icon"/>
    <img v-if="icon === 'Blender'" src="/icons/blender.svg" alt="Blender icon"/>
    <!-- To do: Add more icons for different statuses -->
    {{ text }}
  </div>
</template>

<script setup>
defineProps({
  icon: String,
  text: String,
  isBadge: Boolean
})
</script>


<style scoped>
  div {
    display: flex;
    align-content: center;
    align-items: center;
    gap: 3px;
  }

  /*noinspection CssUnusedSymbol*/
  .badge {
    border: 2px solid var(--font-color);
    border-radius: 5px;
    padding: 4px 8px;
    font-size: 18px;
    gap: 5px;
  }
</style>