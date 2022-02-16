<template>
  <div>
    <div class="col-8 offset-2">
      <div class="fs-3 my-5">Aggiungi Prodotto</div>
      <form @submit.prevent="createPost()" class="text-start form-group">
        <div class="mb-3">
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
      info: null,
      ProductId: null,
      Categories: null,
      formData: {
        Name: "",
        Price: 0,
        BrandId: "",
        ProductId: 0,
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
      if (this.info == null) {
        this.formData.ProductId = await ProductRepo.addProduct(product);
        console.log(this.formData.ProductId);
          this.$router.push({ path: "detail/" + this.formData.ProductId });

      } else {
        var id = await ProductRepo.updateProduct(product);
        if (id != 0)
          this.$router.replace({
            name: "productDetail",
            params: { id: this.formData.ProductId },
          });
      }
    },
  },
  async created() {
    this.Categories = await CatRepo.getCategories();
    this.ListofBrands = await BrandRepo.getBrandsName();
    this.info = await ProductRepo.getProductById(this.$route.params.id);
    if (this.info != null) {
      this.formData.Name = this.info.product.name;
      this.formData.Price = this.info.product.price;
      this.formData.BrandId = this.info.product.brandId;
      this.formData.ProductId = this.info.product.productId;
      this.ProdsCategories = this.info.categoriesSelected;
    }
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
input[type="checkbox"] {
  box-shadow: inset 0 2px 5px #ddd;
}
</style>
