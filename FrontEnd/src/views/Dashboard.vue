<template>
  <div id="app">
    <div>
      <toolbar 
        v-on:switchComponent="switchComponent"
        @filterChange="changeFilter"
        />
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
import Toolbar from "../components/Toolbar";
import ProjectApi from '../ProjectApiFacade';
import EmployeeApi from '../EmployeeApiFacade';

export default {
  name: 'Dashboard',
  components: {
    'project-cards' : ProjectCards,
    'project-table' : ProjectTable,
    'toolbar' : Toolbar
  },//components

  data(){
    return {
      loading: false,
      currentComp: 'project-cards',
      projectsArray: [],
      allProjects:[],
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
        this.allProjects = [...this.projectsArray];
    },
    changeName(){
      this.projectsArray= this.projectsArray.map(project =>{
        project.owner = this.getEmployeeName(project.owner);
        project.lead = this.getEmployeeName(project.lead);
        return project;
      })
    },
    getEmployeeName(id){
      for(let i = 0 ; i < this.employeesArray.length ; i++){
        if(this.employeesArray[i].employeeId === id){
          return this.employeesArray[i].name
        }
      }
    },
    changeFilter(filter){
      if(filter === 'all'){
        this.projectsArray = [...this.allProjects];
      }
      else if(filter === 'personal'){
        let array = this.projectsArray.filter(project => {
          if (project.owner === 'Peter Niedre'){
            return project;
          }
        })

        this.projectsArray = [...array];
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
