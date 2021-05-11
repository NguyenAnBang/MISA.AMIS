import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'


// import CustomerList from './components/views/customer/customerList.vue'
// import EmployeeList from './components/views/employee/employeeList.vue'
// import Report from './components/views/report/report.vue'
// import Setting from './components/views/setting/setting.vue'
import EmployeeList from './components/view/employee/employeeList.vue'
Vue.config.productionTip = false
//1. Định nghĩa các path
const routes = [
//   {path: '/customer', component: CustomerList},
  {path: '/', component: EmployeeList},
//   {path: '/report', component: Report},
//   {path: '/setting', component: Setting},
]
//2. Khởi tạo router
const router = new VueRouter({
  routes // short for routes:routes
})
//3.khai báo sử dụng router
Vue.use(VueRouter)
new Vue({
  router,
  render: h => h(App),
}).$mount('#app')
