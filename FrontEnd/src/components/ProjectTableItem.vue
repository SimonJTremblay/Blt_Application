<template>
    <div class="table-row" @click="redirectToProjectView">
        <h5 class="item weight1">{{project.projectId}}</h5>
        <h5 class="item weight3">{{project.name}}</h5>
        <h5 class="item weight4">{{project.description}}</h5>
        <h5 class="item weight2">{{project.lead}}</h5>
    </div>  
</template>

<script>
export default {
    name: 'project-table-item',
    props: ['project'],
    methods:{
        redirectToProjectView(){
            this.$router.push(
                {
                    name: 'ProjectView',
                    params: { project: this.project }
                }
            )
        }
    }    
}
</script>

<style scoped>
    .table-row{
        display: flex;
        padding: 0 10px;
    }
    .table-row:nth-child(even){
        background-color: #f4f4f4; 
    }
    .table-row:hover{
        background-color: #adadad;
        cursor: pointer;
    }
    .item{
        padding: 5px 10px;
    }
    .weight1{
        flex-basis: 7%;
        text-align: center;
    }
    .weight2{
        flex-basis: 19%;
    }
    .weight3{
        flex-basis: 29%;        
    }
    .weight4{
        flex-basis: 45%;        
    }
</style>


