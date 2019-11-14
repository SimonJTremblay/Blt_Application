<template>
    <div class="container" @click="emitSelection" :class="colorObject">
        <h3>{{textDisplay}}</h3>
    </div>
</template>

<script>
export default {
    props: {
        textDisplay: String,
        value: Number,
        isSelected: Number,
        selectionColor: {
            type:String,
            default: 'isGrey',
        }
    },
    data(){
        return{
            color: null
        }
    },
    computed: {
        colorObject: function () {
               if(this.value === this.isSelected){
                   return this.selectionColor;                   
               } else{
                   return 'isGrey';
               }
        },
    },
    methods:{
        emitSelection(){
           this.$emit("updateSelection", this.value);
        }
    },
}
</script>

<style scoped>
    .container{
        cursor: pointer;
        padding: 5px 15px;
        text-align: center;
    }

</style>