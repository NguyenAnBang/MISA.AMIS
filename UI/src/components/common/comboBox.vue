<template>
    <div style="display: flex; justify-content: center; align-items:center;">
        <div class="dropdown-text-and-icon">
            <input id="dropdown-text" type="text" v-model="selectedOption.name" @focus="showDropDownContent(options)" @blur="hideDropDownContent()"/>
            <button id="dropdown-icon" @click="showDropDownContent(options)" @blur="hideDropDownContent()"></button>
        </div>
        <div id="dropdown">     
            <div class="dropdown-content" :class="{'hide': !isShowOption}" >
                <div class="dropdown-content-a" 
                            v-for="option in options" 
                            :key="option.value" 
                            @click="chooseOption(option)" 
                            @mouseenter="enterClick()" 
                            @mouseleave="leaveClick()"
                            >{{option.name}}</div>
            </div>
        </div>
    </div>
</template>
<script>


export default ({
    created() {
        
    },
    
    methods: {
        //Hiện ra dữ liệu cho combobox
        showDropDownContent(){
            this.isShowOption = true;      
            
        },
        //Ẩn đi dữ liệu cho combobox
        hideDropDownContent(){
            if(this.overClick == false) this.isShowOption = false;
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
        //Chọn option để gán vào input
        chooseOption(option){
            this.selectedOption = option;
            this.$emit('setPerPage', this.selectedOption.value)
            this.overClick = false;
            this.hideDropDownContent();
        }
    },
    data() {
        return {
            //Biến để ẩn/hiện option của combobox
            isShowOption: false,
            //Danh sách option
            options: [
                {
                    value: 10,
                    name: "10 bản ghi trên trang"
                },
                {
                    value: 20,
                    name: "20 bản ghi trên trang"
                },
                {
                    value: 30,
                    name: "30 bản ghi trên trang"
                },
                {
                    value: 50,
                    name: "50 bản ghi trên trang"
                },
                {
                    value: 100,
                    name: "100 bản ghi trên trang"
                },
            ],
            //Option được chọn
            selectedOption: {
                value: 10,
                name: "10 bản ghi trên trang"
            },
            //Biến kiểm tra xem chuột có di chuyển vào các option hay không, để phân biệt click với blur
            overClick: false,
        }
    },
})
</script>

<style scoped>
    .hide{
        display: none;
    }
    #dropdown{
        position: relative;
        display: inline-block;
    }
    .dropdown-content{
        height: 120px;
        width: 200px;
        position: absolute;
        bottom: 20px;
        right: 0px;
        border: 1px solid;
        background-color: #f1f1f1;

    }
    .dropdown-content-a{
        height: 24px;
        width: 100%;
        display: flex;
        align-items: center;
        padding-left: 4px;
        box-sizing: border-box;
        border: none;
        outline: none;
    }
    .dropdown-content-a:hover{
        background-color: #2ca01c;
        color: #fff;
    }
    .dropdown-text-and-icon{
        display: flex;
        align-items: center;
        border: 1px solid #babec5;
        border-collapse: collapse;
        border-radius: 2px;

    }
    .dropdown-text-and-icon:hover{
        border-color: #2ca01c;
        /* background-color: aqua; */

    }
    .dropdown-text-and-icon:focus-within{
        border-color: #2ca01c;
        /* background-color: aqua; */

    }
    #dropdown-text{
        height: 31px;
        width: calc(100% - 32px);
        padding: 6px 0 6px 12px;
        box-sizing: border-box;
        border: none;
        outline: none;
        border-collapse: collapse;
        border-radius: 4px;
    }
    #dropdown-icon{
        width: 31px;
        height: 31px;
        border: none;
        outline: none;
        border-collapse: collapse;
        border-radius: 4px;
        background: url(../../assets/img/Sprites.64af8f61.svg) no-repeat;
        background-position: -553px -353px;
        transform: rotate(0deg);
        transition: transform .15s linear;
    }
    #dropdown-icon:focus-within{
        transform: rotate(0deg);
        transition: transform .15s linear;
    }
    
</style>