<template>
    <div>
        <section class="table-header">
                <h3 class="item weight1">ProjectId</h3>
                <h3 class="item weight3">Title</h3>
                <h3 class="item weight4">Descrition</h3>
                <h3 class="item weight2">Lead</h3>
        </section>
            <project-table-item
                v-for="(item, i) in projectsArray"
                :project="item"
                :key="i"
            />
    </div>
</template>

<script>
import ProjectTableItem from "./ProjectTableItem";

export default {
    name:"ProjectTable",
    components:{
      'project-table-item' : ProjectTableItem  
    },
    props:["projectsArray"]    
}
</script>
<style scoped>
    .table-header{
        display: flex;
        padding: 0 10px;
    }

    .item{
        padding: 5px 10px;
        background: #000;
        color: #fff;
        border-left: solid white 2px;
    }
    .item:first-child {
        border-left: none;
    }
    .weight1{
        flex-basis: 5%;
    }
    .weight2{
        flex-basis: 20%;
    }
    .weight3{
        flex-basis: 30%;        
    }
    .weight4{
        flex-basis: 45%;        
    }
</style>