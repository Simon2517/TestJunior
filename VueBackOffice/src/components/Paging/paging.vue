<template>
    <ul class="pagination d-flex justify-content-center">
      <li class="page-item" :class="currentPage === 1 ? 'disabled' : ''">
        <a class="page-link" @click="previous()">Previous</a>
      </li>
      <li class="page-item" :class="currentPage === 1 ? 'active' : ''">
        <a class="page-link" @click="selectedIndex(1)"> 1 </a>
      </li>
      <li class="page-item">
        <a v-if="currentPage >= 5" class="page-link pe-none"> ... </a>
      </li>
      <li
        class="page-item"
        v-for="item in pages"
        :key="item"
        :class="currentPage === item ? 'active' : ''"
      >
        <a class="page-link" @click="selectedIndex(item)">{{ item }}</a>
      </li>
      <li class="page-item">
        <a
          v-if="currentPage < currentPage + 2 && currentPage < maxPage - 2"
          class="page-link pe-none"
        >
          ...
        </a>
      </li>
      <li
        class="page-item"
        :class="currentPage === maxPage ? 'active' : ''"
        v-if="maxPage !== 1"
      >
        <a class="page-link" @click="selectedIndex(maxPage)">
          {{ maxPage }}
        </a>
      </li>
      <li
        class="page-item"
        :class="this.currentPage === this.maxPage ? 'disabled' : ''"
      >
        <a class="page-link" @click="next()">Next</a>
      </li>
    </ul>
</template>

<script>
export default {
  props:{
    pageNumber:{type:Number},
    pageSize:{type:Number},
    totalPages:{type:Number}
  },
  data() {
    return {  
    }
  },
  computed: {
    pages() {
      let pages = [];
      for (let i = 2; i < this.maxPage; i++) {
        if (i <= this.currentPage + 2 && i >= this.currentPage - 2) pages.push(i);
      }
      return pages;
    },
    currentPage(){
      return this.pageNumber
    },
    pageElements(){
      return this.pageSize
    },
    maxPage(){
      return this.totalPages
    }
  },
  methods: {
    async next() {
      this.$emit('next');
    },
    async previous() {
      this.$emit('previous'); 
    },
    async selectedIndex(index) {
      this.$emit('selectedIndex',{
        index
      }); 
    }
  }
}
</script>

<style scoped>
a {
  cursor: pointer;
}
</style>
