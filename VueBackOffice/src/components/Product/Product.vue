<template>
  <div v-if="info !== null" class="mt-5">
    <div class="d-table w-100">
      <div class="d-table-cell align-bottom">
        <span class="fs-2">Prodotti </span>
      </div>
      <div class="d-table-cell text-end">
        <button
          type="button"
          class="btn btn-outline-primary"
          @click.stop="$router.push({ path: 'product/new' })"
        >
          Aggiungi Prodotto
        </button>
      </div>
    </div>
    <hr class="m-0 my-1" />

    <table class="table table-striped table-hover text-start">
      <thead>
        <tr>
          <th scope="col">
            <tr>
              <td class="w-100 text-start">Nome Brand</td>
              <td @click="orderby(1)">
                <i
                  v-if="this.asc === true && this.orderProperty <= 1"
                  class="bi bi-caret-up-fill d-flex"
                ></i>
                <i v-else class="bi bi-caret-up-fill d-flex notselected"></i>

                <i
                  v-if="this.asc === false && this.orderProperty === 1"
                  class="bi bi-caret-down-fill d-flex"
                ></i>
                <i v-else class="bi bi-caret-down-fill d-flex notselected"></i>
              </td>
            </tr>
          </th>
          <th scope="col">
            <tr>
              <td class="w-100 text-start text-center">Nome Prodotto</td>
              <td @click="orderby(2)">
                <i
                  v-if="this.asc === true && this.orderProperty === 2"
                  class="bi bi-caret-up-fill d-flex"
                ></i>
                <i v-else class="bi bi-caret-up-fill d-flex notselected"></i>

                <i
                  v-if="this.asc === false && this.orderProperty === 2"
                  class="bi bi-caret-down-fill d-flex"
                ></i>
                <i v-else class="bi bi-caret-down-fill d-flex notselected"></i>
              </td>
            </tr>
          </th>
          <th scope="col" class="text-center">Categorie</th>
          <th scope="col">
            <tr>
              <td class="w-100 text-center">Prezzo</td>
              <td @click="orderby(3)">
                <i
                  v-if="this.asc === true && this.orderProperty === 3"
                  class="bi bi-caret-up-fill d-flex"
                >
                </i>
                <i v-else class="bi bi-caret-up-fill d-flex notselected"> </i>

                <i
                  v-if="this.asc === false && this.orderProperty === 3"
                  class="bi bi-caret-down-fill d-flex"
                ></i>
                <i v-else class="bi bi-caret-down-fill d-flex notselected"> </i>
              </td>
            </tr>
          </th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td colspan="100%">
            <select class="form-select-sm w-auto" @change="onChange($event)">
              <option default value="">Tutti i brand</option>
              <option
                v-for="item in ListofBrands"
                :key="item.id"
                :value="item.name"
              >
                {{ item.name }}
              </option>
            </select>
          </td>
        </tr>
      </tbody>
      <tbody>
        <tr
          class="detail"
          v-for="(item, index) in info.listOfElements"
          :key="index"
          @click.stop="$router.push({ path: 'product/detail/' + item.id })"
        >
          <td class="w-25 align-middle">{{ item.brandName }}</td>
          <td class="w-25 align-middle">
            <span class="fw-bold">{{ item.name }}</span
            ><span class="">|{{ item.shortDescription }}</span>
          </td>
          <td class="text-start w-25 align-middle">
            <span
              class="badge bg-primary ms-2"
              v-for="(cat, i) in info.listOfElements[index].categories"
              :key="i"
              :title="cat"
              >{{ cat.substring(0,20) }}</span
            >
          </td>
          <td class="align-middle text-center">{{ item.price }}€</td>
          <td class="align-middle btn-group text-center">
            <button
              class="btn btn-outline-secondary px-2 py-1"
              @click.stop="
                $router.push({ path: 'product/' + item.id + '/edit' })
              "
            >
              <i class="bi bi-pencil-square"></i>
            </button>
            <button
              class="btn btn-outline-secondary px-2 py-1"
              data-bs-toggle="Toast"
              @click.stop="deleteItem(item.id, item.name)"
            >
              <i class="bi bi-trash3-fill"></i>
            </button>
          </td>
        </tr>
      </tbody>
    </table>

    <Paging
      :pageNumber="pageNumber"
      :pageSize="pageSize"
      :totalPages="info.totalPages"
      v-on:next="next"
      v-on:previous="previous"
      v-on:selectedIndex="selectedIndex"
    />
    <div
      class="position-fixed bottom-0 end-0 p-3"
      style="z-index: 11"
      v-show="deletedProduct !== ''"
    >
      <div
        class="toast"
        role="alert"
        aria-live="assertive"
        aria-atomic="true"
      >
        <div class="toast-header">
          <strong class="me-auto">Bootstrap</strong>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="toast"
            aria-label="Close"
            @click="deletedProduct=''"
          ></button>
        </div>
        <div class="toast-body">product {{ deletedProduct }} deleted successfully</div>
      </div>
    </div>
    <div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
  <div id="liveToast" class="toast show" role="alert" aria-live="assertive" aria-atomic="true">
    <div class="toast-header">
      
      <strong class="me-auto">Bootstrap</strong>
      <small>11 mins ago</small>
      <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
    </div>
    <div class="toast-body">
      Hello, world! This is a toast message.
    </div>
  </div>
</div>
  </div>
</template>

<script>
import { RepositoryFactory } from "../../services/repositoryFactory";
const ProductRepo = RepositoryFactory.get("products");
const BrandRepo = RepositoryFactory.get("brands");
import Paging from "../Generics/paging.vue";
// import Toast from "../Generics/Toast.vue"
export default {
  components: {
    Paging,
    // Toast
  },
  data() {
    return {
      info: null,
      categories: null,
      pageNumber: 1,
      pageSize: 10,
      orderProperty: 0,
      asc: true,
      ListofBrands: null,
      brandFilter: "",
      deletedProduct: "",
    };
  },
  computed: {
    pages() {
      let pages = [];
      for (let i = 2; i < this.info.totalPages; i++) {
        if (i <= this.pageNumber + 2 && i >= this.pageNumber - 2) pages.push(i);
      }
      return pages;
    },
  },
  methods: {
    async load() {
      this.info = await ProductRepo.getProducts(
        this.pageNumber,
        this.pageSize,
        this.orderProperty,
        this.asc,
        this.brandFilter
      );
    },
    async deleteItem(id, name) {
      if (confirm("item will be deleted")) {
        this.deletedProduct = name;
        await ProductRepo.deleteProduct(id);
        await this.load();
      }
    },
    async next() {
      if (this.pageNumber < this.info.totalPages) this.pageNumber++;
      await this.load();
    },
    async previous() {
      if (this.pageNumber > 1) this.pageNumber--;
      await this.load();
    },
    async selectedIndex(index) {
      this.pageNumber = index;
      await this.load();
    },
    async onChange(event) {
      this.pageNumber = 1;
      this.brandFilter = event.target.value;
      await this.load();
    },
    async orderby(property) {
      if (property != this.orderProperty) this.asc = true;
      else this.asc = !this.asc;
      this.orderProperty = property;
      await this.load();
    },
  },
  async created() {
    await this.load();
    this.ListofBrands = await BrandRepo.getBrandsName();
  },
};
</script>

<style scoped>
a {
  cursor: pointer;
}
.btn-group {
  width: 10%;
  position: inherit;
  display: table-cell;
}
.badge{
  border-radius: 0.75rem;
}


</style>
