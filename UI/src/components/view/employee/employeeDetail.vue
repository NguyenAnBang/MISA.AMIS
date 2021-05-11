<template>
    <div class="dialog" :class="{'dialog_hide': !isShow}">
     <div class="model"></div>
     <div class="dialog-content">
        <div class="header">
            <div id="title">Thông tin nhân viên</div>
            <button class="btn-check"></button>
            <div class="div">Là khách hàng</div>
            <button class="btn-check"></button>
            <div>Là nhà cung cấp</div>
            <button class="btn-help"></button>
            <button class="btn-X" @click="hideDialog()"></button>
        </div>
        <div class="middle">
            <div class="middle-1">
                <div class="column clm1">
                    <div style="display: flex; align-items: center">
                        <div style="width: 35%; margin-right: 2px;">
                            <div style="position: relative"><b>Mã</b> <span style="color: red;">*</span>
                                <div class="error-message" v-show="!isEmployeeCodeValidMessage">Mã nhân viên chưa hợp lệ</div>
                            </div>
                            <input type="text" class="code-blank-box" 
                            :class="{'blank-box-invalid': !isEmployeeCodeValid}"
                            ref="employeeCode" 
                            v-model="employee.employeeCode"
                            @mouseenter="mouseEnterEmployeeCodeError()"
                            @mouseleave="mouseLeaveEmployeeCodeError()"
                            />
                            
                        </div>
                        <div style="width: 65%; margin-left: 2px;">
                            <div style="position: relative"><b>Tên</b> <span style="color: red;">*</span>
                                <div class="error-message" v-show="!isFullNameValidMessage">Tên nhân viên không được để trống</div>
                            </div>
                            <input type="text" class="fullname-blank-box" 
                            :class="{'blank-box-invalid': !isFullNameValid}"
                            ref="fullName" 
                            v-model="employee.fullName"
                            @mouseenter="mouseEnterFullNameError()"
                            @mouseleave="mouseLeaveFullNameError()"
                            />
                            
                        </div>
                    </div>
                    <div style="position: relative"><b>Đơn vị</b> <span style="color: red;">*</span>
                        <div class="error-message" v-show="!isDepartmentNameValidMessage">Tên đơn vị chưa hợp lệ</div>
                    </div>
                    <!-- combobox -->
                    <div class="dropdown-text-and-icon" :class="{'blank-box-invalid': !isDepartmentNameValid}">
                        <input type="text" class="department-blank-box" 
                        @focus="showDropDownContent()" 
                        @blur="hideDropDownContent()" 
                        ref="departmentName"
                        v-model="employee.departmentName"
                        @mouseenter="mouseEnterDepartmentNameError()"
                        @mouseleave="mouseLeaveDepartmentNameError()"/>
                        <button id="dropdown-icon" @click="showDropDownContent(options)" @blur="hideDropDownContent()"></button>
                    </div>
                    <div id="dropdown">     
                        <div class="dropdown-content" :class="{'dialog_hide': !isShowOption}" >
                            
                            <div class="dropdown-content-a" 
                            :class="{'dropdown-content-a-click': isSelected}"
                            v-for="option in options" 
                            :key="option.id" 
                            @click="chooseOption(option)" 
                            @mouseenter="enterClick()" 
                            @mouseleave="leaveClick()"
                            >{{option.name}}</div>
                        </div>
                    </div>
                    <div><b>Chức danh</b></div>
                    <input type="text" class="medium-blank-box" v-model="employee.jobTitle"/>
                </div>
                <div class="column clm2">
                    <div style="display: flex; align-items: center">
                        <div style="width: 35%; margin-right: 2px;">
                            <div><b>Ngày sinh</b></div>
                            <input type="date" class="date-blank-box" v-model="employee.dateOfBirth" />
                        </div>
                        <div style="width: 65%; margin-left: 2px; padding-left: 6px;">
                            <div><b>Giới tính</b></div>
                            <div class="radio-box" >
                                <button class="btn-radio" :class="{'selected': isMale}" @click="setGenderMale()"></button>
                                <div class="gender" >Nam</div>
                                <button class="btn-radio" :class="{'selected': isFemale}" @click="setGenderFemale()"></button>
                                <div class="gender">Nữ</div>
                                <button class="btn-radio" :class="{'selected': isRest}" @click="setGenderRest()"></button>
                                <div class="gender">Khác</div>
                            </div>
                        </div>
                    </div>
                    <div style="display: flex; align-items: center">
                        <div style="width: 65%; margin-right: 2px;">
                            <div><b>Số CMND</b></div>
                            <input type="text" class="fullname-blank-box" v-model="employee.identityNumber"/>
                        </div>
                        <div style="width: 35%; margin-left: 2px;">
                            <div><b>Ngày cấp</b></div>
                            <input type="date" class="date-blank-box" v-model="employee.identityDate"/>
                        </div>
                    </div>
                    <div><b>Nơi cấp</b></div>
                    <input type="text" class="medium-blank-box" v-model="employee.identityPlace"/>
                </div>
            </div>
            <div class="middle-2">
                <div><b>Địa chỉ</b></div>
                <input type="text" class="large-blank-box" v-model="employee.address"/>
                <div style="display:flex;">
                    <div class="column-1">
                        <div><b>ĐT di động</b>
                            
                        </div>
                        <input type="text" class="small-blank-box" v-model="employee.phone"/>
                        <div><b>Tài khoản ngân hàng</b></div>
                        <input type="text" class="small-blank-box" v-model="employee.bankAccount"/>
                    </div>
                    <div class="column-1">
                        <div style="position: relative"><b>ĐT cố định</b>
                            <div class="error-message" v-show="!isTelephoneValidMessage">Số điện thoại phải đúng định dạng</div>
                        </div>
                        <input type="text" class="small-blank-box" 
                        :class="{'blank-box-invalid': !isTelephoneValid}"
                        ref="telephone" 
                        v-model="employee.telephone"
                        @mouseenter="mouseEnterTelephoneError()"
                        @mouseleave="mouseLeaveTelephoneError()"/>
                        <div><b>Tên ngân hàng</b></div>
                        <input type="text" class="small-blank-box" v-model="employee.bankName"/>
                    </div>
                    <div class="column-1">
                        <div style="position: relative"><b>Email</b>
                            <div class="error-message" v-show="!isEmailValidMessage">Email phải đúng định dạng</div>
                        </div>
                        <input type="text" class="small-blank-box" 
                        :class="{'blank-box-invalid': !isEmailValid}"
                        ref="email" 
                        v-model="employee.email"
                        @mouseenter="mouseEnterEmailError()"
                        @mouseleave="mouseLeaveEmailError()"
                        />
                        <div><b>Chi nhánh</b></div>
                        <input type="text" class="small-blank-box" v-model="employee.bankBranch"/>
                    </div>
                </div>
            </div>
        </div>
        <div class="footer">
            <button class="btn-small cancel" @click="hideDialog()">Hủy</button>
            
            <button class="btn-small post" @click="btnSave()">Cất</button>
            <button class="btn-post-and-put" @click="btnSaveAndAdd()">Cất và thêm</button>

        </div>
     </div>
     <DuplicateEmployeeCode
     :isShow="isDuplicatePopUpShow"
     :employeeCode="employee.employeeCode"
     @hidePopUp="hidePopUp"
     />
     <InvalidDepartment
     :isShow="isInvalidPopUpShow"
     @hidePopUp="hidePopUp"
     />
     <PostOrPut
     :isShow="isPutOrPostPopUpShow"
     @hidePopUp="hidePopUp"
     />
    </div>
