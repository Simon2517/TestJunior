<template>
  <div id="app">
    <div class="container-fluid">
      <div class="row bg-dark text-white text-start shadow">
        <div class="col">
          <h3>Company Name</h3>
        </div>
      </div>

      
      <div class="row ">
        <div class="col vh-100"><SideBar /></div>
      </div>
    </div>
  </div>
</template>

<script>
import SideBar from "./components/SideBar.vue";

export default {
  name: "App",
  components: {
    SideBar

  },
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
</style>
