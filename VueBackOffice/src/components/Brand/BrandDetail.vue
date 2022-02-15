<template>
  <div v-if="info !== null" class="col-8 offset-2 mt-5">
    <div class="text-start fs-4">
      <span>
        {{ info.name }}
      </span>
      <span class="d-flex fw-bold fs-6 mt-4 mb-3">
        Categorie associate ai Prodotti di {{ info.name }}
      </span>
    </div>

    <div class="container-fluid text-center py-2 border border-dark mb-4">
      <div class="row">
        <div class="col-4" v-for="cat in info.listOfCategs" :key="cat.id">
          {{ cat.name }} ({{ cat.numOfProds }})
        </div>
      </div>
    </div>
    <div class="text-start">
      <div class="fw-bold fs-6">Richieste di informazioni per Prodotto</div>
      <div class="my-2">
        <span class="fw-bold fs-6">{{ info.numOfInforequests }}</span> richieste
        informazioni su un totale di
        <span class="fw-bold fs-6">{{ info.numOfProducts }}</span> prodotti
      </div>
    </div>
    <div>
      <table class="table table-success table-striped text-start">
        <thead>
          <tr>
            <th scope="col">Id</th>
            <th scope="col">Nome Prodotto</th>
            <th scope="col">Num di Richieste Informazioni</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="prod in listOfProducts" :key="prod.ProductId">
            <td>{{ prod.productId }}</td>
            <td>{{ prod.productName }}</td>
            <td>{{ prod.numOfInforequest }}</td>
          </tr>
        </tbody>
      </table>
    </div>

    <Paging
      class="mt-5"
      :pageNumber="pageNumber"
      :pageSize="pageSize"
      :totalPages="totalPages"
      v-on:next="next"
      v-on:previous="previous"
      v-on:selectedIndex="selectedIndex"
    />
  </div>
</template>

<script>
import { RepositoryFactory } from "../../services/repositoryFactory";
import Paging from "../Generics/paging.vue";
const BrandRepo = RepositoryFactory.get("brands");
export default {
  components: {
    Paging,
  },
  data() {
    return {
      info: null,
      pageNumber: 1,
      pageSize: 5,
    };
  },
  computed: {
    startIndex() {
      return (this.pageNumber - 1) * this.pageSize;
    },
    endIndex() {
      return this.startIndex + this.pageSize;
    },
    totalPages() {
      return Math.ceil(this.info.numOfProducts / this.pageSize);
    },
    listOfProducts() {
      let list = [...this.info.listOfProds];
      return list.slice(this.startIndex, this.endIndex);
    },
  },
  methods: {
    next() {
      this.pageNumber++;
    },
    previous() {
      this.pageNumber--;
    },
    selectedIndex(index) {
      this.pageNumber = index;
    },
  },
  async created() {
    this.info = await BrandRepo.getBrandDetail(this.$route.params.id);
  },
};
</script>

<style scoped>
a {
  cursor: pointer;
}
</style>
