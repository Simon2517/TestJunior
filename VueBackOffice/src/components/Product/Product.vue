<template>
  <div v-if="info !== null">
    
    <div class="text-end">
      <button>
        <router-link to="/product/new" class="nav-link"
          >Aggiungi Prodotto</router-link
        >
      </button>
    </div>
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
              <td class="w-100 text-start">Nome Prodotto</td>
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
          <th scope="col">Categorie</th>
          <th scope="col">
            <tr>
              <td class="w-100 text-start">Prezzo</td>
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
        <tr v-for="(item, index) in info.listOfElements" :key="index" @click.stop="$router.push({path:'product/detail/'+item.id})">
          <td class="w-25 align-middle">{{ item.brandName }}</td>
          <td class="w-25 align-middle">
            <span>{{ item.name }}</span
            ><span class="text-primary">{{ item.shortDescription }}</span>
          </td>
          <td class="text-start w-50 align-middle">
            <span
              class="badge bg-primary"
              v-for="(cat, i) in info.listOfElements[index].categories"
              :key="i"
              >{{ cat }}</span
            >
          </td>
          <td class="w-25 align-middle">{{ item.price }}€</td>
          <td>
            <i
              class="bi bi-pencil-square"
              @click.stop="$router.push({ path: 'product/' + item.id })"
            ></i>
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
    v-on:selectedIndex="selectedIndex"/>

  </div>
</template>

<script>
import { RepositoryFactory } from "../../services/repositoryFactory";
const ProductRepo = RepositoryFactory.get("products");
const BrandRepo = RepositoryFactory.get("brands");
import Paging from "../Paging/paging.vue"
export default {
    components:{
    Paging
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
    async next() {
      if (this.pageNumber < this.info.totalPages) this.pageNumber++;
      await this.load();
    },
    async previous() {
      if (this.pageNumber > 1) this.pageNumber--;
      await this.load();
    },
    async selectedIndex(page) {
      this.pageNumber = page.index;
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
</style>
