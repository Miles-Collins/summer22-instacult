<template>
  <slot name="button">
    <!-- BUTTON SLOT -->
  </slot>

  <Modal id="cult-form">
    <!-- STUB form -->
    <template #header>Cult time</template>
    <template #body>
      <form class="row p-5" @submit.prevent="handleSubmit">
        <div class="col-md-4 mb-3">
          <label for="cult-name" class="form-label">Cult Name</label>
          <input required v-model="editable.name" type="text" class="form-control" name="cult-name" id="cult-name"
            placeholder="Cult Name">
        </div>
        <div class="col-md-4 mb-3">
          <label for="" class="form-label">Fee</label>
          <input required v-model="editable.fee" type="number" class="form-control" name="" id=""
            aria-describedby="helpId" placeholder="">
        </div>
        <div class="col-md-4 mb-3">
          <label for="" class="form-label">Picture of your cult</label>
          <input required v-model="editable.coverImg" type="text" class="form-control" name="" id=""
            aria-describedby="helpId" placeholder="">
        </div>
        <div class="col-12">
          <img class="img-fluid" :src="editable.coverImg" alt="">
        </div>
        <div class="col-12 mb-3">
          <label for="" class="form-label">PWhat's your cult all about?</label>
          <textarea v-model="editable.description" class="form-control" name="" id="" rows="3"></textarea>
        </div>
        <div class="col-12">
          <button class="btn btn-secondary" title="submit form">Submit <i class="mdi mdi-plus"></i></button>
        </div>
      </form>
    </template>
    <!--  -->
  </Modal>
</template>


<script>
import { ref, watchEffect } from 'vue';
import { cultsService } from '../services/CultsService.js';
import { logger } from '../utils/Logger.js';
export default {
  props: { cultData: { type: Object, required: false, default: {} } },
  setup(props) {
    const editable = ref({})
    watchEffect(() => {
      logger.log('watch')
      editable.value = props.cultData
    })
    return {
      editable,
      async handleSubmit() {
        try {
          logger.log("form data", editable.value);
          if (!editable.value.id) {
            await cultsService.create(editable.value);
            Pop.toast("Cult Created", "warning");
          } else {
            await cultsService.update(editable.value)
            Pop.toast("Cult Updated", "info");
          }
        }
        catch (error) {
        }
      }
    };
  },
};
</script>


<style lang="scss" scoped>

</style>
