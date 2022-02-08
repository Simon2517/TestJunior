import brandServices from "../services/brandServices";
import productServices from "../services/productServices";
import inforequestServices from "../services/inforequestServices"


const repositories = {
  products: productServices,
  brands: brandServices,
  inforequests:inforequestServices
  // other repositories ...
};

export const RepositoryFactory = {
  get: name => repositories[name]
};