</template>
<script>
import axios from 'axios'
import DuplicateEmployeeCode from './pop-up/duplicateEmployeeCode.vue'
import InvalidDepartment from './pop-up/invalidDepartment.vue'
import PostOrPut from './pop-up/postOrPut.vue'
//regular expression của email
const mailRegex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{1,}))$/;
//regular expression của số điện thoại
const telephoneRegex = /^\(\d{3}\) \d{3}-\d{4}$/;
export default ({
    components: {
        DuplicateEmployeeCode,
        InvalidDepartment,
        PostOrPut
    },
    setup() {
        
    },
    created() {
        
    },
    props: {
        //Thuộc tính để ẩn/hiện dialog
        isShow: {
            type: Boolean,
            default: false
        },
        //Thông tin nhân viên được nhập vào dialog
        employee: {
            type: Object,
            default: null
        },
        //Thông tin tất cả nhân viên được gửi về từ database, để check trùng mã nhân viên 
        employees: {
            type: Array,
            default: null
        }
    },
    updated() {
        //auto focus ô input đầu tiên
        //Vì hàm focusInput đã được gọi trước khi dialog hiện ra nên là phải gọi lại nó một lần nữa (sau khi isShow thay đổi từ false sang true)
        while(this.focusFirstInput==true){
        this.focusInput();
        this.focusFirstInput=false;
        
        }
        if(this.isShow == false) this.focusFirstInput = true;

        //Format ngày sau khi dialog được hiện ra, để binding dữ liệu (isShow thay đổi từ false sang true)
        this.employee.dateOfBirth = this.dateFormat(this.employee.dateOfBirth);
        this.employee.identityDate = this.dateFormat(this.employee.identityDate);
        //Format giới tính sau khi dialog được hiện ra, để binding dữ liệu
        this.genderFormat(this.employee.gender);
    },
    // filters:{
    //     dateFormat: function(unformatDate){
    //         var formattedDate = new Date(unformatDate);
    //             var StringMonth = formattedDate.getMonth() + 1;
    //             if (StringMonth < 10) StringMonth = '0' + StringMonth;
    //             var StringDate = formattedDate.getDate();
    //             if (StringDate < 10) StringDate = '0' + StringDate;
    //             var StringYear = formattedDate.getFullYear();

    //             return StringYear + '-' + StringMonth + '-' + StringDate;
    //     }
    // },
    methods: {
        //Ẩn dialog
        hideDialog(){
            //Reset lại các biến validate form
            this.isFullNameValid = true;
            this.isEmployeeCodeValid = true;
            this.isDepartmentNameValid = true;
            this.isEmailValid = true;
            this.isTelephoneValid = true;
            //Gọi đến phương thức ẩn dialog của cha
            this.$emit('hideDialog');
        },
        //Hiện ra dữ liệu cho combobox
        showDropDownContent(){
            this.isShowOption = true;      
            
        },
        //Ẩn đi dữ liệu cho combobox
        hideDropDownContent(){
            if(this.overClick == false) this.isShowOption = false;
        },
        //Gán dữ liệu được chọn từ combobox
        chooseOption(option){
            //Gán giá trị được chọn cho id và tên phòng ban của employee
            this.employee.departmentId = option.id;
            this.employee.departmentName = option.name;
            this.overClick = false;
            this.hideDropDownContent();
        },
        
        //Khi di chuyển chuột vào trong các option
        enterClick(){
            //Gán overClick = true để tránh lỗi click và focusout overlapping (khi click thì focusout sẽ chạy trước mà không chạy click)
            this.overClick = true;
        },
        //Khi di chuyển chuột ra khỏi các option
        leaveClick(){
            this.overClick = false;
        },

        //auto focus vào ô input đầu tiên sau khi hiện dialog
        focusInput(){
            this.$refs.employeeCode.focus();
        },
        //format date để binding vào form
        dateFormat(unformatDate){
            var formattedDate = new Date(unformatDate);
                var StringMonth = formattedDate.getMonth() + 1;
                if (StringMonth < 10) StringMonth = '0' + StringMonth;
                var StringDate = formattedDate.getDate();
                if (StringDate < 10) StringDate = '0' + StringDate;
                var StringYear = formattedDate.getFullYear();

                return StringYear + '-' + StringMonth + '-' + StringDate;
               
        },
        //Format giới tính để binding vào form
        genderFormat(gender){
            if(gender == 0) {
                this.isFemale = true;
                this.isMale = false;
                this.isRest = false;
            }
            else if(gender == 1) {
                this.isMale = true;
                this.isFemale = false;
                this.isRest = false;
            }
            else {
                this.isRest = true;
                this.isMale = false;
                this.isFemale = false;
            }
        },
        //Format giới tính khi thay đổi để lưu vào database
        setGenderMale(){
            this.isMale = true;
            this.isFemale = false;
            this.isRest = false;
            this.employee.gender = 1;
            this.employee.genderName = "Nam";
        },
        setGenderFemale(){
            this.isFemale = true;
            this.isMale = false;
            this.isRest = false;
            this.employee.gender = 0;
            this.employee.genderName = "Nữ";
        },
        setGenderRest(){
            this.isRest = true;
            this.isMale = false;
            this.isFemale = false;
            this.employee.gender = 2;
            this.employee.genderName = "Khác";
        },
        //Validate tên nhân viên không được trống
        fullNameValidation(value){          
            value = value || '';
            if (value.trim() == "") {
                this.isFullNameValid = false;
            }
            else {
                this.isFullNameValid = true;
            }
        },
        //Validate mã nhân viên không được trống
        employeeCodeValidation(value){
            //Validate mã nhân viên không bỏ trống
            value = value || '';
            if (value.trim() == "") {
                this.isEmployeeCodeValid = false;              
            }
        },
        //Validate mã nhân viên không được trùng
        duplicateValidation(){
            this.isEmployeeCodeValid = true;
                //Validate mã nhân viên không bị trùng
                for (var i = 0; i < this.employees.length; i++)
                {
                    if (this.employees[i].employeeCode == this.employee.employeeCode) {
                        //Hiện pop-up thông báo
                        this.isDuplicatePopUpShow = true; 
                        this.isEmployeeCodeValid = false;
                    }
                }
                //
                if(this.employee.employeeId != "") this.isEmployeeCodeValid = true;
        },
        //Ẩn pop-up thông báo trùng mã nhân viên và pop-up thông báo tên đơn vị không hợp lệ
        //Ẩn pop-up phân biệt post hoặc put dữ liệu
        hidePopUp(){
            this.isDuplicatePopUpShow = false;
            this.isInvalidPopUpShow = false;
            this.isPutOrPostPopUpShow = false;
        },
        //Validate tên đơn vị
        departmentNameValidation(value){
            //Validate tên đơn vị không được trống
            value = value || '';
            if (value.trim() == "") {
                this.isDepartmentNameValid = false;
            }
            else {
                this.isDepartmentNameValid = false;

                //Validate tên đơn vị giống với tên đơn vị trong option
                //Kiểm tra xem thông tin nhập vào có đúng với các option tên đơn vị không
                this.options.forEach(option => {
                    if(option.name == value) {
                        this.isDepartmentNameValid = true;
                        this.employee.departmentId = option.id;
                    }
                });
                if(this.isDepartmentNameValid == false) this.isInvalidPopUpShow = true;
            }
        },
        //Validate email đúng định dạng "a@a.a"
        emailValidation(value){
            if (!mailRegex.test(value)) {
                this.isEmailValid = false;
            }
            else {
                this.isEmailValid = true;
            }
        },
        //Validate số điện thoại đúng định dạng (***) ***-****
        telephoneValidation(value){        
            if (!telephoneRegex.test(value)) {
                this.isTelephoneValid = false;
            }
            else {
                this.isTelephoneValid = true;
            }
        },
        
        
        //Phân biệt put với post
        postOrPutValidation(){
            this.employee.employeeId = this.employee.employeeId || "";
            if(this.employee.employeeId == ""){
                this.isPutOrPostPopUpShow = true;
            }
        },
        //Validate form trước khi gửi lên database 
        formValidation(){
            this.fullNameValidation(this.employee.fullName);
            this.employeeCodeValidation(this.employee.employeeCode);
            this.departmentNameValidation(this.employee.departmentName);
            this.emailValidation(this.employee.email);
            this.telephoneValidation(this.employee.telephone);
            if(this.isFullNameValid == true
                && this.isEmployeeCodeValid == true
                && this.isDepartmentNameValid == true
                && this.isEmailValid == true
                && this.isTelephoneValid == true
            ) this.isAppropriate = true;
            else this.isAppropriate = false;
        },
        //Di chuyển chuột vào trong hiện lỗi
        mouseEnterEmployeeCodeError(){
            if( this.isEmployeeCodeValid == false) this.isEmployeeCodeValidMessage = false;
        },
        mouseEnterFullNameError(){
            if( this.isFullNameValid == false) this.isFullNameValidMessage = false;
        },
        mouseEnterDepartmentNameError(){
            if( this.isDepartmentNameValid == false) this.isDepartmentNameValidMessage = false;
        },
        mouseEnterTelephoneError(){
            if( this.isTelephoneValid == false) this.isTelephoneValidMessage = false;
        },
        mouseEnterEmailError(){
            if( this.isEmailValid == false) this.isEmailValidMessage = false;
        },
        //Di chuyển chuột ra ngoài ẩn lỗi
        mouseLeaveEmployeeCodeError(){
            this.isEmployeeCodeValidMessage = true;
        },
        mouseLeaveFullNameError(){
            this.isFullNameValidMessage = true;
        },
        mouseLeaveDepartmentNameError(){
            this.isDepartmentNameValidMessage = true;
        },
        mouseLeaveTelephoneError(){
            this.isTelephoneValidMessage = true;
        },
        mouseLeaveEmailError(){
            this.isEmailValidMessage = true;
        },
        //Lưu dữ liệu lên database (để cập nhật dữ liệu)
        btnSave(){
            this.formValidation();
            this.postOrPutValidation();
            if(this.isAppropriate == true){
                axios.put("https://localhost:44357/api/Employees/" + this.employee.employeeId, this.employee)
                .then(res => {
                    console.log(res);
                    //Đóng Dialog
                    this.$emit('hideDialogAndLoadData');
                })
                .catch(res => {
                    console.log(res);
                })
            }      
        },
        //Lưu dữ liệu lên database (để thêm dữ liệu)
        btnSaveAndAdd(){
            this.duplicateValidation();
            this.formValidation();
            if(this.isAppropriate == true){
                axios.post("https://localhost:44357/api/Employees", this.employee)
                .then(res => {
                    console.log(res);
                    //Đóng Dialog
                    this.$emit('hideDialogAndLoadData');
                })
                .catch(res => { 
                    console.log(res);
                })
            }
            
        }
    },
    data() {
        return {
            //Biến kiểm tra xem dữ liệu combobox có hiện ra hay không
            isShowOption: false,
            //Biến kiểm tra xem option nào được click vào
            isSelected: false,
            //Option được chọn trong dropbox
            selectedOption: {
                id: null,
                name: null
            },
            //Danh sách các lựa chọn
            options: [
                {
                    id: "35e773ea-5d44-2dda-26a8-6d513e380bde",
                    name: "Phòng kế toán"
                },
                {
                    id: "3f8e6896-4c7d-15f5-a018-75d8bd200d7c",
                    name: "Phòng công nghệ"
                },
                {
                    id: "45ac3d26-18f2-18a9-3031-644313fbb055",
                    name: "Phòng Marketing"
                },
                {
                    id: "78aafe4a-67a7-2076-3bf3-eb0223d0a4f7",
                    name: "Phòng quản lý"
                },
                {
                    id: "7c4f14d8-66fb-14ae-198f-6354f958f4c0",
                    name: "Phòng nhân sự"
                },
            ],
            //Biến kiểm tra xem chuột có di chuyển vào các option hay không, để phân biệt click với blur
            overClick: false,
            //Biến kiểm tra xem ô đầu tiên có được focus không
            focusFirstInput: true,
            //Biến để xác nhận giới tính của nhân viên
            isMale: false,
            isFemale: false,
            isRest: false,
            //Biến để xác nhận thông tin trường fullName có hợp lệ không
            isFullNameValid: true, isFullNameValidMessage: true,
            //Biến để xác nhận thông tin trường employeeCode có hợp lệ không
            isEmployeeCodeValid: true, isEmployeeCodeValidMessage: true, 
            //Biến để xác nhận thông tin trường departmentName có hợp lệ không
            isDepartmentNameValid: true, isDepartmentNameValidMessage: true,
            //Biến để xác nhận thông tin trường telephone có hợp lệ không
            isTelephoneValid: true, isTelephoneValidMessage: true,
            //Biến để xác nhận thông tin trường email có hợp lệ không 
            isEmailValid: true, isEmailValidMessage: true,
            //Biển để hiện pop-up thông báo trùng mã nhân viên
            isDuplicatePopUpShow: false,
            //Biến để hiện pop-up tên đơn vị không hợp lệ
            isInvalidPopUpShow: false,
            //Biến để hiện pop-up phân biệt post với put, khi người dùng thêm mới mà lỡ ấn vào nút cập nhật dữ liệu
            isPutOrPostPopUpShow: false,
            //Biến để xác nhận thông tin nhân viên đã hợp lệ để put/post lên database
            isAppropriate: null,
        }
    },
})
</script>

