<template>
    <div>
        <div v-bind:key="task.taskId" v-for="task in taskCardsList">
            <task-item v-bind:task = "task" v-on:del-task="$emit('del-task', task.taskId)"/>
        </div>
    </div>
</template>

<script>
import TaskItem from "./TaskItem";

export default {
    name:"TaskCards",
    components:{
      'task-item' : TaskItem  
    },
    props:["taskCardsList"]    
}
</script>

