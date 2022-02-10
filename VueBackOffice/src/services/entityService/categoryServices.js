import Repository from '../repository'

const resource="/Category"
export default {

    getCategories()

    {

        return Repository.get(`https://localhost:44355${resource}`)

                .then(response => (response.data));

    }

}