<template>
  <div v-if="info !== null">
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
                <i v-if="this.asc === true" class="bi bi-caret-up-fill d-flex">
                </i>
                <i v-else class="bi bi-caret-up-fill d-flex notselected"> </i>

                <i
                  v-if="this.asc === false"
                  class="bi bi-caret-down-fill d-flex"
                ></i>
                <i v-else class="bi bi-caret-down-fill d-flex notselected"> </i>
              </td>
            </tr>
          </th>
        </tr>
      </thead>
      <tbody class="text-start">
        <tr>
          <td colspan="4">
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
            <input type="text" v-model="productName" @keyup.enter="load()"/>
          </td>
        </tr>
      </tbody>
      <tbody>
        <tr v-for="(item, index) in info.listOfElements" :key="index" @click.stop="$router.push({path:'leed/detail/'+item.id})">
          <td>{{ item.brandName }}</td>
          <td>{{ item.productName }}</td>
          <td>{{ item.name }}</td>
          <td>{{ item.requestedDate }}</td>
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
const InforequestRepo = RepositoryFactory.get("inforequests");
const BrandRepo = RepositoryFactory.get("brands");
import Paging from "../Paging/paging.vue"
export default {
    components:{
    Paging
  },
  data() {
    return {
      info: null,
      pageNumber: 1,
      pageSize: 10,
      asc: false,
      ListofBrands: null,
      brandId: 0,
      productName: "",
    };
  },
  methods: {
    async load() {
      this.info = await InforequestRepo.getRequests(
        this.pageNumber,
        this.pageSize,
        this.asc,
        this.brandId,
        this.productName
      );
      this.ListofBrands = await BrandRepo.getBrandsName();
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
    async selectedIndex(page) {
      this.pageNumber = page.index;
      await this.load();
    },
    async orderBy() {
      this.asc = !this.asc;
      await this.load();
    },
  },
  async created() {
    await this.load();
  },
};
</script>

<style scoped>
a {
  cursor: pointer;
}
</style>
