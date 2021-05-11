<template>
    <div id="router">
        <!-- <router-view></router-view> -->
        <div id="router-content">
            <div id="title-bar">
                <div class="title">Nhân viên</div>
                <div class="btn-wrapper">
                    <button id="btn-add" @click="showEmployeeDialog()">Thêm mới nhân viên</button>
                </div>
            </div>
            <div id="table">
                <div id="search">
                    <div id="search-bar-wrapper">
                        <div class="search-bar-and-icon">
                            <input id="search-bar" type="text" placeholder="Tìm theo mã, tên nhân viên" v-model="message" @keyup="searchData()"/>
                            <button id="search-icon" ></button>
                        </div>
                        
                        <div id="btn-refresh-wrapper">
                            <button id="btn-refresh" @click="loadData()"></button>
                            <button id="btn-export" @click="exportData()"></button>
                        </div>
                        
                    </div>                    
                </div>
                <div id="table-content">
                <table id="tblListCustomer">
                        <thead>
                            <tr>
                                <th style="display: flex; justify-content: center;"><button class="btn-checkbox"></button></th>
                                <th>Mã nhân viên</th>
                                <th>Tên nhân viên</th>
                                <th>Giới tính</th>
                                <th>Ngày sinh</th>
                                <th>Số CMND</th>
                                <th>Chức danh</th>
                                <th>Tên đơn vị</th>
                                <th>Số tài khoản</th>
                                <th>Tên ngân hàng</th>
                                <th>Chi nhánh tài khoản ngân hàng</th>
                                <!-- <th style="text-align: center;" :class="{'deleteItems': !isDelete}">Chức năng</th> -->
                                <th style="text-align: center;">Chức năng</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="employee in filteredEmployee" :key='employee.employeeId'>
                            <!-- <tr v-for="employee in filteredEmployee" :key='employee.EmployeeId' @dblclick="rowOnDblClick(employee.EmployeeId)" > -->
                                <td style="display: flex; justify-content: center;"><button class="btn-checkbox"></button></td>
                                <td>{{employee.employeeCode}}</td>
                                <td>{{employee.fullName}}</td>
                                <td>{{employee.genderName}}</td>
                                <td>{{DateFormat(employee.dateOfBirth)}}</td>
                                <td>{{employee.identityNumber}}</td>
                                
                                <td>{{employee.jobTitle}}</td>
                                <td>{{employee.departmentName}}</td>
                                <td>{{employee.bankAccount}}</td>
                                <td>{{employee.bankName}}</td>
                                <td>{{employee.bankBranch}}</td>
                                <!-- <td :class="{'deleteItems': !isDelete}" style="text-align: center;"><button class="button_delete_x" @click="buttonDeleteOnClick(employee.EmployeeId)"></button></td> -->
                                <td><Option 
                                @showDeleteDialog="showDeleteDialog(employee.employeeId)"
                                @showEmployeeDetail="showEmployeeDetail(employee.employeeId)"
                                /></td>
                            </tr>
                            
                        </tbody>
                    </table>
                    
                    <!-- Checkbox -->

                </div>
                <div id="footer">
                    <div id="total-data">Tổng số <b>{{this.employeeNumber}}</b> bản ghi</div>
                    <div id="footer-2">
                        <ComboBox
                        @setPerPage="handlePerPage"
                        />
                    </div>
                    <div id="footer-3">
                        <Pagination
                        :totalPages="totalPage"
                        :perPage="perPage"
                        :currentPage="currentPage"
                        @pagechanged="onPageChange"
                        />
                    </div>
                    
                    
                </div>
            </div>
        </div>
        <EmployeeDetail
            :isShow="isShowDialogEmployee"
            @hideDialog="hideDialog"
            @hideDialogAndLoadData="hideDialogAndLoadData"
            :employee="selectedEmployee"
            :employees="employees"
        />
        <EmployeeDelete
            :isShow="isShowDialogDelete"
            @hideDialog="hideDialog"
            @hideDialogAndLoadData="hideDialogAndLoadData"
            :employee="selectedEmployee"
        />

    </div>
