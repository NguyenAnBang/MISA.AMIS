<template>
  <div id="app">
    <Header/>
    <Navbar/>
    <Content/>
  </div>
</template>

<script>
import Header from './components/layout/header.vue'
import Navbar from './components/layout/navbar.vue'
import Content from './components/layout/content.vue'
export default {
  name: 'App',
  components: {
    Header,
    Navbar,
    Content
  }
}
</script>

<style>
  
</style>
