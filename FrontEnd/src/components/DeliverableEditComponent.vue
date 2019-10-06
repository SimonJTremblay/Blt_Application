<template>
<!-- 
    Component needs to receive:
        projectsList',
        'currentDeliverable'
    Will emit "save-template" with deliverableToSave attached
-->
<div>
    <section class="form-container">
        <form 
            @submit="checkForm"
        >
            <p v-if="errors.length">
                <b>Please correct the following error(s):</b>
                <ul>
                <li :key="error" v-for="error in errors">{{ error }}</li>
                </ul>
            </p>
            
            <div class="item heading">
                <label>Heading:</label>
                <br>
                <textarea
                    v-model.trim.lazy="deliverableToSave.Heading"
                    placeholder="The Heading consists of a brief description of the deliverable."
                >
                </textarea>
            </div>

            <div class="item employee-single-select">
            <div>
                <label>Lead <span class="redish">- optional</span></label>
            </div>
                <multi-select
                    v-model="deliverableToSave.Lead"
                    :options="EmployeesList"
                    :multiple="false"
                    :close-on-select="true"
                    :clear-on-select="false"
                    :hide-selected="true"
                    open-direction="bottom"
                    label="name"
                    track-by="employeeId"
                    placeholder="Select Lead"
                />
            </div>

            <div class="item priority">
                <label for="priority-buttons">Priority:</label>
                 <div class="division">
                  <label for="priority">Priority: (value: {{deliverableToSave.Priority}})</label><br>
                  <radio-button name="priority" label="1" :value="deliverableToSave.Priority" textDisplay="Really Important" @change="changeValuePriority"/>
                  <radio-button name="priority" label="2" :value="deliverableToSave.Priority" textDisplay="Somewhat Important" @change="changeValuePriority"/>
                  <radio-button name="priority" label="3" :value="deliverableToSave.Priority" textDisplay="Not Important" @change="changeValuePriority"/>
              </div>
            </div>

            <div>
                <label for="timeScheduledStart">Scheduled Start <span class="redish">- optional</span></label>
                <input name="timeScheduledStart" v-model="deliverableToSave.DateScheduledStart" type="date">
            </div>

            <div class="item time-estimation">
                <label class="item-title">Time Estimation (days) <span class="redish">- optional</span></label>
                <input
                    type="number"
                    v-model.number="deliverableToSave.TimeEstimation"
                    placeholder="# of days"
                    min="1"
                    class="item-body"
                >
            </div>

            <div class="item projects-multi-select">
                <multi-select
                    v-model="deliverableToSave.ProjectIdList"
                    :options="projectsList"
                    :multiple="true"
                    :close-on-select="false"
                    :clear-on-select="false"
                    :hide-selected="true"
                    label="name"
                    track-by="projectId"
                    placeholder="Select Project(s)"
                />
            </div>
            
            <div class="item submit">
                <input
                    type="submit"
                    value="Create Deliverable"
                    class="btn"
                >
                <button class="btn" @click="$emit('close')">Cancel</button>
            </div>
        </form>
        
    </section>
</div>
</template>

<script>
import EmployeeApi from '../EmployeeApiFacade'; 
import Multiselect from 'vue-multiselect'; 
import RadioButtons from '../components/RadioButtons'

