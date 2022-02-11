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
          <label class="form-label" for="Price">Prezzo</label>
          <input class="form-control" type="number" step=".01" v-model.number="formData.Price" />
        </div>
        <div>
          <select class="form-select-sm w-auto" v-model="formData.BrandId">
            <option :value="0">Tutti i brand</option>
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
      info:null,
      ProductId: null,
      Categories: null,
      formData: {
        Name: "",
        Price: 0,
        BrandId: 0
      },
      ProdsCategories: [],
      ListofBrands: null,
    };
  },
  methods: {
    async createPost() {
      let product = {
        Product: this.formData,
        categoriesSelected: this.ProdsCategories,
      };
      if(this.info==null)
         this.ProductId = await ProductRepo.addProduct(product);
      else
         await ProductRepo.updateProduct(product);
    },
  },
  async created() {
    
    this.Categories = await CatRepo.getCategories();
    this.ListofBrands = await BrandRepo.getBrandsName();
    this.info=await ProductRepo.getProductById(this.$route.params.id);
    if(this.info!=null){
      this.formData.Name=this.info.product.name;
      this.formData.Price=this.info.product.price;
      this.formData.BrandId=this.info.product.brandId;
      this.ProdsCategories=this.info.categoriesSelected;
      }
  },
};
</script>
