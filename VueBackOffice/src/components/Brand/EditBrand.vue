<template>
  <div>
    <div class="col-8 offset-2">
      <div class="fs-3 my-5">Modifica Brand</div>
      <form @submit.prevent="createPost()" class="text-start">
        <div>
          <label class="form-label" for="Email">Brand Name</label>
          <input
            class="form-control"
            placeholder="Brand Name"
            type="text"
            maxlength="200"
            v-model="formData.BrandName"
          />
        </div>
        <div>
          <label class="form-label" for="Email">Email</label>
          <input
            class="form-control"
            placeholder="Email"
            maxlength="200"
            type="email"
            v-model="formData.Account.email"
          />
        </div>
        <div>
          <label class="form-label" for="Password">Password</label>
          <input
            class="form-control"
            placeholder="Password"
            maxlength="200"
            type="password"
            v-model="formData.Account.password"
          />
        </div>
        <div class="text-center my-4">
          <button class="btn btn-primary">create post</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { RepositoryFactory } from "../../services/repositoryFactory";
const BrandRepo = RepositoryFactory.get("brands");
export default {
  name: "CreatePost",
  data() {
    return {
      formData: {
        Id: 0,
        BrandName: "",
        Account: {},
        Description: "",
      },
    };
  },
  methods: {
    async createPost() {
      var id = await BrandRepo.updateBrand(this.formData);
      if (id != 0)
        this.$router.push({ name: "brandDetail",params:{id: this.$route.params.id}});
    },
  },
  async created() {
    let info = await BrandRepo.getBrandById(this.$route.params.id);
    this.formData.BrandName = info.brandName;
    this.formData.Id = info.id;
    this.formData.Account = info.account;
    this.formData.Description = info.description;
  },
};
</script>

<style scoped>
textarea {
  resize: none;
}
.form-control {
  background: lightgray;
}
</style>
