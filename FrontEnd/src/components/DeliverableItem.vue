<template>
    <div>
        <li :class="StatusColor">
            <div class="item">{{deliverable.deliverableId}}</div>|
            <div class="item">{{deliverable.heading}}</div>|
            <div class="item">Estimated {{deliverable.timeEstimation}} days</div>|
            <div class="item">Lead: {{deliverable.lead}}</div>
            <button class="item btn push" @click="toggleShowBlufModal">Bluf</button>
            <button class="item btn" @click="toggleDeliverableEditModal" >Edit</button>
            <button class="item btn" @click="redirectToDeliverableView">Tasks</button>
            <button class="item btn isRed" @click="toggleShowDeleteConfirmation">Delete</button>
        </li>    

        <!-- use the modal component, pass in the prop -->
        <bluf-modal v-if="showBlufModal" @close="showBlufModal = false" @saved="updateStatusColorAndCloseModal" >
        <!--
            you can use custom content here to overwrite
            default content
        -->
            <h3 slot="header">{{deliverable.heading}}</h3>
            <div slot="body"> </div>

        </bluf-modal>

        <delete-confirmation-modal v-if="showDeleteConfirmation" @no="showDeleteConfirmation = false" @yes="emitMessageAndClose(deliverable)">
            <h3 slot="header">Warning!</h3>
            <p slot="body">
                You are about to <u>permanently</u> delete this Deliverable, along with <b>ALL</b> its tasks. Are you sure you want to proceed?
            </p>

        </delete-confirmation-modal>

        <deliverable-edit-modal
            v-if="showDeliverableEditModal"
            @close="toggleDeliverableEditModal"
            @save-deliverable="sendDeliverableToParent"
            :currentDeliverable="deliverable"
            :projectsList="projectsList"
        />
    </div>
</template>

<script>
import BlufModal from "../components/BlufModal";
import DeleteConfirmationModal from "../components/DeleteConfirmationModal";
import DeliverableEditModal from "../components/DeliverableEditModal";

export default {
    name:'deliverable-item',
    props: ['deliverable','deliverableList','Status','projectsList'],
    components:{
        'bluf-modal' : BlufModal,
        'delete-confirmation-modal' : DeleteConfirmationModal,
        'deliverable-edit-modal': DeliverableEditModal
    },
    data(){
        return{
            showBlufModal:false,
            showDeleteConfirmation:false,
            showDeliverableEditModal: false,
            DeliverableStatusColor: null
        }
    },
    computed:{
        StatusColor: function(){
            return this.DeliverableStatusColor;    
        }
    },
    methods: {
        redirectToDeliverableView(){
            this.$router.push(
                {
                    name: 'DeliverableView',
                    params: 
                        { 
                            deliverable: this.deliverable,
                            deliverableList:this.deliverableList
                        }
                }
            )
        },
        toggleShowBlufModal(){
            this.showBlufModal = true;
        },
        toggleShowDeleteConfirmation(){
            this.showDeleteConfirmation = true;
        },
        toggleDeliverableEditModal(){
            this.showDeliverableEditModal = !this.showDeliverableEditModal;
        },
        updateStatusColorAndCloseModal(bluf){
            this.DeliverableStatusColor = bluf;
            this.showBlufModal = false;
        },
        emitMessageAndClose(deliverable){
            this.$emit('deleteDeliverable', deliverable.deliverableId);
            this.showDeleteConfirmation = false;
        },
        sendDeliverableToParent(deliverableToUpdate){
            this.$emit('save-deliverable', deliverableToUpdate);            
            this.toggleDeliverableEditModal();

        }
    }    
}
</script>

<style scoped>
    li{
        display: flex;
        flex-direction: row;
        border: 2px solid black;
        border-radius: 15px;
        padding: 5px;
        margin:5px;
    }
    .item{
        padding: 10px;
    }
    .push{
        margin-left: auto;
    }
</style>

