<template>
  <div v-if="info !== null">
          <router-link to="/brand/new" class="nav-link">Aggiungi Brand</router-link>
    <table class="table table-striped table-hover">
      <thead>
        <tr>
          <th scope="col" class="text-end">
            <tr>
              <th class="w-100 text-start">Id</th>
            <th>

              <i class="bi bi-caret-up-fill d-flex"></i>
              <i class="bi bi-caret-down-fill d-flex"></i>

            </th>
            </tr>
          </th>
          <th scope="col">Nome Brand</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(item, object, index) in info.listOfElements" :key="index">
          <td>{{ item.id }}</td>
          <td>{{ item.name }}</td>
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
        v-bind:class="pageNumber === index ? 'active' : ''"
      >
        <a
          class="page-link"
          v-if="(index <= pageNumber + 2 && index >= pageNumber-2) ||
            index === info.totalPages
          "
          @click="selectedIndex(index)"
          >{{ index }}</a
        >
      </li>
      <li
        class="page-item"
        v-bind:class="this.pageNumber === this.info.totalPages ? 'disabled' : ''"
      >
        <a class="page-link" @click="next()">Next</a>
      </li>
    </ul>
  </div>
</template>

<script>
import { RepositoryFactory } from "../services/repositoryFactory";
const BrandRepo = RepositoryFactory.get("brands");
export default {
  data() {
    return {
      info: null,
      pageNumber: 1,
      pageSize: 10,
      orderProperty: 0,
      asc: true,
    };
  },
  methods: {
    async load() {
      this.info = await BrandRepo.getBrands(
        this.pageNumber,
        this.pageSize,
        this.orderProperty,
        this.asc
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
    async selectedIndex(index) {
      this.pageNumber = index;
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
  cursor: pointer
}
.iconcolor{
  color: red
}
.bi::before{
  line-height: 0.75
};
</style>
