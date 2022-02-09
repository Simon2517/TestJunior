<template>
  <div>
    Aggiungi Brand
    <div class="col-8 offset-2">
      <form @submit.prevent="createPost()">
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
            v-model="formData.Account.Email"
          />
        </div>
        <div>
          <label class="form-label" for="Password">Password</label>
          <input
            class="form-control"
            type="text"
            v-model="formData.Account.Password"
          />
        </div>
        <button @click.prevent="addProduct()">add product</button>
        <div v-for="(item, index) in formData.Products" :key="index">
          <div>
            <label class="form-label" for="Name">ProductName</label>
            <input class="form-control" type="text" v-model="item.Name" />
          </div>
          <div>
            <label class="form-label" for="Price">Price</label>
            <input
              class="form-control"
              type="number"
              v-model.number="item.Price"
            />
          </div>
        </div>
        <button>create post</button>
      </form>
    </div>
  </div>
</template>

<script>
import { RepositoryFactory } from "../services/repositoryFactory";
const CatRepo = RepositoryFactory.get("categories");
const BrandRepo = RepositoryFactory.get("brands");
export default {
  name: "CreatePost",
  data() {
    return {
      BrandId:null,
      formData: {
        BrandName: "",
        Account: { Email: "", Password: "" },
        Products: [],
        Categories:null
      }
      
    };
  },
  methods: {
    async createPost() {
      this.BrandId=await BrandRepo.addBrand(this.formData);
    },
    addProduct() {
      this.formData.Products.push({});
    },
  },
  async created(){
    this.formData.Categories=await CatRepo.getCategories();
  }
};
</script>
