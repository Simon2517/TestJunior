<template>
  <div id="app" >
    <div class="container-fluid">
      <div class="row bg-dark text-white text-start shadow" >
        <div class="col">
          <h3>Company Name</h3>
        </div>
      </div>

      <div class="row">
        <div class="col">
          <div class="container-fluid p-0 ">
            <div class="row">
              <div class="col-2 bg-light">
                <ul class="nav flex-column bg-light text-start" v-bind:class="'view'">
                  <li class="nav-item">
                    <router-link to="/brands" class="nav-link"
                      >Brands</router-link
                    >
                  </li>
                  <li class="nav-item">
                    <router-link to="/products" class="nav-link"
                      >Products</router-link
                    >
                  </li>
                  <li class="nav-item">
                    <router-link to="/inforequest" class="nav-link"
                      >Leads</router-link
                    >
                  </li>
                </ul>
              </div>
              <div class="col-10">
                <router-view></router-view>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "App",
  components: {},
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  height: 100%;
}
.view {
  height: 100vh;
}
</style>
