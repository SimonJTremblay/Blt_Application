<template>
<div type="text/x-template" id="modal-template">
  <transition name="modal">
    <div class="modal-mask">
      <div class="modal-wrapper">
        <div class="modal-container">

          <div class="modal-header">
            <slot name="header">
              default header
            </slot>
          </div>

          <div class="modal-body">
            <slot name="body">
              default body
            </slot>

          <div class="blufItem">
            <label for="schedule">Schedule:</label>
            <radio-buttons-selector name="schedule" :currentValue="Schedule" @updateSelection="changeValueSchedule"/>
          </div>

          <div class="blufItem">
            <label for="Budget">Budget:</label>
            <radio-buttons-selector name="Budget" :currentValue="Budget" @updateSelection="changeValueBudget"/>
          </div>

          <div class="blufItem">
            <label for="Scope">Scope:</label>
            <radio-buttons-selector name="Scope" :currentValue="Scope" @updateSelection="changeValueScope"/>
          </div>

          <div class="blufItem">
            <label for="Issues">Issues:</label>
            <radio-buttons-selector name="Issues" :currentValue="Issues" @updateSelection="changeValueIssues"/>
          </div>

          <div class="blufItem">
            <label for="OtherRisks">Other Risks:</label>
            <radio-buttons-selector name="OtherRisks" :currentValue="OtherRisks" @updateSelection="changeValueOtherRisks"/>
          </div>

        </div>

          <div class="modal-footer">
            <div class="NavButtons">
              <button class="btn modal-default-button" @click="saveBluf" v-bind:disabled="!BlufIsCompleteOrDifferent">
                Save
              </button>
              <button class="btn modal-default-button" @click="$emit('close')">
                Cancel
              </button>
            </div>
              <slot name="footer" />
              <!-- {{BlufIsCompleteOrDifferent}}
              {{Schedule}}
              {{Budget}}
              {{Scope}}
              {{Issues}}
              {{OtherRisks}} -->
          </div>

        </div>
      </div>
    </div>
  </transition>
</div>    
</template>

<script>
import RadioButtonsSelector from '../components/RadioButtonsSelector'

export default {
  components:{
      'radio-buttons-selector' : RadioButtonsSelector
  },
  props: {
    deliverable: Object,
  },
  data(){
      return{
          Schedule:0,
          Budget:0,
          Scope:0,
          OtherRisks:0,
          Issues:0,
          currentBluf:null,
      }
  },
  computed:{
    StatusColor: function(){
        return this.getStatusColor()      
    },
    BlufIsCompleteOrDifferent:function(){
      if(this.deliverable.bluf){
        return (this.Schedule != this.deliverable.bluf.schedule ||  this.Budget != this.deliverable.bluf.budget ||  this.Scope != this.deliverable.bluf.scope ||  this.OtherRisks != this.deliverable.bluf.otherRisks ||  this.Issues != this.deliverable.bluf.issues)
      }
      else{
        return (this.Schedule > 0 && this.Budget > 0 && this.Scope > 0 && this.OtherRisks > 0 && this.Issues > 0) 
      }
    }
  },
  methods:{
      changeValueSchedule: function(newValue){
          this.Schedule = newValue;
      },
      changeValueBudget: function(newValue){
          this.Budget = newValue;
      },
      changeValueScope: function(newValue){
          this.Scope = newValue;
      },
      changeValueIssues: function(newValue){
          this.Issues = newValue;
      },
      changeValueOtherRisks: function(newValue){
          this.OtherRisks = newValue;
      },
      saveBluf(){          
        // Instanciate a bluf value from data and use API call to send the bluf
        const date = new Date();
        const bluf = {
            DeliverableId: this.deliverable.deliverableId,
            Schedule: this.Schedule,
            Budget: this.Budget,
            Scope: this.Scope,
            Issues: this.Issues,
            OtherRisks: this.OtherRisks,
            Date: this.formatDate(date)
          }

        this.$emit('saved', bluf, this.StatusColor);
    },
      getStatusColor(){
          this.currentBluf = {
                Schedule: this.Schedule,
                Budget: this.Budget,
                Scope: this.Scope,
                OtherRisks: this.OtherRisks,
                Issues: this.Issues,
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
            return 'isRed';
          }
          else if (atRisk.length > 0){
            return 'isYellow';            
          }
          else{
            return 'isGreen';            
          }
      },
      assignBluf(){
        if(this.deliverable.bluf){
          this.Schedule = this.deliverable.bluf.schedule;
          this.Budget = this.deliverable.bluf.budget;
          this.Scope = this.deliverable.bluf.scope;
          this.OtherRisks = this.deliverable.bluf.otherRisks;
          this.Issues = this.deliverable.bluf.issues;
        }
        else{
          this.Schedule = 0;
          this.Budget = 0;
          this.Scope = 0;
          this.OtherRisks = 0;
          this.Issues = 0;
        }
      },
      formatDate(date) {
          var d = new Date(date),
              month = '' + (d.getMonth() + 1),
              day = '' + d.getDate(),
              year = d.getFullYear();

          if (month.length < 2) 
              month = '0' + month;
          if (day.length < 2) 
              day = '0' + day;

          return [year, month, day].join('-');
    }
  },  //methods
  async created(){
        this.assignBluf();
      },
}
</script>


<style scoped>
.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, .5);
  display: table;
  transition: opacity .3s ease;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}

.modal-container {
  width: 450px;
  margin: 0px auto;
  padding: 20px 30px;
  background-color: rgb(255, 255, 255);
  border-radius: 2px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, .33);
  transition: all .3s ease;
  font-family: Helvetica, Arial, sans-serif;
}

.modal-header h3 {
  margin-top: 0;
  color: #42b983;
}

.modal-body {
  margin: 20px 0;
}

/*
 * The following styles are auto-applied to elements with
 * transition="modal" when their visibility is toggled
 * by Vue.js.
 *
 * You can easily play with the modal transition by editing
 * these styles.
 */

.modal-enter {
  opacity: 0;
}

.modal-leave-active {
  opacity: 0;
}

.modal-enter .modal-container,
.modal-leave-active .modal-container {
  -webkit-transform: scale(1.1);
  transform: scale(1.1);
}

.division{
    margin-bottom: 15px;
}

label{
  font-size: 1.2rem;
  font-weight: bold;
}

.blufItem{
  margin-bottom: 15px;
}

.NavButtons{
  display: flex;
  justify-content: flex-end;
}
.NavButtons > button{
  margin-left: 10px;
}

</style>