export default {
    components:{
        'multi-select': Multiselect,
        'radio-button': RadioButtons
    },
    data(){
        return{
            errors: [],
            isActive: false,
            EmployeesList: [],
            deliverableToSave: null
        }
    },//data
    props:
        ['projectsList','currentDeliverable'],

    methods: {
        async getAllEmployees() {
            this.loading = true

            try {
            this.EmployeesList = await EmployeeApi.getAll();
            this.getCurrentLeader();
            } finally {
            this.loading = false
            }
        }, 
        getCurrentLeader(){
            this.deliverableToSave.Lead = this.EmployeesList.filter(value => value.employeeId == this.deliverableToSave.Lead)
        },
        getProjectsObject(){
            var element, project;
            let tempArray = [];

            // Loop over given deliverable to find list of projectIds and extract the project object associated with that Id from the ProjectList
            for(project of this.currentDeliverable.projectIdList){
                for(element of this.projectsList){
                    if(element.projectId === project){
                        let el = element;
                        tempArray.push(el);
                    }
                }
            }

            // Empty the temporary array of project objects
            while(tempArray.length > 0){
                this.deliverableToSave.ProjectIdList.push(tempArray.shift());
            }
        },
        checkForm: function (e) {
            //stop default behaviour, ie saving to a file
            e.preventDefault();          

            //If all is good
            if (   this.deliverableToSave.Heading 
                && this.deliverableToSave.Priority
                && this.deliverableToSave.ProjectIdList.length > 0) {
                this.errors = [];

                //If no time estimation was set, we put null
                if(this.deliverableToSave.Lead.length === 0){
                    this.deliverableToSave.Lead = null;
                }

                this.saveDeliverable();
                return true;
            }

            this.errors = [];

            if (!this.deliverableToSave.Heading) {
                this.errors.push('Heading required.');
            }
            if (!this.deliverableToSave.Priority) {
                this.errors.push('Priority required.');
            }
            if (this.deliverableToSave.ProjectIdList.length === 0) {
                this.errors.push('Project(s) required.');
            }           
        },  
        saveDeliverable(){
            let ProjectsSelectedIds = [];

            // returning only the id from the select Projects
            Object.values(this.deliverableToSave.ProjectIdList).map(value => {
                Object.entries(value).forEach(item => {
                    if(item[0] === 'projectId'){
                        ProjectsSelectedIds.push(item[1]);
                    }
                })
            })
            
            
            this.deliverableToSave.ProjectIdList = ProjectsSelectedIds;

            if(this.deliverableToSave.Lead != null){
                this.deliverableToSave.Lead = this.deliverableToSave.Lead.employeeId;
            } 
            
            //send up to parent for save
            this.$emit('save-deliverable',this.deliverableToSave);

        },
        initializeDeliverable(){
            if(this.currentDeliverable != null){
                this.deliverableToSave = {
                    DeliverableId: this.currentDeliverable.deliverableId,
                    Heading : this.currentDeliverable.heading,
                    Priority: this.currentDeliverable.priority,
                    Lead: this.currentDeliverable.lead,
                    DateScheduledStart: this.currentDeliverable.dateScheduledStart,
                    TimeEstimation: this.currentDeliverable.timeEstimation,
                    ProjectIdList : []
                    
                }

                if(this.deliverableToSave.DateScheduledStart){
                    this.deliverableToSave.DateScheduledStart = this.formatDate(this.deliverableToSave.DateScheduledStart);
                }

                this.getProjectsObject();
            }
            else{
                this.deliverableToSave = {
                    Heading : '',
                    Priority: 0,
                    Lead: 0,
                    DateScheduledStart: null,
                    TimeEstimation: null,
                    ProjectIdList: []
                }                
            }
        },
        changeValuePriority: function(newValue){
            this.deliverableToSave.Priority = newValue;
        },
        formatDate(date){
            return date.substr(0,10);       //Substring to get only date portion of parameter
        }
    }, //methods

    async created() {
        this.getAllEmployees();
        this.initializeDeliverable();
    }//created
}
</script>

<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>

<style scoped>


/**********************
    FORM - GRID      
*********************/

    .heading {
        grid-area: heading;
    }
    .priority {
        grid-area: priority;
    }
    .time-estimation {
        grid-area: time-estimation;
    }
    .submit {
        grid-area: submitArea;
    }
    .projects-multi-select{
        grid-area: projects-multi-select;
    }
    .form-container {
        display: grid;
        grid-gap: 10px;
        grid-template-columns: 1fr;
        grid-template-rows: repeat(auto-fill, minmax(200px, 1fr));
        grid-template-areas:
            "heading heading ."
            "priority . time-estimation"
            "projects-multi-select projects-multi-select projects-multi-select"
            "submitArea submitArea submitArea";
        
        justify-items: stretch;
        align-items: stretch;
        border: 5px solid red;
        align-content: stretch;
    }
    .item{
        border: 3px solid black;
    }


    .line-item{
        width: 100%;
        display: flex;
        flex-direction: row;
        flex: 1 100%;
    }
    .item-title{
        flex-grow: 1;
        padding: 5px;
        flex: none;
    }
    .item-body{
        flex-grow: 2;
        padding: 5px;
        border: 2px solid black;
        margin: 5px;
    }

   .radioBtn{
        padding: 5px;
    }

    #one {
        margin-right: 5px;
    }
    #two {
        margin-right: 5px;
    }
    #three {
        margin-right: 5px;
    }
    .redish{
        color: #bb3838;
    }
</style>