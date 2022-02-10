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
                v-for="item in ListofBrands"
                :key="item.id"
                :value="item.id"
              >
                {{ item.name }}
              </option>
            </select>
            <input type="text" v-model="productName" @keyup.enter="load()"/>
          </td>
        </tr>
      </tbody>
      <tbody>
        <tr v-for="(item, index) in info.listOfElements" :key="index">
          <td>{{ item.brandName }}</td>
          <td>{{ item.productName }}</td>
          <td>{{ item.name }}</td>
          <td>{{ item.requestedDate }}</td>
        </tr>
      </tbody>
    </table>

    <ul class="pagination d-flex justify-content-center">
      <li class="page-item" :class="this.pageNumber === 1 ? 'disabled' : ''">
        <a class="page-link" @click="previous()">Previous</a>
      </li>
      <li
        class="page-item"
        v-for="index in info.totalPages"
        :key="index"
        :class="pageNumber === index ? 'active' : ''"
      >
        <a
          class="page-link"
          v-if="
            (index <= pageNumber + 2 && index >= pageNumber - 2) ||
            index === info.totalPages ||
            index === 1
          "
          @click="selectedIndex(index)"
          >{{ index }}</a
        >
      </li>
      <li
        class="page-item"
        :class="this.pageNumber === this.info.totalPages ? 'disabled' : ''"
      >
        <a class="page-link" @click="next()">Next</a>
      </li>
    </ul>
  </div>
</template>

<script>
import { RepositoryFactory } from "../services/repositoryFactory";
const InforequestRepo = RepositoryFactory.get("inforequests");
const BrandRepo = RepositoryFactory.get("brands");
export default {
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
    async selectedIndex(index) {
      this.pageNumber = index;
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