<style scoped>
.dialog_hide {
    display:none;
}
.model {
    position: fixed;
    top: 0px;
    left: 0px;
    right: 0px;
    bottom: 0px;
    background-color: #000000;
    opacity: .4;
}
.dialog-content{
    position: fixed;
    width: 900px;
    height: 625px;
    top: calc(50% - 312px);
    left: calc(50% - 450px);
    background-color: white;
    border-radius: 4px;
    box-sizing: border-box;
}
.header{
    display: flex;
    align-items: center;
    width: 100%;
    height: 60px;
    width: 100%;
    padding: 20px 32px;
    padding-right: 12px;
    position: relative;
    overflow: hidden;
    border-radius: 5px 5px 0 0;
    box-sizing: border-box;
}
#title{
    font-size: 24px;
    color: #111;
    font-weight: 700;
}
.btn-check{
    width: 18px;
    height: 18px;
    cursor: pointer;
    margin-left: 24px;
    margin-right: 8px;
}
.div{
    margin-right: 24px;
}
.btn-help{
    height: 48px;
    width: 48px;
    position: absolute;
    right: 45px;
    top: 0px;
    min-width: 24px;
    min-height: 24px;
    background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat;
    cursor: pointer;
    background-position: -70px -132px;
    border: none;
    outline: none;
    border-radius: 0px 4px 0px 4px;
}
.btn-X{
    height: 48px;
    width: 48px;
    position: absolute;
    right: 0px;
    top: 0px;
    min-width: 24px;
    min-height: 24px;
    background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat;
    cursor: pointer;
    background-position: -134px -132px;
    border: none;
    outline: none;
    border-radius: 0px 4px 0px 4px;
}
.middle{
    padding: 24px;
}
.middle-1{
    display: flex;
}
.column{
    width: 420px;
    height: 220px;
}
.clm1{
    margin-right: 12px;
}
.clm2{
    margin-left: 12px;
}
.code-blank-box{
    width: 100%;
    padding: 6px 10px;
    font-size: 13px;
    height: 32px;
    border: 1px solid #babec5;
    box-sizing: border-box;
    margin-top: 8px;
    margin-bottom: 16px;
    border-radius: 3px;
    outline: none;
}
.code-blank-box:focus{
    border-color: #2ca01c;
}
.date-blank-box{
    width: 100%;
    padding: 6px 10px;
    font-size: 13px;
    height: 32px;
    border: 1px solid #babec5;
    box-sizing: border-box;
    margin-top: 8px;
    margin-bottom: 16px;
    border-radius: 3px;
    outline: none;
}
.date-blank-box:focus{
    border-color: #2ca01c;
}

