<template>
  <div>
    Aggiungi Prodotto
    <div class="col-8 offset-2">
      <form @submit.prevent="createPost()" class="text-start">
        <div>
          <label class="form-label" for="BrandName">Product Name</label>
          <input class="form-control" type="text" v-model="formData.Name" />
        </div>
        <div>
          <label class="form-label" for="Email">Prezzo</label>
          <input class="form-control" type="text" v-model="formData.Price" />
        </div>
        <div>
          <select class="form-select-sm w-auto" v-model="formData.Brand.Id">
            <option default>Tutti i brand</option>
            <option
              v-for="brand in ListofBrands"
              :key="brand.id"
              :value="brand.id"
            >
              {{ brand.name }}
            </option>
          </select>
        </div>
        <div
          class="d-inline-flex form-check"
          v-for="item in Categories"
          :key="item.id"
        >
          <input
            class="form-check-input"
            type="checkbox"
            :value="item.id"
            v-model="ProdsCategories"
          />
          <label for="">{{ item.name }}</label>
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
const ProductRepo = RepositoryFactory.get("products");
export default {
  name: "CreatePost",
  data() {
    return {
      ProductId: null,
      Categories: null,
      formData: {
        Name: "",
        Price: 0,
        Brand: { Id: 0, Name: "" },
      },
      ProdsCategories: [],
      BrandName:'',
      ListofBrands: null,
    };
  },
  methods: {
    async createPost() {
      let product = {
        Product: this.formData,
        categoriesSelected: this.ProdsCategories,
      };
      this.ProductId = await ProductRepo.addProduct(product);
    },

  },
  async created() {
    this.Categories = await CatRepo.getCategories();
    this.ListofBrands = await BrandRepo.getBrandsName();
  },
};
</script>
