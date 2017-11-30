<template>
  <div id="app">
    <transition name="fade" mode="out-in">
    <router-view></router-view>
    </transition>
  </div>
</template>

<script>
  export default {
    name: 'app',
    comments: {

    }
  }
</script>

<style lang="scss">
body {
  margin: 0;
  padding: 0;
  font-family: Arial, Helvetica, sans-serif;
  font-size: 14px;
  -webkit-font-smoothing: antialiased;
}
</style>
