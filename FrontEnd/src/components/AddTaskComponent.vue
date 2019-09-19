<template>
    <div>
        <form @submit="addTask">
            <input type="text" v-model="Name" name="Name" placeholder="Add Task">
            <input type="number" min="1" v-model.number="TimeEstimation" name="TimeEstimation" placeholder="Time Estimation (days)">
            <input type="submit" value="Submit" class="btn">
        </form>
    </div>
</template>

<script>
export default {
    name:"AddTaskComponent",
    data(){
        return{
            Name: '',
            Status: 1,
            TimeEstimation: null,
            DateStart: null,
            DateComplete: null,
            EmployeesAssigned: null,
        }
    },
    methods: {
        addTask(e){
            //stop default behaviour, ie saving to a file
            e.preventDefault();

            const newTask = {
                Name: this.Name,
                Status: this.Status,
                TimeEstimation: this.TimeEstimation,
                DateStart: this.DateStart,
                DateComplete: this.DateComplete,
                EmployeesAssigned: this.EmployeesAssigned,
                Deliverable: null
            }
            //send up to parent
            this.$emit('add-task', newTask);

            this.Name = '';
            this.Status = 0,
            this.TimeEstimation = 0,
            this.DateStart = null,
            this.DateComplete = null,
            this.EmployeesAssigned = null
        }
    }
}
</script>

<style scoped>
    form{
        display: flex;
    }

    input[type="text"]{
        flex:10;
        padding: 5px;
    }

    input[type="submit"]{
        flex:2;
    }

</style>