.radio-box{
    width: 100%;
    font-size: 13px;
    height: 32px;
    box-sizing: border-box;
    margin-top: 8px;
    margin-bottom: 16px;
    border-radius: 3px;
    display: flex;
    align-items: center;
}
.radio-box .gender{
    margin-right: 16px;
}
.btn-radio{
    width: 18px;
    height: 18px;
    border-radius: 50%;
    border: 1px solid #afafaf;
    background: #fff;
    z-index: 1;
    justify-content: center;
    display: block;
    /* margin-left: 16px; */
    margin-right: 8px;
    cursor: pointer;
}
.selected {
    background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat;
    cursor: pointer;
    background-position: -198px -481px;
    /* border: #2ca01c; */
}
.fullname-blank-box{
    width: 100%;
    padding: 6px 10px;
    font-size: 13px;
    height: 32px;
    border: 1px solid #babec5;
    box-sizing: border-box;
    margin-top: 8px;
    margin-bottom: 16px;
    border-radius: 3px;
    outline: none;
}
.fullname-blank-box:focus{
    border-color: #2ca01c;
}
.medium-blank-box{
    width: 100%;
    padding: 6px 10px;
    font-size: 13px;
    height: 32px;
    border: 1px solid #babec5;
    box-sizing: border-box;
    margin-top: 8px;
    margin-bottom: 16px;
    border-radius: 3px;
    display: flex;
    align-items: center;
    outline: none;
    /* background-color: aqua; */
}
.medium-blank-box:focus{
    border-color: #2ca01c;
}
.dropdown-text-and-icon{
    width: 100%;
    padding: 6px 10px;
    font-size: 13px;
    height: 32px;
    border: 1px solid #babec5;
    box-sizing: border-box;
    margin-top: 8px;
    margin-bottom: -2px;
    border-radius: 3px;
    /* box-sizing: border-box; */
    display: flex;
    align-items: center;
    /* background-color: aqua; */
}
.dropdown-text-and-icon:focus-within{
    border-color: #2ca01c;
}
.department-blank-box{
    height: 30px;
    width: calc(100% - 32px);
    /* padding: 6px 0 6px 12px; */
    box-sizing: border-box;
    border: none;
    outline: none;
    border-collapse: collapse;
    border-radius: 4px;
    /* background-color: aqua; */
}
#dropdown-icon{
    width: 30px;
    height: 30px;
    border: none;
    outline: none;
    border-collapse: collapse;
    border-radius: 4px;
    background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat;
    background-position: -545px -352px;
    transform: rotate(0deg);
    transition: transform .15s linear;
    /* background-color: aqua; */
}

