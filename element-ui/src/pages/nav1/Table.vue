<template>
  <section>
    <!-- 工具条 -->
    <el-col :span="24" class="toolbar" style="padding-bottom: 0;">
      <el-form :inline="true" :model="filters">
        <el-form-item>
          <el-input v-model="filter.name" placeholder="姓名"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="getUsers">查询</el-button>
        </el-form-item>
        <el-from-item>
          <el-button type="primary" @click="handleAdd">新增</el-button>
        </el-from-item>
      </el-form>
    </el-col>

    <!-- 列表 -->
    <el-table :data="users" heighlight-current-row v-loading='listLoading' @select-change="selsChange"  style="width:100%">
      <el-table-column type="selection" width="55"></el-table-column>
      <el-table-column type="index" width="60"></el-table-column>
      <el-table-column prop="name" label="姓名" width="120" sortable></el-table-column>
      <el-table-column prop="sex" label="性别" width="100" :formatter="formatSex" sortable></el-table-column>
      <el-table-column prop="birth" label="生日" width="120" sortable></el-table-column>
      <el-table-column prop="addr" label="地址" width="180" sortable></el-table-column>
      <el-table-column label="操作" width="150">
        <template  scope="scope">
          <el-button size="small" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
          <el-button type="danger" size="small" @click="handleDel(scoep.$index, scope.row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <!-- 工具条 -->
    <el-col :span="24" class="toolbar">
      <el-button type="danger" @click="batchRemove" :disabled="this.sels.length===0">
        批量删除
      </el-button>
      <el-pagination layout="prev, pager, next" @current-change="handleCurrentChange" :page-size="20" :total="total" style="float:right;"></el-pagination>
    </el-col>

    <!-- 编辑界面 -->
    <el-dialog title="编辑" v-model="editFormVisible" :close-on-click-modal="false">
      <el-form :model="editForm" label-width="80px" :rules="editFormRules" ref="editForm">
        <el-form-item label="姓名" prop="name">
          <el-input v-model="editForm.name" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="性别">
          <el-radio-group v-model="editForm.sex">
            <el-radio class="radio" :label="1">男</el-radio>
            <el-radio class="radio" :label="0">女</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="年龄">
          <el-input-number v-model="editForm.age" :min="0" :max="200"></el-input-number>
        </el-form-item>
        <el-form-item label="生日">
          <el-date-picker type="date" placeholder="选择日期" v-model="editForm.birth"></el-date-picker>
        </el-form-item>
        <el-form-item label="地址">
          <el-input type="textarea" v-model="editForm.addr"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click.native="editFormVisible = false">取消</el-button>
        <el-button type="primary" @click.native="eidtSubmit" :loading="editLoading">提交</el-button>
      </div>
    </el-dialog>
    <!-- 新增界面 -->
    <el-dialog title="新增" v-model="addFormVisible" :close-on-click-modal="false" >
      <el-form :model="addForm" label-width="80px" :rules="addFomeRules" ref="addForm">
        <el-form-item label="姓名" prop="name">
          <el-input v-model="addForm.name" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="性别">
          <el-radio-group v-model="addForm.sex">
            <el-radio class="radio" :label="1">男</el-radio>
            <el-radio class="radio" :label="0">女</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="年龄">
          <el-input-number v-model="addForm.age" :min="0" :max="200"></el-input-number>
        </el-form-item>
        <el-form-item label="生日">
          <el-date-picker type="date" placeholder="选择日期" v-model="addForm.birth"></el-date-picker>
        </el-form-item>
        <el-form-item label="地址">
          <el-input type="textarea" v-model="addForm.addr"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click.native="addFormVisible = false">取消</el-button>
        <el-button type="primary" @click.native="addSubmit" :loading="addLoading">提交</el-button>
      </div>
    </el-dialog>
  </section> 

</template> 

