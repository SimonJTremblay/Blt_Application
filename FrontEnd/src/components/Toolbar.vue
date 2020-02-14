<template>
    <div class="container">
        <multi-select
            v-model="value"
            :options="options"
            :close-on-select="true"
            :searchable="false"
            :show-labels="false"
            :allow-empty="false"
            @select="onSelect"
        />

        <button 
        class="btn"
        name="TableViewBtn"
        @click="$emit('switchComponent','project-table')"
        >TableView</button>
        
        <button 
        class="btn"
        name="GridViewBtn"
        @click="$emit('switchComponent','project-cards')"
        >GridView</button>

    </div>
</template>

<script>
import Multiselect from 'vue-multiselect'; 

export default {
    components:{
        'multi-select': Multiselect,
    },
    data(){
        return{
            value: 'all',
            options: ['all', 'personal']
        }
    },
    methods: {
        onSelect(option){
            this.$emit('filterChange', option)
        }
    }
}
</script>

<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>

<style scoped>
    .container{
        display:flex;
        justify-content: flex-end;
        margin-bottom: 10px;
    }
    button{
        padding: 10px 30px;
    }
</style>

