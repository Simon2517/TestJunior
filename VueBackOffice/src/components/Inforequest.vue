<template>
  <div v-if="info !== null">
    <table class="table table-striped table-hover">
      <thead>
        <tr>
          <th scope="col">Nome Brand</th>
          <th scope="col">Nome Prodotto</th>
          <th scope="col">Nome Richiedente</th>
          <th scope="col">Data Richiesta</th>
        </tr>
      </thead>
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
import { RepositoryFactory } from "../services/repositoryFactory";
const InforequestRepo = RepositoryFactory.get("inforequests");
export default {
  data() {
    return {
      info: null,
      pageNumber: 1,
      pageSize: 10,
      asc: false,
    };
  },
  methods: {
    async load() {
      this.info = await InforequestRepo.getRequests(
        this.pageNumber,
        this.pageSize,
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
  cursor: pointer;
}
</style>
