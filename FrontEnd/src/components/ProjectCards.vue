<template>
    <div class="container-projectCard">
        <div v-bind:key="project.projectId" v-for="project in projectsArray">
            <ProjectCardsItem 
                class="projectCard"
                v-bind:project = "project"
                v-bind:projectsArray = "projectsArray"
            />  
            <!-- WHY AM I PASSING projectsArray???-->          
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
        grid-gap: 1rem;
        grid-template-columns: repeat(auto-fill, minmax(400px, 1fr));
        grid-template-rows: repeat(auto-fill, 1fr);
    }
    .projectCard{
        font-size: 1.3rem;
        padding: 1.5rem;
        border-radius: 25px;
        background: #f4f4f4;
        height: 100%;
    }

     .projectCard:hover{
        cursor:pointer;
        background: #dbdbdb;
    }
</style>


