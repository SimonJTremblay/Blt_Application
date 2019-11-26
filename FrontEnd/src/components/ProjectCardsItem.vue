<template>
    <div class="project-item" @click="redirectToProjectView">
        <div class="item flexed projectNumber">
            Project {{project.projectId}}
            <status-flag-color v-bind:minBlufValue = "minBlufValue" />
        </div>
        <div class="item description">
            <p class="title">{{project.name}}</p>
            <p>Owner: {{project.owner}}</p>
            <p>Lead: {{project.lead ? project.lead: 'N/A'}}</p>            
        </div>
    </div>    
</template>

<script>
import StatusFlagColor from './StatusFlagColor'

export default {
    name:"ProjectItem",
    props: ["project"], 
    components:{
        'status-flag-color': StatusFlagColor
    },
    data(){
        return{
            minBlufValue:''
        }
    },
    async created(){
        this.minBlufValue = await this.getStatusColor(this.project);
    },
    methods: {
        getStatusColor(project){
            switch(project.maxBluf){
              case(1):
                  return 'isGreen'
              case(2):
                  return 'isYellow'
              case(3):
                  return 'isRed'
              default:
                  return 'isGrey'
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
        display: flex;
        flex-flow: column wrap;
        justify-content: space-between;
    }
    .projectNumber{
        text-align: center;
    }    
    .flexed{
        display:flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 10px;
        text-decoration: underline;
    }
    .title{
        font-size: 1.3em;
    }
    
</style>
