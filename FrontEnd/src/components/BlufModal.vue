<template>
<div type="text/x-template" id="modal-template">
  <transition name="modal">
    <div class="modal-mask">
      <div class="modal-wrapper">
        <div class="modal-container">

          <div class="modal-header" :class="StatusColor">
            <slot name="header">
              default header
            </slot>
          </div>

          <div class="modal-body">
            <slot name="body">
              default body
            </slot>
            <form>
                <!-- Schedule
                Budget
                Scope
                Other Risks
                Issues -->
                <div class="division">
                    <label for="schedule">Schedule: (value: {{Schedule}})</label><br>
                    <radio-button name="schedule" label="1" :value="Schedule" textDisplay="As Planned" @change="changeValueSchedule"/>
                    <radio-button name="schedule" label="2" :value="Schedule" textDisplay="At Risk" @change="changeValueSchedule"/>
                    <radio-button name="schedule" label="3" :value="Schedule" textDisplay="Problem(s)" @change="changeValueSchedule"/>
                </div>
                
                <div class="division">
                    <label for="budget">Budget: (value: {{Budget}})</label><br>
                    <radio-button name="budget" label="1" :value="Budget" textDisplay="As Planned" @change="changeValueBudget"/>
                    <radio-button name="budget" label="2" :value="Budget" textDisplay="At Risk" @change="changeValueBudget"/>
                    <radio-button name="budget" label="3" :value="Budget" textDisplay="Problem(s)" @change="changeValueBudget"/>
                </div>

                <div class="division">
                    <label for="scope">Scope: (value: {{Scope}}) </label><br>
                    <radio-button name="scope" label="1" :value="Scope" textDisplay="As Planned" @change="changeValueScope"/>
                    <radio-button name="scope" label="2" :value="Scope" textDisplay="At Risk" @change="changeValueScope"/>
                    <radio-button name="scope" label="3" :value="Scope" textDisplay="Problem(s)" @change="changeValueScope"/>
                </div>

                <div class="division">
                    <label for="issues">Issues: (value: {{Issues}})</label><br>
                    <radio-button name="issues" label="1" :value="Issues" textDisplay="As Planned" @change="changeValueIssues"/>
                    <radio-button name="issues" label="2" :value="Issues" textDisplay="At Risk" @change="changeValueIssues"/>
                    <radio-button name="issues" label="3" :value="Issues" textDisplay="Problem(s)" @change="changeValueIssues"/>
                </div>

                <div class="division">
                    <label for="otherRisks">OtherRisks: (value: {{OtherRisks}})</label><br>
                    <radio-button name="otherRisks" label="1" :value="OtherRisks" textDisplay="As Planned" @change="changeValueOtherRisks"/>
                    <radio-button name="otherRisks" label="2" :value="OtherRisks" textDisplay="At Risk" @change="changeValueOtherRisks"/>
                    <radio-button name="otherRisks" label="3" :value="OtherRisks" textDisplay="Problem(s)" @change="changeValueOtherRisks"/>
                </div>
          </form>
          </div>

          <div class="modal-footer">
            <slot name="footer" />
              <button class="modal-default-button" @click="$emit('close')">
                Cancel
              </button>
              <button class="modal-default-button" @click="saveBluf" v-bind:disabled="!BlufIsCompleteOrDifferent">
                Save
              </button>
              {{BlufIsCompleteOrDifferent}}
              {{Schedule}}
              {{Budget}}
              {{Scope}}
              {{Issues}}
              {{OtherRisks}}
          </div>

        </div>
      </div>
    </div>
  </transition>
</div>    
</template>

<script>
import RadioButtons from '../components/RadioButtons'
export default {
  components:{
      'radio-button' : RadioButtons
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
  width: 400px;
  margin: 0px auto;
  padding: 20px 30px;
  background-color: #fff;
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

.modal-default-button {
  float: right;
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
</style>
