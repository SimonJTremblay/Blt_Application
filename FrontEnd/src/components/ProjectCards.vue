<template>
    <div class="container-projectCard">
        <div v-bind:key="project.projectId" v-for="project in projectsArray">
            <ProjectCardsItem 
                class="projectCard"
                v-bind:project = "project"
                v-bind:projectsArray = "projectsArray"
            />            
        </div>
    </div>
</template>

<script>
import ProjectCardsItem from "./ProjectCardsItem";

export default {
    name:"ProjectCards",
    components:{
      ProjectCardsItem  
    },
    props:["projectsArray"]  
}
</script>
<style scoped>
    .container-projectCard{
        display: grid;
        grid-gap: 10px;
        grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
        justify-content: center;
        height: 50vh;
    }
    .projectCard{
        font-size: 1.5rem;
        padding: 50px;
        margin: 10px;
    }
</style>


