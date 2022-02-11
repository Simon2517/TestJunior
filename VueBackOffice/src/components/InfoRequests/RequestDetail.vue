<template>
  <div v-if="info !== null">
    {{info}}
  </div>
</template>

<script>
import { RepositoryFactory } from "../../services/repositoryFactory";
const InfoRepo = RepositoryFactory.get("inforequests");
export default {
  data() {
    return {
      info: null
    };
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
