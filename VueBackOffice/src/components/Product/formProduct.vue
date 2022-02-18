<template>
  <div>
    <div class="mb-3">
      <input
        placeholder="Product Name"
        class="form-control"
        type="text"
        v-model="productForm.name"
        required
      />
    </div>
    <div class="mb-3">
      <label class="form-label" for="Price">Price</label>
      <input
        class="form-control"
        type="number"
        min="0"
        step=".01"
        v-model.number="productForm.price"
        required
      />
    </div>
    <div class="mb-3">
      <textarea
        class="form-control"
        placeholder="Description"
        v-model="productForm.description"
        required
      />
    </div>
    <div class="mb-3">
      <input
        class="form-control"
        type="text"
        placeholder="ShortDescription"
        v-model="productForm.shortDescription"
        required
      />
    </div>
    <div v-if="$route.path !== '/brand/new'">
      <select
        class="form-select-sm w-auto mb-4"
        v-model="productForm.brandId"
        required
      >
        <option default :value="''">Tutti i brand</option>
        <option v-for="brand in brands" :key="brand.id" :value="brand.id">
          {{ brand.name }}
        </option>
      </select>
    </div>
    <div class="row m-0">
      <div class="form-check col-3" v-for="item in categories" :key="item.id">
        <input
          class="form-check-input me-1"
          type="checkbox"
          :value="item.id"
          v-model="listOfCategs"
          @input="$emit('update:ProdsCategories', listOfCategs)"
        />
        <label for="">{{ item.name }}</label>
      </div>
    </div>
    <div class="text-center"></div>
  </div>
</template>

<script>
import { RepositoryFactory } from "../../services/repositoryFactory";
const CatRepo = RepositoryFactory.get("categories");
const BrandRepo = RepositoryFactory.get("brands");
export default {
  props: {
    formData: { type: Object },
    ProdsCategories: { type: Array },
  },
  data() {
    return {
      brands: null,
      categories: null,
      categorylist: this.ProdsCategories,
    };
  },
  computed: {
    productForm() {
      return this.formData;
    },
    listOfCategs: {
      get() {
        return this.ProdsCategories;
      },
      set(value) {
        this.$emit("update:ProdsCategories", value);
      },
    },
  },
  async created() {
    this.categories = await CatRepo.getCategories();
    this.brands = await BrandRepo.getBrandsName();
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
