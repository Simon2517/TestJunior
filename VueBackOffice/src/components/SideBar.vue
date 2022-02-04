<template>
  <div class="container-fluid p-0 ">

    <div class="row ">
      <div class="col-2 bg-light vh-100 ">
        <ul class="nav flex-column bg-light text-start ">
          <li class="nav-item">
            <a class="nav-link active" aria-current="page" href="#">Active</a>
          </li>
          <li class="nav-item">
            <router-link to="/brands" class="nav-link" >Brands</router-link>
          </li>
          <li class="nav-item">
            <router-link to="/products" class="nav-link" >Products</router-link>
          </li>
          <li class="nav-item">
            <a class="nav-link disabled">Disabled</a>
          </li>
        </ul>
      </div>
      <div class="col-10">
        <router-view></router-view>
      </div>
    </div>
  </div>
</template>

<script lang="js">

export default {

}
</script>
