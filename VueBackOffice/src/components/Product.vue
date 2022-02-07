<template>
  <div v-if="info !== null">
    <table class="table table-striped table-hover text-start">
      <thead>
        <tr>
          <th scope="col">Nome Brand</th>
          <th scope="col">Nome Prodotto</th>
          <th scope="col">Categorie</th>
          <th scope="col">Prezzo</th>
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
          <td class="w-25 align-middle">{{ item.name }}</td>
          <td class="text-start w-50 align-middle">
            <span
              class="badge bg-success"
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
            (index <= pageNumber + 2 && index >= pageNumber) ||
            index === info.totalPages
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
import brandServices from "../services/brandServices";
import productServices from "../services/productServices";
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
    };
  },
  methods: {
    async load() {
      this.info = await productServices.getProducts(
        this.pageNumber,
        this.pageSize,
        this.orderProperty,
        this.asc,
        this.brandFilter
      );
      this.Listofnames = await brandServices.getBrandsName();
      this.categories = this.info.listOfElements[0].categories;
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
      this.pageNumber=1
      this.brandFilter = event.target.value;
      await this.load();
    },
  },
  async created() {
    await this.load();
  },
};
</script>

<style>
a {
  cursor: pointer;
};
.t_body{
  border: 1px solid black;
};
.remove-border {
  border-collapse:initial;
};
table {
  border-collapse: initial;
}
</style>
