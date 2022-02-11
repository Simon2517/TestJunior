<template>
  <div>
    Update Brand
    <div class="col-8 offset-2">
      <form @submit.prevent="createPost()" class="text-start">
        <div>
          <label class="form-label" for="BrandName">Brand Name</label>
          <input
            class="form-control"
            type="text"
            v-model="formData.BrandName"
          />
        </div>
        <div>
          <label class="form-label" for="Email">Email</label>
          <input
            class="form-control"
            type="text"
            v-model="formData.Account.email"
          />
        </div>
        <div>
          <label class="form-label" for="Password">Password</label>
          <input
            class="form-control"
            type="text"
            v-model="formData.Account.password"
          />
        </div>
        <div class="text-center">
          <button>create post</button>
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
        Description:""
      },
    };
  },
  methods: {
    async createPost() {
      await BrandRepo.updateBrand(this.formData);
      this.$router.push({path:'detail/'+this.$route.params.id})
    }
  },
  async created(){
    let info=await BrandRepo.getBrandById(this.$route.params.id);
    this.formData.BrandName=info.brandName;
    this.formData.Id=info.id;
    this.formData.Account=info.account;
    this.formData.Description=info.description;
  }
}
</script>
