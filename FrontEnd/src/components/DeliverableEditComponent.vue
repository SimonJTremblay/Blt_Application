<template>
<!-- 
    Component needs to receive:
        projectsList',
        'employeeList',
        'currentDeliverable'
    Will emit save-template with deliverableToSave attached
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
                    v-model.trim.lazy="this.deliverableToSave.Heading"
                    placeholder="The Heading consists of a brief description of the deliverable."
                >
                </textarea>
            </div>

            <div class="item employee-single-select">
                <multi-select
                    v-model="this.deliverableToSave.Lead"
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
                  <label for="priority">Priority: (value: {{this.deliverableToSave.Priority}})</label><br>
                  <radio-button name="priority" label="1" :value="this.deliverableToSave.Priority" textDisplay="Really Important" @change="changeValuePriority"/>
                  <radio-button name="priority" label="2" :value="this.deliverableToSave.Priority" textDisplay="Somewhat Important" @change="changeValuePriority"/>
                  <radio-button name="priority" label="3" :value="this.deliverableToSave.Priority" textDisplay="Not Important" @change="changeValuePriority"/>
              </div>
            </div>

            <div class="item time-estimation">
                <label class="item-title">Time Estimation (days):</label>
                <input
                    type="number"
                    v-model.number="this.deliverableToSave.TimeEstimation"
                    placeholder="# of days"
                    min="1"
                    class="item-body"
                >
            </div>

            <div class="item projects-multi-select">
                <multi-select
                    v-model="this.deliverableToSave.ProjectIdList"
                    :options="projectsList"
                    :multiple="true"
                    :close-on-select="false"
                    :clear-on-select="false"
                    :hide-selected="true"
                    label="name"
                    track-by="projectId"
                    placeholder="Select Project(s)"
                />
                {{this.deliverableToSave.ProjectIdList}}
            </div>
            
            <div class="item submit">
                <input
                    type="submit"
                    value="Create Deliverable"
                    class="btn"
                >
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
        ['projectsList','employeeList','currentDeliverable'],

    methods: {
        async getAllEmployees() {
            this.loading = true

            try {
            this.EmployeesList = await EmployeeApi.getAll();
            } finally {
            this.loading = false
            }
        }, 
        checkForm: function (e) {
            //stop default behaviour, ie saving to a file
            e.preventDefault();

            //If all is good
            if (this.Heading && this.Priority && this.Lead && this.TimeEstimation && this.ProjectsSelectedList.length > 0) {
                this.errors = [];

                this.saveDeliverable();
                return true;
            }

            this.errors = [];

            if (!this.Heading) {
                this.errors.push('Heading required.');
            }
            if (!this.Priority) {
                this.errors.push('Priority required.');
            }
            if (this.ProjectsSelectedList.length === 0) {
                this.errors.push('Select Project(s).');
            }            
        },  
        saveDeliverable(){
            const ProjectsSelectedIds = [];

            Object.values(this.ProjectsSelectedList).map(value => {
                Object.entries(value).forEach(item => {
                    if(item[0] === 'projectId'){
                        ProjectsSelectedIds.push(item[1]);
                    }
                })
            })

            //send up to parent for save
            this.$emit('save-deliverable',this.deliverableToSave);
            this.ToggleActive();
        },
        initializeDeliverable(){
            if(this.currentDeliverable != undefined){
                this.deliverableToSave = {
                    Heading : this.currentDeliverable.heading,
                    Priority: this.currentDeliverable.priority,
                    Lead: this.currentDeliverable.lead,
                    DateScheduledStart: this.currentDeliverable.dateScheduledStart,
                    TimeEstimation: this.currentDeliverable.timeEstimation,
                    ProjectIdList: this.currentDeliverable.projectIdList
                }
            }
            else{
                this.deliverableToSave = {
                    Heading : '',
                    Priority: null,
                    Lead: null,
                    DateScheduledStart: null,
                    TimeEstimation: null,
                    ProjectIdList: []
                }
            }
        },
        changeValuePriority: function(newValue){
            this.currentDeliverable.priority = newValue;
        },
    }, //methods

    async created() {
        this.getAllEmployees();
        this.initializeDeliverable();
    }//created
}
</script>

<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>

<style scoped>

    .wide-rounded{
        width: 100%;
        border-radius: 15px;
    }

    /* Add a background color to the button if it is clicked on (add the .active class with JS), and when you move the mouse over it (hover) */
    .active, .btn:hover {
        background-color: #ccc;
    }

    /* Adds a '+' sign at the far right of the button */
    .btn:after {
        content: '\002B';
        color: white;
        font-weight: bold;
        float: right;
    }

    /* When is active, says '(active)' on the far right of the button */
    .active:after {
        content: "(active)";
    }


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
        grid-template-columns: 1fr 1fr 1fr;
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
</style>