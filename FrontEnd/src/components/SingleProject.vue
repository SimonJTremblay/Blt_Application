<template>
    <div class="projectBox">
        <div class="project-information">
            <ul>
                <li>Project #{{singleProject.projectId}}</li>
                <li>Name:{{singleProject.name}}</li>
                <li>Lead: {{singleProject.lead}}</li>
            </ul>
        </div>
        <div class="project-description">
            <div>
                {{singleProject.description}}
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'single-project',
    props: ['singleProject']
}
</script>

<style scoped>
    .projectBox{
        width: 100%;
        display: flex;
        flex-direction: row;
        flex: 1 100%;
    }
    .project-information{
        flex-grow: 1;
        padding: 5px;
        flex: none;
    }
    .project-description{
        flex-grow: 2;
        padding: 2px;
        border: 2px solid black;
        margin: 5px;
    }
</style>


