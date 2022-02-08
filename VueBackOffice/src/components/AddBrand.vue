<template>
  <div>
    Aggiungi Brand

    <form @submit.prevent="createPost">
      <div>
        <label for="BrandName">Brand Name</label>
        <input type="text" v-model="formData.BrandName" />
      </div>
      <div>
        <label for="AccountId">Account ID</label>
        <input type="number" v-model="formData.AccountId" />
      </div>
      <div>
        <label for="Products.Name">ProductName</label>
        <input type="text" v-model="formData.Products[0].Name" />
      </div>
      <div>
        <label for="Products.Price">Price</label>
        <input type="number" v-model="formData.Products[0].Price" />
      </div>
      <button>create post</button>
    </form>
  </div>
</template>

<script>
import { RepositoryFactory } from "../services/repositoryFactory";
const BrandRepo = RepositoryFactory.get("brands");
export default {
  name: "CreatePost",
  data() {
    return {
      formData: {
        BrandName: "",
        AccountId: 0,
        Products: [
          {
            Name: "",
            Price: 0,
          },
        ],
      },
    };
  },
  methods: {
    createPost() {
      this.formData.AccountId = parseInt(this.formData.AccountId);
      BrandRepo.addBrand(this.formData);
    },
  },
};
</script>
