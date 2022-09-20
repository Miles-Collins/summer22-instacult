<template>
  <div class="container-fluid" v-if="cult.id">
    <div class="row bg-image text-center py-4">
      <div class="col-12">
        <img class="leader-img" :src="cult.leader.picture" alt="">
      </div>
      <div class="col-12 text-light text-shadow">
        <h1>{{cult.name}}</h1>
      </div>
      <CultForm :cultData="cult" v-if="cult.leaderId == account.id">
        <template #button>
          <button class="btn btn-warning col-3" data-bs-target="#cult-form" data-bs-toggle="modal">edit</button>
        </template>
      </CultForm>
      <button class="btn btn-dark col-3" @click="deleteCult">Destroy cult</button>
    </div>
    <div class="row justify-content-center py-3">
      <div class="col-md-8 text-light">
        <p>{{cult.description}}</p>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';
import CultForm from '../components/CultForm.vue';
import { router } from '../router.js';
import { cultsService } from '../services/CultsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    const route = useRoute();
    onMounted(() => {
      getCult();
    });
    async function getCult() {
      try {
        await cultsService.getCult(route.params.id);
      }
      catch (error) {
        router.push({ name: 'Home' })
        Pop.error(error.message);
        logger.log(error);
      }
    }
    return {
      cult: computed(() => AppState.activeCult),
      account: computed(() => AppState.account),
      background: computed(() => `url(${AppState.activeCult.coverImg})`),
      async deleteCult() {
        await cultsService.delete(route.params.id)
        Pop.toast('Cult deleted', 'success')
        router.push({ name: 'Home' })
      }
    };
  },
  components: { CultForm }
};
</script>


<style lang="scss" scoped>
.leader-img {
  height: 50px;
  width: 50px;
}

.bg-image {
  background-image: v-bind(background);
  background-position: center;
  background-size: cover;
}
</style>
