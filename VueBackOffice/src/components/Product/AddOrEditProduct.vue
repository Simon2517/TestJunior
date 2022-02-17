<template>
  <div>
    <div class="col-8 offset-2">
      <div v-if="formData.productId===0" class="fs-3 my-5">Aggiungi Prodotto</div>
      <div v-else class="fs-3 my-5">Modifica Prodotto</div>
      <form @submit.prevent="createPost()" class="text-start form-group">
        <!-- <div class="mb-3">
          <input
            placeholder="Product Name"
            class="form-control"
            type="text"
            v-model="formData.Name"
            required
          />
        </div>
        <div class="mb-3">
          <label class="form-label" for="Price">Price</label>
          <input
            class="form-control"
            type="number"
            min="0"
            max="999999999999999999"
            step=".01"
            v-model.number="formData.Price"
            required
          />
        </div>
        <div>
          <select
            class="form-select-sm w-auto mb-4"
            v-model="formData.BrandId"
            required
          >
            <option default :value="''">Tutti i brand</option>
            <option
              v-for="brand in ListofBrands"
              :key="brand.id"
              :value="brand.id"
            >
              {{ brand.name }}
            </option>
          </select>
        </div>
        <div class="row m-0">
          <div
            class="form-check col-3"
            v-for="item in Categories"
            :key="item.id"
          >
            <input
              class="form-check-input me-1"
              type="checkbox"
              :value="item.id"
              v-model="ProdsCategories"
            />
            <label for="">{{ item.name }}</label>
          </div>
        </div>
        <div class="text-center">
          <button class="btn btn-primary mt-4">create post</button>
        </div>-->

        <formProduct
          :formData.sync="formData"
          :ProdsCategories.sync="ProdsCategories"    
              
        />
             <button class="btn btn-primary mt-4">create post</button>
      </form>
    </div>
  </div>
</template>

<script>
import { RepositoryFactory } from "../../services/repositoryFactory";
import formProduct from "./formProduct.vue";
// const CatRepo = RepositoryFactory.get("categories");
// const BrandRepo = RepositoryFactory.get("brands");
const ProductRepo = RepositoryFactory.get("products");
// import formProduct from "../Product/formProduct.vue";
export default {
  components: { formProduct },
  name: "CreatePost",
  data() {
    return {
      info: null,
      formData: {
        name: "",
        price: 0,
        brandId: "",
        productId: 0,
        description:"",
        shortDescription:""
      },
      ProdsCategories: [],
    };
  },
  methods: {
    async createPost() {
      let product = {
        Product: this.formData,
        categoriesSelected: this.ProdsCategories,
      };
      if (this.formData.productId == 0) {
        this.formData.productId = await ProductRepo.addProduct(product);
        this.$router.push({ path: "detail/" + this.formData.productId });
      } 
      else {
        console.log(this.formData.productId);
        let id= await ProductRepo.updateProduct(product);
        if (id != 0)
          this.$router.replace({
            name: "productDetail",
            params: { id: this.formData.productId },
          });
      }
    },
  },
  async created() {
    // this.Categories = await CatRepo.getCategories();
    // this.ListofBrands = await BrandRepo.getBrandsName();
    this.info = await ProductRepo.getProductById(this.$route.params.id);
    if (this.info != null) {
      this.formData=this.info.product
       this.ProdsCategories = this.info.categoriesSelected;
    }
  },
};
</script>


