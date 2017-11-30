<template>
  <el-form ref="loginForm" :model="loginForm" :rules="rules2" label-position="left" label-width="50px"  class="loginForm login-container">
    <h3 class="title">登入系统</h3>
    <el-form-item label="账户" prop="account">
      <el-input v-model="loginForm.account" auto-complete="off" placeholder="帐号">
      </el-input>
    </el-form-item>
    <el-form-item label="密码" prop="psw">
      <el-input type="password" v-model="loginForm.psw" auto-complete="off" placeholder="密码">
      </el-input>
    </el-form-item>
    <el-checkbox v-model="checked" checked class="remember">
      记住密码
    </el-checkbox>
    <el-form-item style="width:100%;">
      <el-button type="primary" style="width:100%" @click.native.prevent="loginSubmit('loginForm')" :loading = "logining" >登录</el-button>
    </el-form-item>
  </el-form>
</template>

<script>
  import { RequestLogin } from '../api/api'
  export default {
    data () {
      return {
        logining: false,
        loginForm: {
          account: 'admin',
          psw: '123456'
        },
        rules2: {
          account: [{
            required: true,
            message: '请输入帐号',
            trigger: 'blur'
          }],
          psw: [{
            required: true,
            message: '请输入密码',
            trigger: 'blur'
          }]
        },
        checked: true
      }
    },
    methods: {
      loginSubmit (e) {
        this.$refs.loginForm.validate((valid) => {
          if (valid) {
            this.logining = true
            let loginParams = {
              username: this.loginForm.account,
              password: this.loginForm.psw
            }
            RequestLogin(loginParams).then(data => {
              this.logining = false
              let { msg, code, user } = data
              if (code !== 200) {
                this.$message({
                  message: msg,
                  type: 'error'
                })
              } else {
                sessionStorage.setItem('user', JSON.stringify(user))
                this.$router.push({
                  path: '/table'
                })
              }
            })
          } else {
            console.log('error submit！')
            return false
          }
        })
      }
    }
  }
</script>

<style lang="scss" scoped>
.login-container {
    /*box-shadow: 0 0px 8px 0 rgba(0, 0, 0, 0.06), 0 1px 0px 0 rgba(0, 0, 0, 0.02);*/
    -webkit-border-radius: 5px;
    border-radius: 5px;
    -moz-border-radius: 5px;
    background-clip: padding-box;
    margin: 180px auto;
    width: 350px;
    padding: 35px 35px 15px 35px;
    background: #fff;
    border: 1px solid #eaeaea;
    box-shadow: 0 0 25px #cac6c6;
    .title {
      margin: 0px auto 40px auto;
      text-align: center;
      color: #505458;
    }
    .remember {
      margin: 0px 0px 35px 0px;
    }
  }
</style>
