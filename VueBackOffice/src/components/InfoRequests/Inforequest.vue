<template>
  <div>
    <div v-if="isLoading === true" class="mt-5 text-center">
      <button class="btn btn-primary" type="button" disabled>
        <span
          class="spinner-border spinner-border-sm"
          role="status"
          aria-hidden="true"
        ></span>
        Retrieving Data
      </button>
    </div>
    <div v-if="isLoading === false" class="mt-5">
      <div class="fs-2">Leads</div>
      <hr class="m-0 my-1" />
      <table class="table table-striped table-hover">
        <thead>
          <tr>
            <th scope="col">Nome Brand</th>
            <th scope="col">Nome Prodotto</th>
            <th scope="col">Nome Richiedente</th>
            <th scope="col">
              <tr>
                <td class="w-100 text-start">Data Richiesta</td>
                <td @click="orderBy()">
                  <i
                    v-if="this.asc === true"
                    class="bi bi-caret-up-fill d-flex"
                  >
                  </i>
                  <i v-else class="bi bi-caret-up-fill d-flex notselected"> </i>

                  <i
                    v-if="this.asc === false"
                    class="bi bi-caret-down-fill d-flex"
                  ></i>
                  <i v-else class="bi bi-caret-down-fill d-flex notselected">
                  </i>
                </td>
              </tr>
            </th>
          </tr>
        </thead>
        <tbody class="text-start">
          <tr>
            <td>
              <select class="form-select-sm w-auto" @change="onChange($event)">
                <option default value="">Tutti i brand</option>
                <option
                  v-for="brand in ListofBrands"
                  :key="brand.id"
                  :value="brand.id"
                >
                  {{ brand.name }}
                </option>
              </select>
            </td>
            <td>
              <input
                type="text"
                placeholder="Nome Prodotto"
                v-model="productName"
                @keyup="search()"
                @keyup.enter="load()"
              />
            </td>
            <td colspan="100%"></td>
          </tr>
        </tbody>
        <tbody>
          <tr
            class="detail"
            v-for="(item, index) in info.listOfElements"
            :key="index"
            @click.stop="$router.push({ path: 'leed/detail/' + item.id })"
          >
            <td>{{ item.brandName }}</td>
            <td>{{ item.productName }}</td>
            <td>{{ item.name }}</td>
            <td>{{ requestedDate(item.requestedDate) }}</td>
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
    </div>
  </div>
</template>

<script>
import { RepositoryFactory } from "../../services/repositoryFactory";
const InforequestRepo = RepositoryFactory.get("inforequests");
const BrandRepo = RepositoryFactory.get("brands");
import Paging from "../Generics/paging.vue";
export default {
  components: {
    Paging,
  },
  data() {
    return {
      isLoading: true,
      info: null,
      pageNumber: 1,
      pageSize: 10,
      asc: false,
      ListofBrands: null,
      brandId: 0,
      productName: "",
    };
  },
  computed: {
    productId() {
      if (this.$route.params.productId != null)
        return this.$route.params.productId;
      else return 0;
    },
  },
  methods: {
    async load() {
      this.info = await InforequestRepo.getRequests(
        this.pageNumber,
        this.pageSize,
        this.asc,
        this.brandId,
        this.productId,
        this.productName
      );
    },
    async onChange(event) {
      this.pageNumber = 1;
      this.brandId = event.target.value;
      await this.load();
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
    async orderBy() {
      this.asc = !this.asc;
      await this.load();
    },
    async search() {
      if (this.productName.length > 3) await this.load();
    },
    requestedDate(date) {
      return new Date(date).toLocaleDateString("IT");
    },
  },
  async created() {
    await this.load();
    this.ListofBrands = await BrandRepo.getBrandsName();
    this.isLoading = false;
  },
};
</script>

<style scoped>
a {
  cursor: pointer;
}
</style>
