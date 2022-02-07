import brandServices from "../services/brandServices";
import productServices from "../services/productServices";


const repositories = {
  products: productServices,
  brands: brandServices
  // other repositories ...
};

export const repositoryFactory = {
  get: name => repositories[name]
};