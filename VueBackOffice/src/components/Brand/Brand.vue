<template>
  <div v-if="info !== null">
    <router-link to="/brand/new" class="nav-link">Aggiungi Brand</router-link>
    <table class="table table-striped table-hover">
      <thead>
        <tr>
          <th scope="col">Id</th>
          <th scope="col">Nome Brand</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(item, index) in info.listOfElements"
          :key="index"
          @click.stop="$router.push({ path: 'brand/detail/' + item.id })"
        >
          <td>{{ item.id }}</td>
          <td>{{ item.name }}</td>
          <td>{{ item.description }}</td>
          <td>
            <i
              class="bi bi-pencil-square"
              @click.stop="$router.push({ path: 'brand/' + item.id })"
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
    <!-- <ul class="pagination d-flex justify-content-center">
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
        v-bind:class="
          this.pageNumber === this.info.totalPages ? 'disabled' : ''
        "
      >
        <a class="page-link" @click="next()">Next</a>
      </li>
    </ul> -->
  </div>
</template>

<script>
import Paging from "../Paging/paging.vue"
import { RepositoryFactory } from "../../services/repositoryFactory";
const BrandRepo = RepositoryFactory.get("brands");
export default {
  components:{
    Paging
  },
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
    async selectedIndex(page) {
      this.pageNumber = page.index;
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
</style>