.large-blank-box{
    width: 100%;
    padding: 6px 10px;
    font-size: 13px;
    height: 32px;
    border: 1px solid #babec5;
    box-sizing: border-box;
    margin-top: 8px;
    margin-bottom: 16px;
    border-radius: 3px;
    outline: none;
}
.large-blank-box:focus{
    border-color: #2ca01c;
}
.column-1{
    width: 220px;
    height: 100px;
}
.small-blank-box{
    width: 214px;
    padding: 6px 10px;
    font-size: 13px;
    height: 32px;
    border: 1px solid #babec5;
    box-sizing: border-box;
    margin-top: 8px;
    margin-bottom: 16px;
    outline: none;
}
.small-blank-box:focus{
    border-color: #2ca01c;

}
.footer{
    position: absolute;
    bottom: 0px;
    width: 100%;
    height: 68px;
    display: flex;
    align-items: center;
    /* background-color: aqua; */
    padding-left: 24px;
    padding-right: 24px;
    box-sizing: border-box;
    border-top: 1px solid #8d9096;
}
.btn-small{
    height: 36px;
    padding: 8px 20px;
    border: 1px solid #8d9096;
    color: #111;
    background-color: transparent;
    border-radius: 3px;
    cursor: pointer;
}
.btn-post-and-put{
    position: absolute;
    right: 24px;
    height: 36px;
    padding: 8px 20px;
    border: 1px solid transparent;
    color: #fff;
    background-color: #2ca01c;
    border-radius: 3px;
    cursor: pointer;
}
.post{
    position: absolute;
    right: 148px;
}

#dropdown{
        position: relative;
        display: inline-block;
    }
    .dropdown-content{
        height: 120px;
        width: 410px;
        position: absolute;
        top: -6px;
        /* right: 0px; */
        border: 1px solid;
        background-color: #fff;
        border-radius: 3px;
    }
    .dropdown-content-a{
        height: 24px;
        width: 100%;
        display: flex;
        align-items: center;
        padding-left: 12px;
        box-sizing: border-box;
        border: none;
        outline: none;
    }
    .dropdown-content-a:hover{
        background-color: #2ca01c;
        color: #fff;
    }
    .dropdown-content-a-click{
        background-color: #2ca01c;
        color: #fff;
    }

.blank-box-invalid {
    border-color: #F65454;
    outline: none;
}
.error-message{
    position: absolute;
    top: 50px;
    left: calc(50%);
    background-color: black;
    color: #babec5;
    font-size: 11px;
    padding-left: 8px;
    padding-top: 2px;
    padding-bottom: 2px;
    width: 60%;
    border-radius: 4px;
}

</style>