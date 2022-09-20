<template>
  <div class="container-fluid">
    <h1 class="text-center">Wanna Start a Cult?</h1>
    <CultForm>
      <template #button>
        <button class="btn btn-secondary" data-bs-target="#cult-form" data-bs-toggle="modal">Start your cult +</button>
      </template>
    </CultForm>
    <h1 class="text-center">Wanna Join a Cult?</h1>
    <div class="row">
      <div class="col-md-6" v-for="c in cults" :key="c.id">
        <CultCard :cult="c" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { AppState } from '../AppState.js';
import CultCard from '../components/CultCard.vue';
import CultForm from '../components/CultForm.vue';
import { cultsService } from '../services/CultsService.js';
import Pop from '../utils/Pop.js';

export default {
  name: "Home",
  setup() {
    onMounted(() => {
      getCults();
    });
    async function getCults() {
      try {
        await cultsService.getCults();
      }
      catch (error) {
        Pop.error(error);
      }
    }
    return {
      cults: computed(() => AppState.cults),

    };
  },
  components: { CultCard, CultForm }
}
</script>

<style scoped lang="scss">

</style>
