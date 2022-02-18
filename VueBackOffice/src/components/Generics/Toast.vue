<template>
  <div>
    <div class="position-fixed top-0 start-50 translate-middle-x mt-3">
      <div
        class="toast"
        role="alert"
        aria-live="assertive"
        aria-atomic="true"
        id="toastModal"
        data-bs-autohide="false"
        data-bs-delay="2000"
      >
        <div class="toast-body">
          <span class="fs-5 d-block"> {{deletedItem.name}} will be deleted <span class="d-block">are you sure?</span> </span>
          <div class="mt-2 pt-2 border-top text-center">
            <button
              type="button"
              class="btn btn-success btn-sm me-3"
              data-bs-dismiss="toast"
              @click="deleteItem()"
            >
             <span class="fs-6">Confirm</span> 
            </button>
            <button
              type="button"
              class="btn btn-danger btn-sm"
              data-bs-dismiss="toast"
            >
              <span class="fs-6">Cancel</span> 
            </button>
          </div>
        </div>
      </div>
    </div>

    <div
      class="position-fixed bottom-0 end-0 p-3"
      style="z-index: 11"
      v-show="deletedItem.name !== ''"
    >
      <div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
        <div
          id="liveToast"
          class="toast"
          role="alert"
          aria-live="assertive"
          aria-atomic="true"
        >
          <div class="toast-header">
            <strong class="me-auto">Success</strong>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="toast"
              aria-label="Close"
            ></button>
          </div>
          <div class="toast-body">
            {{ deletedItem.name }} has been deleted
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    deletedItem: Object,
  },
  methods: {
    deleteItem() {
      let item = this.deletedItem;
      this.$emit("deleteItem", item);
    }
  },
};
</script>
