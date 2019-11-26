<template>
    <div class="table-row" @click="redirectToProjectView">
        <h5 class="item weight1">{{project.projectId}}</h5>
        <h5 class="item weight3">{{project.name}}</h5>
        <h5 class="item weight4">{{project.description}}</h5>
        <h5 class="item weight2">{{project.owner}}</h5>
        <h5 class="item weight2">{{project.lead}}</h5>
        <status-flag-color class="item weight1" v-bind:minBlufValue = "minBlufValue"/>
    </div>  
</template>

<script>
import StatusFlagColor from './StatusFlagColor'
export default {
    name: 'project-table-item',
    props: ['project'],
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
    methods:{
        redirectToProjectView(){
            this.$router.push(
                {
                    name: 'ProjectView',
                    params: { project: this.project }
                }
            )
        },
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
    }   //methods    
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
        flex-basis: 5%;
        text-align: center;
    }
    .weight2{
        flex-basis: 14%;
    }
    .weight3{
        flex-basis: 37%;        
    }
    .weight4{
        flex-basis: 25%;        
    }
</style>


