<template>
    <div>
        <li>
            <div class="deliverable-information">
                <div class="item info">{{currentDeliverable.heading}}</div>
                <div class="item info">Est. {{currentDeliverable.timeEstimation}} days</div>
                <div class="item info">Lead: {{currentDeliverable.lead}}</div>
                <button :class="StatusColor" class="item blufBtn clickable" @click="toggleShowBlufModal">{{blufText}}</button>
            </div>
            <div class="deliverable-interaction push">
                <button class="item btn" @click="toggleDeliverableEditModal" >Edit</button>
                <button class="item btn" @click="redirectToDeliverableView">Tasks</button>
                <button class="item btn isRed" @click="toggleShowDeleteConfirmation">Delete</button>
                <button class="item btn" @click="show">Show</button>
            </div>
        </li>

            <!-- use the modal component, pass in the prop -->
        <bluf-modal 
            v-if="showBlufModal" 
            @close="showBlufModal = false"
            @saved="updateStatusColorAndCloseModal"
            :deliverable='currentDeliverable' 
        >
            <!--
                you can use custom content here to overwrite
                default content
            -->
            <h3 v-if="blufDoesNotExist" slot="header">No BLUF Report Found.</h3>
            <template v-else>
                <h3 slot="header" class="header">{{currentDeliverable.heading}}</h3>
                <h6 slot="footer">Last Modified Date: {{lastModifiedDate}}</h6>
            </template>

            <div slot="body"> </div>
        </bluf-modal>

        <delete-confirmation-modal v-if="showDeleteConfirmation" @no="showDeleteConfirmation = false" @yes="emitMessageAndClose(currentDeliverable)">
            <h3 slot="header">Warning!</h3>
            <p slot="body">
                You are about to <u>permanently</u> delete this Deliverable, along with <b>ALL</b> its tasks. Are you sure you want to proceed?
            </p>

        </delete-confirmation-modal>

        <deliverable-edit-modal
            v-if="showDeliverableEditModal"
            @close="toggleDeliverableEditModal"
            @save-deliverable="sendDeliverableToParent"
            :currentDeliverable="currentDeliverable"
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
    props: {
        deliverable: Object,
        deliverableList: Array,
        projectsList: Array
    },
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
            DeliverableStatusColor: null,
            blufDoesNotExist: false,
            lastModifiedDate: null,
            currentDeliverable: this.deliverable,    // Local data property to mutate the current deliverable
            blufText:'',
        }
    },
    computed:{
        StatusColor: function (){
            return this.DeliverableStatusColor;  
        },
    },
    async created(){
        this.getBlufStatus();
    },
    methods: {
        redirectToDeliverableView(){
            this.$router.push(
                {
                    name: 'DeliverableView',
                    params: 
                        { 
                            deliverable: this.currentDeliverable,
                            deliverableList:this.deliverableList
                        }
                }
            )
        },
        show(){
            Object.entries(this.currentDeliverable.bluf).forEach(item => {
                console.log(item);
            });
 
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
        updateStatusColorAndCloseModal(bluf, statusColor){
            // Update Deliverable bluf status color
            this.DeliverableStatusColor = String(statusColor);
            this.showBlufModal = false;
            
            // Emits message to parent to save the bluf
            this.$emit('save-bluf', bluf);

        },
        emitMessageAndClose(deliverable){
            this.$emit('deleteDeliverable', deliverable.deliverableId);
            this.showDeleteConfirmation = false;
        },
        sendDeliverableToParent(deliverableToUpdate){
            this.$emit('save-deliverable', deliverableToUpdate);            
            this.toggleDeliverableEditModal();
        },
        formatDate(date){
            return date.substr(0,10);       //Substring to get only date portion of parameter
        },
        getBlufStatus(){
            //Assign Deliverable Color
            this.DeliverableStatusColor = this.getStatusColor();
        },
        getStatusColor(){
            if(!this.currentDeliverable.bluf){
                this.blufDoesNotExist = true;
                return 'isGrey';
            }

            this.lastModifiedDate = this.formatDate(this.currentDeliverable.bluf.date);

            this.currentBluf = {
               Schedule : this.currentDeliverable.bluf.schedule,
               Budget : this.currentDeliverable.bluf.budget,
               Scope : this.currentDeliverable.bluf.scope,
               OtherRisks : this.currentDeliverable.bluf.otherRisks,
               Issues : this.currentDeliverable.bluf.issue
            }
            let atRisk = [];
            let Problem = [];

            Object.values(this.currentBluf).forEach(item =>{
                if (item == 2){
                    atRisk.push(item);
                }
                else if (item == 3){
                    Problem.push(item);
                }
            });

            if (Problem.length > 0){                
                this.blufText = "Danger!"
                return 'isRed';
            }
            else if (atRisk.length > 0){
                this.blufText = "Warning!"
                return 'isYellow';            
            }
            else{
                this.blufText = "Good!"
                return 'isGreen';            
            }
        },
    },  //methods  
}
</script>

<style scoped>
    li{
        display: flex;
        flex-direction: row;
        padding: 5px;
        margin:5px;
        border: red solid 2px;
    }

    li:hover{
        background: rgb(223, 210, 202);
    }
    .item{
        padding: 10px;
        margin: 4px;
    }
    .deliverable-information{
        width: 100%;
        display: grid;
        grid-template-columns: 3fr 1fr 1fr 1fr;
        grid-template-rows: 1fr;
        margin-right: 10px;
    }
    .deliverable-interaction{
        display: flex;
        flex-direction: row;
    }
    .info{
        border-radius: 15px;
        background: rgb(129, 129, 173);
        margin-right: 5px;
    }
    .blufBtn{
        padding: 10px 10px;
        font-size: 1.2rem;
    }      
    .push{
        margin-left: auto;
    }
    .header{
        font-size: 1.5em;
        text-align: center;
    }
</style>
