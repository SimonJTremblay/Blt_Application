<template>
    <div>
        <section class="table-header">
                <h3 class="item weight1">Id</h3>
                <h3 class="item weight3">Title</h3>
                <h3 class="item weight4">Descrition</h3>
                <h3 class="item weight2">Owner</h3>
                <h3 class="item weight2">Lead</h3>
                <h3 class="item weight1">Status</h3>
        </section>
            <project-table-item
                class="project-table-item"
                v-for="(project, i) in projectsArray"
                :project="project"
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
        background: #000;
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
        flex-basis: 15%;
    }
    .weight3{
        flex-basis: 40%;        
    }
    .weight4{
        flex-basis: 30%;        
    }
    .project-table-item{
        font-size: 1.3em;
        padding: 0 10px;
    }
</style>