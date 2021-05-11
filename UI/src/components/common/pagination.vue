<template>
    <div class="paging">
                    <button id="previous"
                    @click="onClickPreviousPage()"
                    :disabled="isInFirstPage"
                    >Trước</button>
                    
                    <div id="paging_bar">
                        <ul style="list-style-type: none; margin: 0; padding: 0;" 
                        v-for="page in pages" :key="page.name"
                        >
                            <li style="">
                                <button class="button_page" 
                                @click="onClickPage(page.name)"
                                :disabled="page.isDisabled"
                                :class="{ selected: isPageActive(page.name) }"
                                
                                >{{ page.name }}</button>
                            </li>
                        </ul>
                    </div>
                    <button id="next" 
                    @click="onClickNextPage()"
                    :disabled="isInLastPage"
                    >Sau</button>
    </div>
</template>

<script>

export default({
    props: {
        //Số lượng button hiển thị
    maxVisibleButtons: {
      type: Number,
      required: false,
      default: 3
    },
    //Tổng số trang
    totalPages: {
      type: Number,
      required: true
    },
    //Số lượng bản ghi 1 trang
    perPage: {
      type: Number,
      required: true
    },
    //Trang hiện tại
    currentPage: {
      type: Number,
      required: true
    },
  },
  computed: {
    startPage() {
      if (this.currentPage === 1) {
        return 1;
      }

      if (this.currentPage === this.totalPages) { 
        if (this.totalPages < this.maxVisibleButtons) return 1
        else return this.totalPages - this.maxVisibleButtons + 1;
      }

      return this.currentPage - 1;

    },
    endPage() {
      
      return Math.min(this.startPage + this.maxVisibleButtons - 1, this.totalPages);
      
    },
    pages() {
      const range = [];

      for (let i = this.startPage; i <= this.endPage; i+= 1 ) {
        range.push({
          name: i,
          isDisabled: i === this.currentPage 
        });
      }

      return range;
    },
    isInFirstPage() {
      return this.currentPage === 1;
    },
    isInLastPage() {
      return this.currentPage === this.totalPages;
    },
  },
  methods: {
    onClickFirstPage() {
      this.$emit('pagechanged', 1);
    },
    onClickPreviousPage() {
      this.$emit('pagechanged', this.currentPage - 1);
    },
    onClickPage(page) {
      this.$emit('pagechanged', page);
    },
    onClickNextPage() {
      this.$emit('pagechanged', this.currentPage + 1);
    },
    onClickLastPage() {
      this.$emit('pagechanged', this.totalPages);    
    },
    isPageActive(page) {
      return this.currentPage === page;
    },
  }
})
</script>


<style scoped>
.paging {
    
    /* width: 400px; */
    height: 30px;
    display: flex;
    align-items: center;
    justify-content: center;
    /* background-color: aqua; */
    
}
#paging_bar{
    display: flex;
    align-items: center;
}


.paging #previous {
    height: 30px;
    background-repeat: no-repeat;
    background-position: center;
    border:none;
    outline: none;
    margin-right: 4px;
}

#previous:hover {
    cursor: pointer;
    background-color: #e5e5e5;
}

.paging #next {
    height: 30px;
    background-repeat: no-repeat;
    background-position: center;
    border:none;
    outline: none;
    margin-left: 4px;
}

#next:hover {
    cursor: pointer;
    background-color: #e5e5e5;
}

.button_page{
    margin: 2px;
    /* border: 1px solid #e0e0e0; */
    outline: none;
    border: none;
    cursor: pointer;
}

#backward:active #previous:active #next:active #forward:active{
    cursor: pointer;
    background-color: #e5e5e5;
}
.selected, .selected:hover {
    /* background-color: #019160; */
    border: 1px solid #e0e0e0;    
    font-weight: bold;
    color: black;
    cursor: pointer;
}
</style>