</template>
<script>
import axios from 'axios'
import Option from '../../common/option.vue'
import ComboBox from '../../common/comboBox.vue'
import Pagination from '../../common/pagination.vue'
import EmployeeDetail from './employeeDetail.vue'
import EmployeeDelete from './employeeDelete.vue'
export default ({
    components: {
        Option,
        ComboBox,
        Pagination,
        EmployeeDetail,
        EmployeeDelete
    },
    created() {
        // load dữ liệu cho trang
        axios.get("https://localhost:44357/api/Employees")
        .then(res => {
            console.log(res);
            this.employees = res.data;
            this.filterEmployee = res.data;

            //Mặc định sau khi load xong dữ liệu thì số bản ghi 1 trang là 10 bản ghi
            //CurrentPage là 1
            this.employeeNumber = this.employees.length;
            this.perPage = 10;
            this.totalPage = Math.floor(this.employeeNumber / this.perPage) + 1;
        })
        .catch((res) =>{
            console.log(res);
        });
        
    },
    methods: {
        loadData(){
            //load dữ liệu cho trang
            axios.get("https://localhost:44357/api/Employees")
            .then(res => {
                console.log(res);
                this.employees = res.data;
                this.filterEmployee = res.data;
                this.employeeNumber = this.employees.length;
                // alert("load thanh cong!");
            })
            .catch((res) =>{
                console.log(res);
            });

            this.message = "";
        },
        //hiện dialog thêm nhân viên
        showEmployeeDialog(){
            this.isShowDialogEmployee = true;
            axios.get("https://localhost:44357/api/Employees/MaxCode")
            .then(res => {
                this.selectedEmployee  = {};
                this.selectedEmployee.employeeCode = res.data;
            })
            .catch(res =>{
                console.log(res);
            })
            // this.selectedEmployee  = {};
        },
        //Hiện dialog sửa nhân viên
        showEmployeeDetail(employeeId){
            this.isShowDialogEmployee = true;
            // alert(employeeId);
            //Get dữ liệu từ APi về phù hợp với bản ghi
            axios.get("https://localhost:44357/api/Employees/" + employeeId)
            .then(res => {
                this.selectedEmployee = res.data;
            })
            .catch(res => {
                console.log(res);
            })
        },
        //ẩn dialog thêm nhân viên và dialog xóa nhân viên
        hideDialog(){
            this.isShowDialogEmployee = false;
            this.isShowDialogDelete = false;
        },
        //ẩn dialog thêm nhân viên và dialog xóa nhân viên, và load lại data
        hideDialogAndLoadData(){
            this.isShowDialogEmployee = false;
            this.isShowDialogDelete = false;
            this.loadData();
        },
        //hiện dialog xác nhận xóa nhân viên
        showDeleteDialog(employeeId){
            this.isShowDialogDelete = true;
            //Get dữ liệu từ APi về phù hợp với bản ghi
            axios.get("https://localhost:44357/api/Employees/" + employeeId)
            .then(res => {
                this.selectedEmployee = res.data;
            })
            .catch(res => {
                console.log(res);
            })
        },
        //Hiện kết quả tìm kiếm dựa trên mã nhân viên hoặc tên nhân viên
        searchData(){
            // console.log(search_input);
            
            //     axios.get("https://localhost:44357/api/Employees/Search/" + search_input)
            //     .then(res => {
            //         // console.log(res);
            //         this.filterEmployee = res.data;
            //     })
            //     .catch((res) =>{
            //         console.log(res);
            //     });      
            this.filterEmployee = this.employees.filter(employee => {
                return (
                    employee.employeeCode.toLowerCase().includes(this.message.toLowerCase())
                    || employee.fullName.toLowerCase().includes(this.message.toLowerCase())
                )
            })
        },
        //format date of birth
        DateFormat(DateOfBirth) {
            var newDate = new Date(DateOfBirth);
            var StringDate = newDate.getDate();
            if (StringDate < 10) StringDate = '0' + StringDate;
            var StringMonth = newDate.getMonth() + 1;
            if (StringMonth < 10) StringMonth = '0' + StringMonth;
            var StringYear = newDate.getFullYear();
            return StringDate + '/' + StringMonth + '/' + StringYear;
        },
        //Xuất dữ liệu ra file excel
        exportData(){
            window.open("https://localhost:44357/api/Employees/Export", "blank");
        },

        //Thay đổi số trang, thay đổi currentPage
        onPageChange(page) {
            console.log(page);
            this.currentPage = page;
        },
        //Xử lý khi set số bản ghi 1 trang //info là số lượng bản ghi 1 trang truyền lên từ ComboBox (component con)
        handlePerPage(info){
            this.currentPage = 1;
            this.perPage = info;
            this.totalPage = Math.floor(this.employeeNumber / this.perPage) + 1;
        }
    },
    computed:{
        //limit hiển thị số lượng bản ghi mỗi trang tùy thuộc vào currentpage và perpage
        filteredEmployee: function(){
            return this.filterEmployee.slice(this.perPage * this.currentPage - this.perPage,  this.perPage * this.currentPage);
        },

    },
    data() {
        return {
            //dữ liệu đầu vào (axios.get) -> để gán dữ liệu
            employees: [],
            filterEmployee: [],
            //Tổng số bản ghi
            employeeNumber: null,
            //Kiểm tra đóng/mở dialog thêm nhân viên
            isShowDialogEmployee: false,
            //Kiểm tra đóng/mở dialog xóa nhân viên
            isShowDialogDelete: false,
            //Biến nhận giá trị tìm kiếm
            message: null,
            //Dữ liệu của nhân viên được chọn
            selectedEmployee: {},    
            
            //Dấu trang
            currentPage: 1,

            //Số lượng bản ghi được hiển thị trong 1 trang
            perPage: 0,
            //Số lượng trang
            totalPage: 0,
        }
    },
})

</script>

