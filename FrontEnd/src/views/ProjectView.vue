<template>
  <div class="single-project">
    
    <div class="project-view-header">

      <section v-if="errored">
        <h2>Sorry, there was a problem fetching the relevant information</h2>
      </section>

      <!-- Project Description -->
      <section v-else>
        <div v-if="isProjectNull(currentProject)">
          <h2>Please select a Project in the Dashboard</h2>
          <router-link class="routeToDashBoard" :to="{name: 'Dashboard'}">Dashboard</router-link>
        </div>

        <div v-else>
          <single-project 
            class="project-view-information" 
            :singleProject="currentProject"
          />
        </div>
      </section>

      <!-- Navigation between Projects -->
      <model-view-navigation
        class="project-view-nav"
        @previous="setPreviousProject"
        @next="setNextProject"
        :returnTo = this.parentPage
      />
    </div>
    
    <!-- Project Deliverables -->
    <section>
      <ul>
        <deliverable-item 
          v-for="(item, i) in deliverableList"
          :deliverable="item"
          :deliverableList="deliverableList"
          :key="i"
          :projectsList = "projectsList"
          @deleteDeliverable="deleteDeliverable"
        />
      </ul>
    </section> 

    <!-- Add a Deliverable section -->
    <add-deliverable :projectsList = "projectsList" v-on:add-deliverable="addDeliverable"/>

  </div>
</template>

<script>
import DeliverableItem from '../components/DeliverableItem';
import SingleProject from '../components/SingleProject';
import ModelViewNavigation from '../components/ModelViewNavigation';
import ProjectApi from '../ProjectApiFacade';
import DeliverableApi from '../DeliverableApiFacade';
import AddDeliverable from '../components/AddDeliverableComponent';

export default {
  // <router-link to="/" exact>View</router-link> 
  name: 'ProjectView',
  components: {
    'deliverable-item' : DeliverableItem,
    'single-project' : SingleProject,
    'model-view-navigation' : ModelViewNavigation,
    'add-deliverable' : AddDeliverable,
  },
  data(){
    return{
      currentProject: undefined,
      deliverableList:[],
      projectsList:[],
      errored: false,
      parentPage: 'Dashboard'
      
    }//return
  },//data

  async created() {
    this.currentProject = this.$route.params.project !== undefined ? this.$route.params.project: null;
    if(this.currentProject !== null){
      this.getDeliverablesFromProject(this.currentProject.projectId);
    }
    this.getAllProjects();
    // this.getAllDeliverables();
  },//created

  methods: {
    isProjectNull(project){
      return project === null;
    },
    async getAllProjects() {
      this.loading = true

      try {
        this.projectsList = await ProjectApi.getAll();
      } 
      finally {
        this.loading = false
      }
    },
    // Project Navigation
    setPreviousProject(){
      if(this.currentProject === null || this.currentProject === undefined){
        return;
      }

      if(this.currentProject.projectId === 1){
        return;
      }
      else{      
        this.currentProject = this.projectsList[this.currentProject.projectId - 2];
      }
      this.getDeliverablesFromProject(this.currentProject.projectId);      
    },
    setNextProject(){
      if(this.currentProject === null || this.currentProject === undefined){
        return;
      }

      if(this.currentProject.projectId + 1 < this.projectsList.length){
        this.currentProject = this.projectsList[this.currentProject.projectId];
      }
      else{
        this.currentProject = this.projectsList[this.projectsList.length - 1]; 
      }
      this.getDeliverablesFromProject(this.currentProject.projectId);
    },
    async getDeliverablesFromProject(projectId){

      this.loading = true

      try {
        this.deliverableList = await ProjectApi.GetDeliverablesFromProject(projectId);
      } 
      finally {
        this.loading = false
      }
    },
    async addDeliverable(newDeliverable){

      await DeliverableApi.create(newDeliverable);

      if(!this.isProjectNull()){
        this.getDeliverablesFromProject(this.currentProject.projectId);
      }
    },
    async deleteDeliverable(deliverableToDelete){
      await DeliverableApi.delete(deliverableToDelete);

      if(!this.isProjectNull()){
        this.getDeliverablesFromProject(this.currentProject.projectId);
      }
      
    }
  } //methods
}

</script>

<style scoped>
  .project-view-header{
    background: rgb(221, 11, 11);
    color: #fff;
    display: flex;
    align-items: center;
    height: 15vh;
  }
  .project-view-header > * {
    flex: 1 100%;
  }
  .project-view-nav{
    flex:none;
  }
  .project-view-information{
    flex:auto;
  }
  a.routeToDashBoard{
    text-decoration: none;
    padding-left: 5px;
  }
  a.routeToDashBoard.router-link-active{
    color: #555;
  }

</style>