import brandServices from "../services/entityService/brandServices";
import productServices from "../services/entityService/productServices";
import inforequestServices from "../services/entityService/inforequestServices"
import categoryrepository from "../services/entityService/categoryServices"


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