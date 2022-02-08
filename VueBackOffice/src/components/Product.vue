<template>
  <div v-if="info !== null">

    <table class="table table-striped table-hover text-start">
      <thead>
        <tr>
          <th scope="col">
            <tr>
              <td class="w-100 text-start">Nome Brand</td>
              <td @click="orderby(1)">
                <i
                  v-if="this.asc === true && this.orderProperty === 1"
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
          <td colspan="4">
            <select class="form-select-sm w-auto" @change="onChange($event)">
              <option default value="">Tutti i brand</option>
              <option
                v-for="(item, index) in Listofnames"
                :key="index"
                :value="item"
              >
                {{ item }}
              </option>
            </select>
          </td>
        </tr>
      </tbody>
      <tbody>
        <tr v-for="(item, index) in info.listOfElements" :key="index">
          <td class="w-25 align-middle">{{ item.brandName }}</td>
          <td class="w-25 align-middle"><span>{{ item.name }}</span><span class="text-primary">{{item.shortDescription}}</span></td>
          <td class="text-start w-50 align-middle">
            <span
              class="badge bg-primary"
              v-for="(cat, i) in info.listOfElements[index].categories"
              :key="i"
              >{{ cat }}</span
            >
          </td>
          <td class="w-25 align-middle">{{ item.price }}€</td>
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
            (index === info.totalPages || index === 1)
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
const ProductRepo = RepositoryFactory.get("products");
const BrandRepo = RepositoryFactory.get("brands");
export default {
  data() {
    return {
      info: null,
      categories: null,
      pageNumber: 1,
      pageSize: 10,
      orderProperty: 0,
      asc: true,
      Listofnames: null,
      brandFilter: "",
      listOfproperties: [],
    };
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
      this.Listofnames = await BrandRepo.getBrandsName();
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
      if(property!=this.orderProperty)
        this.asc=true
      else
        this.asc = !this.asc;
      this.orderProperty = property;
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
.bi::before {
  line-height: 0.75;
}
.bi {
  line-height: 0.75;
  cursor: pointer;
}
.notselected {
  color: lightgray;
}
</style>
