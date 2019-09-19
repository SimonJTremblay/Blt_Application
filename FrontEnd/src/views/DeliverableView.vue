<template>
  <div class="single-deliverable">
    <div class="deliverable-view-header">
      <section v-if="errored">
        <h2>Sorry, there was a problem fetching the relevant information</h2>
      </section>

      <!-- Deliverable Description -->
      <section v-else>
        <div v-if="isDeliverableNull(currentDeliverable)">
          <h2>Please select a Deliverable within the Project</h2>
        </div>

        <div v-else>
          <single-deliverable 
            class="project-view-information" 
            :singleDeliverable="currentDeliverable"
          />
        </div>
      </section>

      <!--Navigation between Deliverables-->
        <model-view-navigation
          class="model-view-nav"
          @previous="setPreviousDeliverable"
          @next="setNextDeliverable"
          :returnTo = this.parentPage
        />
    </div>

    <section>
      <AddTaskComponent
        v-on:add-task="addTask"
      />
      <TaskCard 
        v-bind:taskCardsList="taskCardsList"
      />
        <!-- v-on:del-task="deleteTask" -->
    </section>
  </div>
</template>

<script>
import TaskCard from "../components/TaskCard";
import ModelViewNavigation from '../components/ModelViewNavigation';
import AddTaskComponent from "../components/AddTaskComponent";
import SingleDeliverable from '../components/SingleDeliverable';
import TaskApi from '../TaskApiFacade';
export default {
  name: 'Deliverable',
  components: {
    TaskCard,
    'model-view-navigation' : ModelViewNavigation,
    'single-deliverable' : SingleDeliverable,
    AddTaskComponent
  },
  data(){
    return {
      taskCardsList:[],
      DeliverableList: null,
      parentPage: 'ProjectView',
      errored: false,
      currentDeliverable: undefined
    }    
  },
  async created() {
    this.currentDeliverable = this.$route.params.deliverable !== undefined ? this.$route.params.deliverable: null;
    if(this.currentDeliverable !== null){
      this.getTasksFromDeliverable();
    }

    this.DeliverableList = this.$route.params.DeliverableList !== undefined ? this.$route.params.DeliverableList: null;
    console.log('DeliverableList: ' + this.DeliverableList);
  },

  methods: {
    async addTask(newTask){
      if(!this.isDeliverableNull()){
        newTask.Deliverable = this.currentDeliverable;

        await TaskApi.create(newTask);
        
        this.getTasksFromDeliverable();
      }
    },

    isDeliverableNull(deliverable){
      return deliverable === null;
    },

    setPreviousDeliverable(){
      if(this.currentDeliverable === null || this.currentDeliverable === undefined){
        return;
      }

      // if(this.currentProject.projectId === 1){
      //   return;
      // }
      // else{      
      //   this.currentProject = this.projectsList[this.currentProject.projectId - 2];
      // }
      // this.getDeliverablesFromProject();      
    },

    setNextDeliverable(){
      if(this.currentDeliverable === null || this.currentDeliverable === undefined){
        return;
      }

      // if(this.currentProject.projectId + 1 < this.projectsList.length){
      //   this.currentProject = this.projectsList[this.currentProject.projectId];
      // }
      // else{
      //   this.currentProject = this.projectsList[this.projectsList.length - 1]; 
      // }
      // this.getDeliverablesFromProject();
    },

   async getTasksFromDeliverable(){
       this.loading = true

      try {
        this.taskCardsList = await TaskApi.getFromDeliverable(this.currentDeliverable.deliverableId);
      } finally {
        this.loading = false
      }
   },
  //  redirectToParent(){
  //    this.$router.push(
  //     {
  //         name: this.parentPage,
  //         params: { project: this.project }
  //     })
  //  }
  }, //methods
}
</script>

<style scoped>
.deliverable-view-header{
    background: rgb(221, 11, 11);
    color: #fff;
    display: flex;
    align-items: center;
    height: 15vh;
  }
  .deliverable-view-header > * {
    flex: 1 100%;
  }
  .deliverable-view-nav{
    flex:none;
  }
  .deliverable-view-information{
    flex:auto;
  }

</style>
