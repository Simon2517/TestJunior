<template>
  <div v-if="info !== null" class="col-8 offset-2 mt-5">
    <div class="text-start fs-5 mb-4">
      <span class="d-block">
        Richiesta di informazioni di {{ info.name }}
        <span class="d-block">
          per il prodotto {{ info.product.productName }}
        </span>
      </span>
    </div>
    <div class="mb-4">
      <span class="fw-bold d-block fs-6"> Dati del richiedente: </span>
      <span class="d-block fs-6">
        {{ info.name }}
      </span>
      <span class="d-block fs-6">{{ info.address }}</span>
    </div>
    <div>
      <span class="fs-6">
        <span class="d-block fw-bold mb-2"> Richiesta inviata dall'utente: </span>
        <span class="d-block">{{ info.requestText }} </span>
      </span>
    </div>
    <div>
      <div class="mb-2 mt-5"><span class="fw-bold fs-6"> Risposte/commenti alla Richiesta</span></div>
      <div v-for="(item, index) in listOfReplies" :key="index">
        <div class="card border-success mb-3">
          <div class="card-header">{{insertedDate(item.insertedDate)}} - {{item.name}}</div>
          <div class="card-body text-success">
            <p class="card-text">
              {{item.replyText}}
            </p>
          </div>
        </div>
      </div>
    </div>

    <Paging
      :pageNumber="pageNumber"
      :pageSize="pageSize"
      :totalPages="totalPages"
      v-on:next="next"
      v-on:previous="previous"
      v-on:selectedIndex="selectedIndex"
    />
  </div>
</template>

<script>
import { RepositoryFactory } from "../../services/repositoryFactory";
const InfoRepo = RepositoryFactory.get("inforequests");
import Paging from "../Generics/paging.vue";

export default {
  components: {
    Paging,
  },
  data() {
    return {
      info: null,
      pageNumber: 1,
      pageSize: 2,
    };
  },
  
  computed: {
    startIndex() {
      return (this.pageNumber - 1) * this.pageSize;
    },
    endIndex() {
      return this.startIndex + this.pageSize;
    },
    totalPages() {
      return Math.ceil(this.info.replies.length / this.pageSize);
    },
    listOfReplies() {
      let list = [...this.info.replies];
      return list.slice(this.startIndex, this.endIndex);
    },
  },
  methods: {
    next() {
      this.pageNumber++;
    },
    previous() {
      this.pageNumber--;
    },
    selectedIndex(index) {
      this.pageNumber = index;
    },
        insertedDate(date) {
      return new Date(date).toLocaleDateString("IT");
    },
  },
  async created() {
    this.info = await InfoRepo.getRequestDetail(this.$route.params.id);
  },
};
</script>

<style scoped>
a {
  cursor: pointer;
}
</style>
