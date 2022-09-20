<template>
  <div class=" p-2">
    <div class="bg-danger text-center p-4">
      <img :src="cult.leader.picture" alt="">
      <h3 class="m-0 p-2">
        {{cult.name}}
      </h3>
    </div>
    <div class="bg-grey p-3">
      <p>{{cult.description}}</p>
    </div>
    <router-link :to="{name: 'CultDetails', params: {id: cult.id}}">
      <button class="btn btn-warning">see more</button>
    </router-link>
  </div>
</template>


<script>
export default {
  props: { cult: { type: Object, required: true } },
  setup() {
    return {}
  }
};
</script>


<style lang="scss" scoped>

</style>
