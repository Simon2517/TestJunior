<template>
  <div v-if="info !== null" class="col-8 offset-2 mt-5">
    <div class="text-start fs-4">
      <span> {{ info.productName }} by {{ info.brandName }} </span>
    </div>
    <span class="d-flex fw-bold fs-6 mt-4 mb-3">
      Categorie associate al Prodotto
    </span>
    <div class="container-fluid text-center fs-6 py-2 border border-dark mb-4">
      <div class="row">
        <div class="col-4" v-for="cat in info.categories" :key="cat.id">
          {{ cat.name }}
        </div>
      </div>
    </div>
    <div class="text-start">
      <div class="fw-bold fs-6">Leads per questo Prodotto</div>
      <div class="my-2">
        <span class="fw-bold fs-6">{{ totalRequests }}</span> richieste
        informazioni fra cui
        <span class="fw-bold fs-6">{{ info.numberOfGuestRequest }}</span> da
        utenti guest e
        <span class="fw-bold fs-6">{{ info.numberOfUserRequest }}</span> da
        utenti registrati
        <hr class="m-0 mt-1" />
      </div>
      <button
        type="button"
        class="btn btn-primary"
        @click="
          $router.push({
            name: 'leads',
            params: { productId: info.productId },
          })
        "
      >
        Vedi tutte le richieste informazioni
      </button>
    </div>
  </div>
</template>

<script>
import { RepositoryFactory } from "../../services/repositoryFactory";
const ProductRepo = RepositoryFactory.get("products");
export default {
  data() {
    return {
      info: null,
    };
  },
  computed: {
    totalRequests() {
      return this.info.numberOfGuestRequest + this.info.numberOfUserRequest;
    },
  },
  async created() {
    this.info = await ProductRepo.getProductDetail(this.$route.params.id);
  },
};
</script>

<style scoped>
a {
  cursor: pointer;
}
hr {
  color: yellow;
}
</style>
