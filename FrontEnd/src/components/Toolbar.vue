<template>
    <div class="container">
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

        <button 
        class="btn"
        name="ListViewBtn"
        @click="$emit('switchComponent','project-list')"
        >ListView</button>
    </div>
</template>

<script>
export default {
    name: 'Toolbar'
}
</script>

<style scoped>
    .container{
        display:flex;
        justify-content: flex-end;
        margin-bottom: 20px;
    }
</style>

