import Repository from "../repository";

const resource="/InfoRequest"
export default {

    getRequests(pageNumber,pageSize,asc,brandId,productName)

    {
        
        return Repository.get(`https://localhost:44355${resource}/requests/${pageNumber}/${pageSize}/${asc}/${brandId}/${productName}`)
                .then(response => (response.data));

    },



    getRequestDetail(id)

    {

        return Repository.get(`${resource}/${id}`)

                .then(response => (response))

    }

}