<style scoped>
    #router-content{
        position: absolute;
        top: 48px;
        left: 178px;
        box-sizing: border-box;
        display: inline-block;
        background:rgb(244, 245, 246);
        width: calc(100% - 178px);
        height: calc(100% - 48px);
        /* background-color: aqua; */
        padding: 24px;
    }
    #title-bar{
        display: flex;
        align-items: center;
        width: 100%;
        height: 40px;
        /* background-color: aqua; */
    }
    .title{
        /* font-weight: bold; */
        font-size: 24px;
        font-weight: 700;
        color: #111;
    }
    .btn-wrapper{
        position: absolute;
        right: 24px;
        height: 40px;
    }
    #btn-add{
        height: 100%;
        width: 200px;
        border-radius: 4px;
        border: 1px solid transparent;
        color: #fff;
        background-color: #2ca01c;
    }
    #table{
        background: #fff;
        color: #111;
        height: calc(100% - 36px);
        width: 100%;
        /* overflow: scroll; */
        /* padding: 24px; */
    }
    #table-content{
        max-height: calc(100% - 120px);
        width: 100%;
        /* background-color: green; */
        overflow: auto;
    }
    #tblListCustomer{
        width: 100%;
    }
    /* #tblListCustomer{
        height: 400px;
        background-color: green;
        overflow: scroll;
    } */
    /* .table tbody{
        overflow: scroll;
        height: 10000px;
    } */
    #search{
        height: 80px;
        width: 100%;
    }
    #search-bar-wrapper{
        height: 80px;
        /* background-color: aqua; */
        display: flex;
        align-items: center;
        position: absolute;
        right: 24px;       
    }
    .search-bar-and-icon{
        border-radius: 2px;
        box-sizing: border-box;
        width: 232px;
        display: flex;
        align-items: center;
        border: 1px solid #babec5;
        border-collapse: collapse;
    }
    .search-bar-and-icon:focus-within {
        border: 1px solid #2ca01c; 
    }
    #search-bar{
        width: 199px;
        height: 31px;
        font-size: 13px;
        /* border-radius: 2px;
        border: 1px solid #babec5; */
        outline: none;
        border: none;
        /* border: 1px solid #babec5; */
        /* border-right: none; */
        box-sizing: border-box;
        padding: 6px 10px;
        font-style: italic;
        border-collapse: collapse;
        
    }
    /* #search-bar:focus{
        border: 1px solid #2ca01c ;
        border-right: none;
    }
    #search-bar:focus + #search-icon{
        border: 1px solid #2ca01c ;
        border-left: none; */
    /* } */
    #search-icon{
        width: 31px;
        height: 31px;
        background:url(../../../assets/img/Sprites.64af8f61.svg) no-repeat;
        background-position: -984px -353px;
        border: none;
        outline: none;
        /* border: 1px solid #babec5; */
        /* border-left: none; */
        /* border: 1px solid #babec5; */
        border-collapse: collapse;
    }
    

    #btn-refresh-wrapper{
        height: 80px;
        width: 80px;
        padding-left: 20px;
        display: flex;
        align-items: center;
        justify-content: center;
    }
    th{
        font-size: 12px;
        text-transform: uppercase!important;
        align-items: center;
        text-align: left;
        /* cursor: pointer; */
        padding-left: 8px;
        padding-right: 8px;
        border-right: 1px solid #c7c7c7;
        border-bottom: 1px solid #c7c7c7;
        min-height: 34px;
        height: 34px;
    }
    th:last-child, td:last-child{
        border-right: none;
    }
    td{
        font-size: 12px;
        /* text-transform: uppercase!important; */
        align-items: center;
        /* cursor: pointer; */
        padding-left: 8px;
        padding-right: 8px;
        border-right: 1px solid #c7c7c7;
        border-bottom: 1px solid #c7c7c7;
        min-height: 34px;
        height: 100%;
    }
    #btn-refresh{
        height: 32px;
        width: 32px;
        background:url(../../../assets/img/Sprites.64af8f61.svg) no-repeat;
        background-position: -423px -197px;
        border: none;
        outline: none;
        cursor: pointer;
    }
    #btn-export{
        width: 32px;
        height: 32px;
        background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat;
        background-position: -700px -197px;
        cursor: pointer;
        border: none;
        outline: none;
    }
    .btn-checkbox{
        width: 18px;
        height: 18px;
        border-radius: 2px;
        display: flex;
        align-items: center;
        justify-content: center;
        box-sizing: border-box;
        border: 1px solid #afafaf;
        background: #fff;
        transition: all .2s ease;
        transform: rotate(-90deg);
        cursor: pointer;
    }

    #footer{
        margin-top: 24px;
        position: absolute;
        bottom:24px;
        /* background-color: aqua; */
        width: calc(100% - 48px);
        box-sizing: border-box;
        display: flex;
        align-items: center;
    }
    #footer-2{
        position: absolute;
        right: 280px;
        /* background-color: aqua; */
    }
    #footer-3{
        /* background-color: green; */
        position: absolute;
        right: 24px;
        width: 250px;
    }

</style>