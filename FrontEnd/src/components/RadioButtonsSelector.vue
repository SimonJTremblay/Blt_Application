<template>
    <div class="buttons-container">
        <radio-button textDisplay="On Track" :value=1 selectionColor="isGreen" :isSelected="isSelected" @updateSelection="updateSelection" />
        <radio-button textDisplay="At Risk" :value=2 selectionColor="isYellow" :isSelected="isSelected" @updateSelection="updateSelection" />
        <radio-button textDisplay="Problem(s)" :value=3 selectionColor="isRed" :isSelected="isSelected" @updateSelection="updateSelection" />
    </div>
</template>

<script>
import RadioButton from './RadioButton'
export default {
    props:{
        currentValue: Number
    },
    components:{
        'radio-button': RadioButton
    },

    data(){
        return{
            isSelected: 0
        }
    },
    methods:{
        updateSelection(selection){
            this.isSelected = selection;
            this.$emit('updateSelection', selection);
        }
    },
    async created(){
        this.isSelected = this.currentValue;
    }
}
</script>

<style scoped>
    .buttons-container{
        display: flex;
        flex-wrap: nowrap;
        justify-content: space-between;
    }
</style>