<template>
  <div>
    <div class="col-8 offset-2">
      <div v-if="formData.productId === 0" class="fs-3 my-5">
        Aggiungi Prodotto
      </div>
      <div v-else class="fs-3 my-5">Modifica Prodotto</div>
      <form @submit.prevent="createPost()" class="text-start form-group">
        <formProduct
          :formData="formData"
          v-model="formData"
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
const ProductRepo = RepositoryFactory.get("products");
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
        description: "",
        shortDescription: "",
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
      } else {
        console.log(this.formData.productId);
        let id = await ProductRepo.updateProduct(product);
        if (id != 0)
          this.$router.replace({
            name: "productDetail",
            params: { id: this.formData.productId },
          });
      }
    },
  },
  async created() {
    this.info = await ProductRepo.getProductById(this.$route.params.id);
    if (this.info != null) {
      this.formData = this.info.product;
      this.ProdsCategories = this.info.categoriesSelected;
    }
  },
};
</script>
