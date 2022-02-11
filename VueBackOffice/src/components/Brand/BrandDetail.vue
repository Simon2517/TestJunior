<template>
  <div v-if="info !== null" class="col-6 offset-2">
    <div>{{ info.name }}</div>

    <div class="container-fluid text-start">
      <div class="row">
              <div class="col-4" v-for="cat in info.listOfCategs" :key="cat.id">
      {{ cat.name }} ({{ cat.numOfProds }})
    </div>
      </div>
    </div>
<table class="table">
    <tr v-for="prod in info.listOfProds" :key="prod.ProductId">
      <td>{{ prod.productId }}</td>
      <td>{{ prod.productName }}</td>
      <td>{{ prod.numOfInforequest }}</td>
    </tr>
    </table>
    <Paging 
    :pageNumber="1"
    :pageSize="3"
    :totalPages="info.numOfProducts"
    v-on:next="next"
    v-on:previous="previous"
    v-on:selectedIndex="selectedIndex"/>
    {{ info.numOfInforequests }}
  </div>
</template>

<script>
import { RepositoryFactory } from "../../services/repositoryFactory";
import Paging from '../Paging/paging.vue';
const BrandRepo = RepositoryFactory.get("brands");
export default {
    components:{
    Paging
  },
  data() {
    return {
      info: null,
    };

  },
  methods:{
    next(){

    }
  },
  async created() {
    this.info = await BrandRepo.getBrandDetail(this.$route.params.id);
  },
};
</script>

<style scoped>
a {
  cursor: pointer;
}
</style>
