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
        margin-bottom: 10px;
    }
    button{
        padding: 10px 30px;
    }
</style>

