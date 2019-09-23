<template>
<div>
    <button 
        class="btn wide-rounded"
        name="addDeliverableBtn"
        @click="toggleDeliverableEditModal"
    >Add a Deliverable</button>
    
    <deliverable-edit-modal
        v-if="showDeliverableEditModal"
        @close="toggleDeliverableEditModal"
        @save-deliverable="sendDeliverableToParent"
        :currentDeliverable=null
        :projectsList="projectsList"
    />
   
</div>
</template>

<script>
import DeliverableEditModal from "../components/DeliverableEditModal";

export default {
    components:{
        'deliverable-edit-modal': DeliverableEditModal
    },
    data(){
        return{
            showDeliverableEditModal: false
        }
    }, //data
    props:
        ['projectsList'],

    methods: {
        toggleDeliverableEditModal(){
            this.showDeliverableEditModal = !this.showDeliverableEditModal;
        },
        sendDeliverableToParent(deliverableToAdd){
            //send up to parent for save
            this.$emit('save-deliverable',deliverableToAdd);
            this.toggleDeliverableEditModal();
        }
    }, //methods
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


