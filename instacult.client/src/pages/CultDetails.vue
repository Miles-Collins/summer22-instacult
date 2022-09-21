<template>
  <div class="container-fluid" v-if="cult.id">
    <div class="row bg-image text-center py-4">
      <div class="col-12">
        <img class="leader-img" :src="cult.leader.picture" alt="">
      </div>
      <div class="col-12 text-light text-shadow">
        <h1>{{cult.name}}</h1>
      </div>
      <CultForm :cultData="cult" v-if="isLeader">
        <template #button>
          <button class="btn btn-warning col-3" data-bs-target="#cult-form" data-bs-toggle="modal">edit</button>
        </template>
      </CultForm>
      <button v-if="isLeader" class="btn btn-dark col-3" @click="deleteCult">Destroy cult</button>
      <button class="btn btn-info col-3" @click="joinCult">👥Join Us👥</button>
    </div>
    <section class="row justify-content-center py-3">
      <div class="col-md-8 text-light">
        <p>{{cult.description}}</p>
      </div>
    </section>
    <section class="row">

      <TransitionGroup name="fade">
        <div v-for="clt in cultists" class="col-3 p-2" :key="clt.id">
          <MembershipCard :cultist="clt" />
        </div>
      </TransitionGroup>

    </section>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';
import CultForm from '../components/CultForm.vue';
import MembershipCard from '../components/MembershipCard.vue';
import { router } from '../router.js';
import { cultMembersService } from '../services/CultMembersService.js';
import { cultsService } from '../services/CultsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    const route = useRoute();
    onMounted(() => {
      getCult();
      getCultists();
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
    async function getCultists() {
      try {
        await cultsService.getCultists(route.params.id);
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
      cultists: computed(() => AppState.cultists),
      isLeader: computed(() => AppState.activeCult?.leaderId == AppState.account?.id),
      background: computed(() => `url(${AppState.activeCult.coverImg})`),
      async joinCult() {
        try {
          let newCultist = { cultId: route.params.id }
          await cultMembersService.joinCult(newCultist)
        } catch (error) {
          Pop.error(error.message);
          logger.log(error);
        }
      },
      async deleteCult() {
        try {
          await cultsService.delete(route.params.id)
          Pop.toast('Cult deleted', 'success')
          router.push({ name: 'Home' })
        } catch (error) {
          Pop.error(error.message);
          logger.log(error);
        }
      }
    };
  },
  components: { CultForm, MembershipCard }
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



.fade-enter-active,
.fade-leave-active {
  transition: all 0.5s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
