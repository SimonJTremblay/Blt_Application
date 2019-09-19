<template>
    <div>
        <tr @click="redirectToProjectView">
            <td>{{project.projectId}}</td>
            <td>{{project.name}}</td>
            <td>{{project.description}}</td>
            <td>{{project.lead}}</td>
        </tr>  
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
    td{
        padding: 5px;
    }
</style>


