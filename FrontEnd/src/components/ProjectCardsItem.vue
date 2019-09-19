<template>
    <div class="project-item" v-bind:class="getStatusColor(project.Status)" @click="redirectToProjectView">
        {{project.name}}<br>
        Lead: {{project.lead}}<br>
        Last-update: {{getDate()}}
    </div>    
</template>

<script>
export default {
    name:"ProjectItem",
    props: ["project"], 
    methods: {
        getStatusColor(status){
            switch(status){
              case(1):
                  return 'greenCard'
              case(2):
                  return 'yellowCard'
              case(3):
                  return 'redCard'
              default:
                  return ''
          }
        },
        getDate(){
            let date = new Date();
            return `${date.getFullYear()}-${date.getMonth()}-${date.getDay()}`;
        },
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
    .project-item{
        background: #f4f4f4;
        padding: 10px;
        border-bottom: 1px #ccc dotted;
        border-radius: 25px;
    }

    .project-item:hover{
        cursor:pointer;
        background: #dbdbdb;
    }

    .redCard {
        border: 3px dotted #d62020;
    }

    .yellowCard {
        border: 3px dotted #e0e329;
    }
    .greenCard {
        border: 3px dotted #29e335;
    }
</style>
