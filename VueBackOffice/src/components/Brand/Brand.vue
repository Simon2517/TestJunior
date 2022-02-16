<template>
  <div v-if="info !== null" class="mt-5">
    <div class="d-table w-100 mb-3">
      <div class="d-table-cell align-bottom">
        <span class="fs-2">Brands </span>
      </div>
      <div class="d-table-cell text-end">
        <button
          type="button"
          class="btn btn-outline-primary"
          @click.stop="$router.push({ path: 'brand/new' })"
        >
          Aggiungi Brand
        </button>
      </div>
    </div>
    <div class="col-3 mb-3">
      <input
        type="text"
        class="form-control"
        placeholder="Brand Name"
        v-model="brandName"
        @keyup="search()"
        @keyup.enter="load()"
      />
    </div>
    <hr class="m-0 my-1" />
    <table class="table table-striped table-hover">
      <thead>
        <tr>
          <th scope="col">Id</th>
          <th scope="col" class="text-start">Nome Brand</th>
          <th scope="col">Description</th>
        </tr>
      </thead>
      <tbody>
        <tr
          class="detail"
          v-for="(item, index) in info.listOfElements"
          :key="index"
          @click.stop="$router.push({ path: 'brand/detail/' + item.id })"
        >
          <td>{{ item.id }}</td>
          <td class="w-25 text-start">{{ item.name }}</td>
          <td class="text-start">{{ item.description }}</td>
          <td class="update_delete align-middle btn-group text-center">
            <button
              class="btn btn-outline-secondary px-2 py-1"
              @click.stop="$router.push({ path: 'brand/' + item.id + '/edit' })"
            >
              <i class="bi bi-pencil-square"></i>
            </button>
            <button
              class="btn btn-outline-secondary px-2 py-1"
              @click.stop="deleteItem(item.id)"
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
  </div>
</template>

<script>
import Paging from "../Generics/paging.vue";
import { RepositoryFactory } from "../../services/repositoryFactory";
import brandServices from "../../services/entityService/brandServices";
const BrandRepo = RepositoryFactory.get("brands");
export default {
  components: {
    Paging,
  },
  data() {
    return {
      info: null,
      pageNumber: 1,
      pageSize: 10,
      brandName: "",
    };
  },
  methods: {
    async load() {
      this.info = await BrandRepo.getBrands(
        this.pageNumber,
        this.pageSize,
        this.brandName
      );
    },
    async deleteItem(id) {
      await brandServices.deleteBrand(id);
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
    async search() {
      if (this.brandName.length > 3) await this.load();
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
.btn-group {
  width: 10%;
  position: inherit;
  display: table-cell;
}
</style>