<script>
  import util from '../../utils/util'
  import  api from '../../api'

  export default{
    data() {
      return {
        filters: {
          name: ''
        },
      total: 0,
      users: [],
      page: 1,
      listLoading: false,
      sels: [],// 选中的列表

      editFormVisible: false,
      editLoading: false,
      editFormRules: {
        name: [
          { required: true, message: '请输入姓名', trigger: 'blur' }
        ]
      },
      // 编辑界面数据
      editForm:{
        name: '',
        sex: -1,
        age: 0,
        birth: '',
        addr: ''
      },

      addFormVisible: false, //新增界面是否显示
      addLoading: false,
      addFomeRules: {
        name: [
          { required: true, message: '请输入姓名', trigger: 'blur'}
        ]
      },

      addForm: {
        name: '',
        sex: -1,
        age: 0,
        birth: '',
        addr: ''
      }

      }
    },
    
    methods: {
      //性别显示转换
      formatSex: function (row, column) {
        return row.sex == 1 ? '男' :row.sex == 0 ? '女' : '未知';
      },
      handleCurrentChange (val) {
        this.page = val,
        this.getUsers()
      },
      //获取用户列表
      getUsers () {
        let para = {
          page: this.page,
          name: this.filters.name
        }
        this.listLoading = true
        getUsersListPage(para).then((res)=>{
          this.total = res.data.total,
          this.users = res.data.customers,
          this.listLoading = false
        })
      },
      
      handleDel: function (index, row) {
        this.$confirm('确认删除该记录吗？','提示',{
          type: 'warning'
        }).then(()=>{
          this.listLoading = true
          let para = {id: row.id}
          removeUser(para).then((res) => {
            this.listLoading = false,
            this.$message({
              message: '删除成功',
              type: 'success'
            })
            this.getUsers()
          })
        }).catch(() => {})
      },
      // 显示编辑界面
      handleEdit: function (index,row) {
       this.editFormVisible = true,
       this.editForm  = Object.assign({}, row) 
      },
      // 显示新增界面
      handleAdd: function () {
        this.addFormVisible = true,
        this.addForm = {
          name: '',
          sex: -1,
          age: 0,
          birth: '',
          addr: ''
        }
      },

      editSubmit: function () {
        this.$ref.editForm.validate((valid) => {
          if (valid) {
            this.$confirm('确认提交吗？','提示',{}).then(() => {
              this.editLoading = true
              let para = Object.assign({}, this.editForm)
              para.birth = ( !para.birth || para.birth == '')? '': util.formatDate.format(new Date(para.birth),'yyyy-MM-dd')
              editUser(para).then((res) => {
                this.editLoading = false,
                this.$message({
                  message: '提交成功',
                  type: 'success'
                })
              })
              this.$refs['editForm'].resetFields()
              this.editFormVisible = false
              this.getUsers()
            })
          }
        })
      },

      addSubmit: function () {
        this.$refs.addForm.validate((valid) => {
          if (valid) {
            this.$confirm('确认提交吗？', '提示', {}).then(() => {
              this.addLoading = true
              let para = Object.assign({}, this.addForm)
              para.birth  = (para.birth || para.birth == '')? '' : util.formatDate.format(new Date(para.birth),'yyyy-MM-dd')
              addUser(para).then((res) => {
                this.addLoading = false,
                this.$message({
                   message: '提交成功',
                   type: 'sucess'
                })
                this.$refs['addForm'].resetFields()
                this.addFormVisible = false
                this.getUsers()
              })
            })
          }
        })
      },

      selsChange: function (sels) {
        this.sels = sels 
      },

      batchRemove: function () {
        var ids = this.sels.map(itme => item.id).toString()
        this.$confirm('确认删除','提示',{
          type: 'warning'
        }).then(() => {
          this.listLoading = true
          let para = { ids: ids}
          batchRemove(para).then((res) => {
            this.listLoading = false
            this.$message({
              message:'删除成功',
              type: 'success'
            })
            this.getUsers()
          })
        }).catch(() => {})
      }
    },
    mounted () {
      this.getUsers()
    }
  }
</script>