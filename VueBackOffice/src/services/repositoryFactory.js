import brandServices from "../services/brandServices";
import productServices from "../services/productServices";
import inforequestServices from "../services/inforequestServices"
import categoryrepository from "../services/categoryServices"


const repositories = {
  products: productServices,
  brands: brandServices,
  inforequests:inforequestServices,
  categories:categoryrepository
  // other repositories ...
};

export const RepositoryFactory = {
  get: name => repositories[name]
};