<template>
  <div>
    <div class="position-absolute top-0 start-50 translate-middle-x">
      <div
        class="toast"
        role="alert"
        aria-live="assertive"
        aria-atomic="true"
        id="toastModal"
        data-bs-autohide="false"
      >
        <div class="toast-body">
          {{deletedItem.name}} will be deleted are you sure?
          <div class="mt-2 pt-2 border-top">
            <button
              type="button"
              class="btn btn-success btn-sm"
              data-bs-dismiss="toast"
              @click="deleteItem()"
            >
              Confirm
            </button>
            <button
              type="button"
              class="btn btn-danger btn-sm"
              data-bs-dismiss="toast"
            >
              Cancel
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
            Product {{ deletedItem.name }} has been deleted
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
    },
  },
};
</script>
