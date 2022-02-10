<template>
  <div>
    Aggiungi Brand
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
        <div class="text-end">
          <button @click.prevent="addProduct()">add product</button>
        </div>
        <div v-for="(item, index) in formData.Products" :key="index">
          <div>
            <label class="form-label" for="Name">ProductName</label>
            <input class="form-control" type="text" v-model="item.Product.Name" />
          </div>
          <div>
            <label class="form-label" for="Price">Price</label>
            <input
              class="form-control"
              type="number"
              v-model.number="item.Product.Price"
            />
          </div>
          <div
            class="d-inline-flex form-check"
            v-for="cat in Categories"
            :key="cat.id"
          >
            <input
              class="form-check-input"
              type="checkbox"
              :value="cat.id"
              v-model="item.categoriesSelected"
            />
            <label for="">{{ cat.name }}</label>
          </div>
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
const CatRepo = RepositoryFactory.get("categories");
const BrandRepo = RepositoryFactory.get("brands");
export default {
  name: "CreatePost",
  data() {
    return {
      BrandId: null,
      Categories: null,
      formData: {
        BrandName: "",
        Account: { Email: "", Password: "" },
        Products: [],
      },

      
    };
  },
  methods: {
    async createPost() {
      let brand={brand:this.formData,prodCategories:this.formData.Products}
      this.BrandId = await BrandRepo.addBrand(brand);
    },
    addProduct() {
      this.formData.Products.push({Product:{Name:'',Price:0},categoriesSelected:[]});
      
    }
  },
  async created() {
    this.Categories = await CatRepo.getCategories();
  },
};
</script>
