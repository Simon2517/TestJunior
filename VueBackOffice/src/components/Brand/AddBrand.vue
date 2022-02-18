<template>
  <div>
    <div class="col-8 offset-2">
      <div class="fs-3 my-5">Aggiungi Brand</div>
      <div
        v-show="Errors !== null"
        v-for="(item, index) in Errors"
        :key="index"
      >
        <div v-for="err in item" :key="err">
          <span class="badge bg-danger">{{ err }}</span>
        </div>
      </div>
      <form @submit.prevent="createPost()" class="text-start form-group">
        <div class="mb-3">
          <input
            placeholder="Brand Name"
            maxlength="200"
            class="form-control"
            type="text"
            v-model="formData.BrandName"
            required
          />
        </div>
        <div class="mb-3">
          <textarea
            placeholder="Description"
            class="form-control"
            rows="5"
            maxlength="200"
            type="textarea"
            v-model="formData.Description"
          />
        </div>
        <div class="mb-3">
          <input
            placeholder="Email"
            class="form-control"
            maxlength="200"
            type="email"
            v-model="formData.Account.Email"
            required
          />
        </div>
        <div class="mb-3">
          <label class="form-label" for="Password">Password</label>
          <input
            placeholder="Password"
            maxlength="200"
            class="form-control"
            type="password"
            v-model="formData.Account.Password"
            required
          />
        </div>
        <div class="d-table w-100 mb-2">
          <div class="d-table-cell align-bottom">
            <span class="fs-2">Prodotti </span>
          </div>
          <div class="d-table-cell text-end align-bottom">
            <button
              class="btn btn-primary btn-sm ms-2"
              @click.prevent="addProduct()"
            >
              Aggiungi Prodotto
            </button>
          </div>
        </div>
        <div
          class="mb-4"
          v-for="(item, index) in formData.Products"
          :key="index"
        >
          <div class="d-table w-100 my-3">
            <div class="d-table-cell align-bottom">
              <span class="text-primary">Prodotto #{{ index + 1 }}</span>
            </div>
            <div class="d-table-cell text-end align-bottom">
              <button
                class="btn btn-danger btn-sm ms-2"
                @click.prevent="formData.Products.splice(index, 1)"
              >
                <i class="bi bi-file-x-fill"></i>
              </button>
            </div>
          </div>
          <formProduct
            :formData.sync="item.Product"
            :ProdsCategories.sync="item.categoriesSelected"
          />
        </div>
        <div class="text-center my-5">
          <button class="btn btn-primary">create post</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { RepositoryFactory } from "../../services/repositoryFactory";
import formProduct from "../Product/formProduct.vue";
const CatRepo = RepositoryFactory.get("categories");
const BrandRepo = RepositoryFactory.get("brands");
export default {
  name: "CreatePost",
  components: { formProduct },
  data() {
    return {
      Errors: null,
      BrandId: null,
      Categories: null,
      formData: {
        BrandName: "",
        Account: { Email: "", Password: "", Description: "" },
        Products: [],
      },
    };
  },
  methods: {
    async createPost() {
      if (!isNullOrWhiteSpaces(this.formData.BrandName)) {
        let brand = {
          brand: this.formData,
          prodCategories: this.formData.Products,
        };
        await BrandRepo.addBrand(brand)
          .then((response) => {
            this.BrandId = response.data;
          })
          .catch((error) => {
            if (error.response && error.response.status === 400) {
              this.Errors = error.response.data;
            }
          });
        if (this.BrandId != 0 && this.BrandId != undefined)
          this.$router.push({ path: "detail/" + this.BrandId });
      }
    },
    addProduct() {
      this.formData.Products.push({
        Product: { name: "", price: 0, description: "", shortDescription: "" },
        categoriesSelected: [],
      });
    },
    checkForm: function () {},
  },
  async created() {
    this.Categories = await CatRepo.getCategories();
  },
};

function isNullOrWhiteSpaces(str) {
  return str === null || str.match(/^ *$/) !== null;
}
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
