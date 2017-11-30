<template>
  <el-row class="container">
    <el-col :span="24" class="header">
      <el-col :span="10" class="logo" :class="collapsed?'logo-collapse-width':'logo-width'">
        {{ collapsed?'':sysName}}
      </el-col>
      <el-col :span="10">
        <div class="tools" @click.prevent="collapse">
          <i class="fa fa-align-justify"></i>
        </div>
      </el-col>
      <el-col :span="4" class="userinfo">
        <el-dropdown trigger="hover">
          <span class="el-dropdown-link userinfo-inner">
            <img :src="this.sysUserAvator" alt="头像">
            {{sysUserName}}
          </span>
          <el-dropdown-menu slot='dropdown'>
            <el-dropdown-item>我的消息</el-dropdown-item>
            <el-dropdown-item>设置</el-dropdown-item>
            <el-dropdown-item divided @click.native="logout">退出登入</el-dropdown-item>
          </el-dropdown-menu>
        </el-dropdown>
      </el-col>
    </el-col>
    <el-col :span="24" class="main">
      <aside :class="collapsed?'menu-collapsed':'menu-expanded'">
        <el-menu :default-active="$route.path" class="el-menu-vertical-demo" @open="handleopen" @close="handleclose" @select="handleselect" unique-opened router :collapse="collapsed" background-color='#f0f0f0'>
          <el-submenu v-for="(item, index) in $router.options.routes" :key="index" :index="index + ''"  v-if="!item.hidden">
             <template slot="title">
              <i :class="item.iconCls"></i>
              <span slot="title">{{item.name}}</span>
            </template>
            <el-menu-item-group v-if="item.children.length>0">
              <el-menu-item v-for="child in item.children" :index="child.path" :key="child.path">{{child.name}}</el-menu-item>
            </el-menu-item-group>
          </el-submenu>  
        </el-menu>
      </aside>

      <section class="content-container">
        <div class="grid-content bg-purple-light">
          <el-col :span="24" class="breadcrumb-container">
            <strong class="title">{{$route.name}}</strong>
            <el-breadcrumb separator="/" class="breadcrumb-inner">
              <el-breadcrumb-item v-for="item in $route.matched" :key="item.path">
                {{item.name}}
              </el-breadcrumb-item>
            </el-breadcrumb>
          </el-col>
          <el-col :span="24" class="content-wrapper">
            <transition name="fade" mode="out-in">
              <router-view></router-view>
            </transition>
          </el-col>
        </div>
      </section>
    </el-col>
  </el-row>
</template>


<script>
  export default {
    data () {
      return {
        sysName: 'Kissshot13',
        collapsed: false,
        sysUserAvator: '',
        sysUserName: 'kissshot'
      }
    },
    methods: {
      handleopen (key, keyPath) {
        console.log(key, keyPath)
      },
      handleclose (key, keyPath) {
        console.log(key, keyPath)
      },
      handleselect: function (a, b) {},
      collapse: function () {
        this.collapsed = !this.collapsed
      },
      logout: function () {
        var _this = this
        this.$confirm('确认退出吗?', '提示', {
        }).then(() => {
          sessionStorage.removeItem('user')
          _this.$router.push('/login')
        })
      }
    },
    mounted () {
      var usr = sessionStorage.getItem('user')
      if (usr) {
        usr = JSON.parse(usr)
        this.sysUserName = usr.name || ''
        this.sysUserAvator = usr.avator || ''
      }
    }
  }
</script>

<style lang="scss">
$color-primary: #20a0ff;
	.container {
		position: absolute;
		top: 0px;
		bottom: 0px;
		width: 100%;
		.header {
			height: 60px;
			line-height: 60px;
			background: $color-primary;
			color:#fff;
			.userinfo {
				text-align: right;
				padding-right: 35px;
				float: right;
				.userinfo-inner {
					cursor: pointer;
					color:#fff;
					img {
						width: 40px;
						height: 40px;
						border-radius: 20px;
						margin: 10px 0px 10px 10px;
						float: right;
					}
				}
			}
			.logo {
				//width:230px;
				height:60px;
				font-size: 22px;
				padding-left:20px;
				padding-right:20px;
				border-color: rgba(238,241,146,0.3);
				border-right-width: 1px;
				border-right-style: solid;
				img {
					width: 40px;
					float: left;
					margin: 10px 10px 10px 18px;
				}
				.txt {
					color:#fff;
				}
			}
			.logo-width{
				width:230px;
			}
			.logo-collapse-width{
				width:65px
			}
			.tools{
				padding: 0px 23px;
				width:14px;
				height: 60px;
				line-height: 60px;
				cursor: pointer;
			}
		}
		.main {
			display: flex;
			// background: #324057;
			position: absolute;
			top: 60px;
			bottom: 0px;
			overflow: hidden;
      
      .menu-expanded {
        width: 230px;
      }
      .el-menu {
          height: 100%;
        }
			.content-container {
				flex:1;
				overflow-y: auto;
				padding: 20px;
				.breadcrumb-container {
					//margin-bottom: 15px;
					.title {
						width: 200px;
						float: left;
						color: #475669;
					}
					.breadcrumb-inner {
						float: right;
					}
				}
				.content-wrapper {
					background-color: #fff;
					box-sizing: border-box;
				}
			}
		}
	}
</style>
