<template>
  <div id="app">
    <div>
      <toolbar v-on:switchComponent="switchComponent" />
    </div>
    <div>
      <component :is="currentComp" 
        v-bind:projectsArray="projectsArray"
        v-bind:CurrentComp="currentComp"
      ></component>
    </div>
  </div>
</template>

<script>
import ProjectCards from "../components/ProjectCards";
import ProjectTable from "../components/ProjectTable";
import ProjectList from "../components/ProjectList";
import Toolbar from "../components/Toolbar";
import ProjectApi from '../ProjectApiFacade';
import EmployeeApi from '../EmployeeApiFacade';

export default {
  name: 'Dashboard',
  components: {
    'project-cards' : ProjectCards,
    'project-table' : ProjectTable,
    'project-list' : ProjectList,
    'toolbar' : Toolbar
  },//components

  data(){
    return {
      loading: false,
      currentComp: 'project-cards',
      projectsArray: [],
      employeesArray:[]
    } //return
  }, //data

  methods: {
    switchComponent(comp){
      this.currentComp = comp;
    },
    async getAll() {
        this.loading = true

        try {
          this.projectsArray = await ProjectApi.getAll();
          this.employeesArray = await EmployeeApi.getAll();
        } finally {
          this.loading = false
        }

        this.changeName();
    },
    changeName(){
      this.projectsArray= this.projectsArray.map(project =>{
        project.lead = this.getLeadName(project.lead);
        return project;
      })
    },
    getLeadName(id){
      for(let i = 0 ; i < this.employeesArray.length ; i++){
        if(this.employeesArray[i].employeeId === id){
          return this.employeesArray[i].name
        }
      }
    }
  }, //methods

  async created() {
    this.getAll();
  },//created
}
</script>

<style>
/* #app {} */

</style>
