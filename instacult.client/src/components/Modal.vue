<template>

  <!-- Modal Body -->
  <!-- if you want to close by clicking outside the modal, delete the last endpoint:data-bs-backdrop and data-bs-keyboard -->
  <div class="modal fade" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" role="dialog"
    aria-labelledby="modalTitleId" aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-xl" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <slot name="header">
            <!-- MODAL HEADER SLOT -->
          </slot>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <slot name="body">
            <!-- MODAL BODY SLOT -->
          </slot>
        </div>
        <div class="modal-footer">
          <slot name="footer">
            <!-- MODAL FOOTER SLOT -->
          </slot>
        </div>
      </div>
    </div>
  </div>

</template>


<script>
export default {
  setup() {
    return {}
  }
};
</script>


<style lang="scss" scoped>

</style>
