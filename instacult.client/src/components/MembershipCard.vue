<template>
  <div class="bg-info text-center ">
    <img class="img-fluid" :src="cultist.picture" alt="">
    <b>Membership Id: {{cultist.cultMemberId}}</b>
    <p>{{cultist.name}}</p>
    <button class="btn btn-primary" @click="excommunicate"><i class="mdi mdi-account-minus"></i></button>
  </div>
</template>


<script>
import { cultMembersService } from '../services/CultMembersService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

export default {
  props: { cultist: { type: Object, required: true } },
  setup(props) {
    return {
      async excommunicate() {
        try {
          await cultMembersService.excommunicate(props.cultist.cultMemberId)
        } catch (error) {
          Pop.toast(error.message, 'error')
          logger.error(error)
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped>

</style>
