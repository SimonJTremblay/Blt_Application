<template>
    <div>
        <table>
            <tr>
                <th>Id</th>
                <th>Name</th>
                <th>Descrition</th>
                <th>Lead</th>
            </tr>
            <project-table-item
                v-for="(item, i) in projectsArray"
                :project="item"
                :key="i"
            />
        </table>
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
    table{
        width:100%;
    }
    table tr:nth-child(even) {
        background-color: #eee;
    }
    table tr:nth-child(odd) {
        background-color: #fff;
    }
    table th {
        color: white;
        background-color: black;
        text-align: left;
        padding: 10px;
        font-size: 1.2rem;
    }
    table tr:hover{
        background-color: rgb(111, 135, 202);
        cursor: pointer;
    }
</style>