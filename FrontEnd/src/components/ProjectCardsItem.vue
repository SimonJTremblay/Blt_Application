<template>
    <div class="project-item" @click="redirectToProjectView">
        <div class="item flexed projectNumber">
            Project {{project.projectId}}
            <status-flag-color v-bind:minBlufValue = "minBlufValue" />
        </div>
        <div class="item description">
            <p>{{project.name}}</p>
            <p>Lead: {{project.lead}}</p>            
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
        getStatusColor(status){ // change status for attribute of project -> minBlufValue
            switch(status){
              case(1):
                  return 'greenCard'
              case(2):
                  return 'yellowCard'
              case(3):
                  return 'redCard'
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
    p{
        margin-bottom: 5px;
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
