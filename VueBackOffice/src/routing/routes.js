import Vue from 'vue'
import Router from 'vue-router'
Vue.use(Router)

export default new Router({
    routes: [
        {
            path: '/todolist',
            name:'ToDoList',
            component: TodoList
        },
        {
            path: '/brands',
            name: 'brandList',
            component:CreateTodo
        }],


})