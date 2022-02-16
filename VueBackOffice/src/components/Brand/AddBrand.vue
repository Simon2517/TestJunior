<template>
  <div>
    <div class="col-8 offset-2">
      <div class="fs-3 my-5">Aggiungi Brand</div>
      <form @submit.prevent="createPost()" class="text-start form-group">
        <div class="mb-3">
          <input
            placeholder="Brand Name"
            maxlength="200"
            class="form-control"
            type="text"
            v-model="formData.BrandName"
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
          <div class="mb-2">
            <input
              placeholder="Product Name"
              class="form-control"
              min="0"
              type="text"
              v-model="item.Product.Name"
              required
            />
          </div>
          <div class="mb-3">
            <label class="form-label" for="Price">Price</label>
            <input
              step=".01"
              class="form-control"
              type="number"
              v-model.number="item.Product.Price"
              required
            />
          </div>
          <div class="row m-0">
            <div
              class="col-3 form-check"
              v-for="cat in Categories"
              :key="cat.id"
            >
              <input
                class="form-check-input me-1"
                type="checkbox"
                :value="cat.id"
                v-model="item.categoriesSelected"
              />
              <label>{{ cat.name }}</label>
            </div>
          </div>
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
const CatRepo = RepositoryFactory.get("categories");
const BrandRepo = RepositoryFactory.get("brands");
export default {
  name: "CreatePost",
  data() {
    return {
      Errors: [],
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
      if (!isNullOrWhiteSpaces(this.formData.BrandName)) {
        let brand = {
          brand: this.formData,
          prodCategories: this.formData.Products,
        };
        this.BrandId = await BrandRepo.addBrand(brand);
        if (this.BrandId != 0)
          this.$router.push({ path: "detail/" + this.BrandId });
      }
    },
    addProduct() {
      this.formData.Products.push({
        Product: { Name: "", Price: 0 },
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
