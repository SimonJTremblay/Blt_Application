<template>
<div>
    <button 
        class="btn wide-rounded"
        name="addDeliverableBtn"
        :class = "{'active' : isActive}"
        @click="toggleDeliverableEditModal"
    >Add a Deliverable</button>
    
    <deliverable-edit-modal
        v-if="showDeliverableEditModal"
        @close="showDeliverableEditModal = false"
        :currentDeliverable=null
        :projectsList="projectsList"
    />
   
</div>
</template>

<script>
import EmployeeApi from '../EmployeeApiFacade'; 
import DeliverableEditComponent from '../components/DeliverableEditComponent';
import DeliverableEditModal from "../components/DeliverableEditModal";

export default {
    name: 'AddDeliverable',
    components:{
        'deliverable-edit-modal': DeliverableEditModal
    },
    data(){
        return{
            isActive: false,
            deliverable: null,
            EmployeesList: [],
            currentDeliverable: null,
            showDeliverableEditModal: false
        }
    }, //data
    props:
        ['projectsList'],

    methods: {
        toggleDeliverableEditModal(){
            this.showDeliverableEditModal = true;
        },
        async getAllEmployees() {
            this.loading = true

            try {
            this.EmployeesList = await EmployeeApi.getAll();
            } finally {
            this.loading = false
            }
        }, 
        addDeliverable(){
            const ProjectsSelectedIds = [];

            Object.values(this.ProjectsSelectedList).map(value => {
                Object.entries(value).forEach(item => {
                    if(item[0] === 'projectId'){
                        ProjectsSelectedIds.push(item[1]);
                    }
                })
            })

            const deliverableToAdd = {
                Heading : this.Heading,
                Priority: this.Priority,
                Lead: this.Lead.employeeId,
                DateScheduledStart: this.DateScheduledStart,
                TimeEstimation: this.TimeEstimation,
                ProjectIdList: ProjectsSelectedIds
            }

            //send up to parent for save
            this.$emit('add-deliverable',deliverableToAdd);
            this.ToggleActive();
        }
    }, //methods

    async created() {
        this.getAllEmployees();
        this.deliverable = {
            Heading : '',
            Priority : null,
            Lead : [],
            DateScheduledStart : null,
            TimeEstimation : null,
            ProjectsSelectedList : null,
        }
    }//created
}
</script>

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


</style>


