<template>
    
 <select  name="" id="">
     <option v-for="(item,index) in info" :key="index" value=""> {{item}}</option>
 </select>

</template>

<script>
import brandServices from "../services/brandServices";
export default {
    data(){
        return{
            info:null
        }
    },
    async created() {
    this.info=await brandServices.getBrandsName();
  }
}
</script>

<style>

</style>