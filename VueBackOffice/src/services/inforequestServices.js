import axios from "axios";

const resource="/InfoRequest"
export default {

    getRequests(pageNumber,pageSize,asc)

    {

        return axios.get(`https://localhost:44355${resource}/requests/${pageNumber}/${pageSize}/${asc}`)

                .then(response => (response.data));

    },



    getRequestDetail(id)

    {

        return axios.get(`${resource}/${id}`)

                .then(response => (response))

    }

}