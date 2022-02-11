<template>
  <div v-if="info !== null">
    {{info}}
  </div>
</template>

<script>
import { RepositoryFactory } from "../../services/repositoryFactory";
const ProductRepo = RepositoryFactory.get("products");
export default {
  data() {
    return {
      info: null
    };
  },
  async created() {
    this.info = await ProductRepo.getProductDetail(this.$route.params.id);
  },
};
</script>

<style scoped>
a {
  cursor: pointer;
}
</style>
