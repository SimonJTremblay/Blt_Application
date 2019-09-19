<template>
    <div class="task-item" >
        <!-- v-bind:class="{'isComplete':task.completed}" -->

        <p>
            <!-- <input type="checkbox" v-on:change="markComplete"> -->
            {{task.name}}
            <!-- <button @click="$emit('del-task', task.taskId)" class="del">X</button> -->
        </p>
    </div>    
</template>

<script>
export default {
    name:"TaskItem",
    props: ["task"],
    methods: {
        markComplete(){
            //this.task.completed = !this.task.completed;
        }
    }
}
</script>

<style scoped>
    .task-item{
        background: #f4f4f4;
        padding: 10px;
        border-bottom: 1px #ccc dotted;
    }

    .task-item:hover{
        cursor:default;
    }

    .isComplete{
        text-decoration: line-through;
    }

    .del{
        background: #ff0000;
        color: #fff;
        border: none;
        padding: 5px 9px;
        border-radius: 50%;
        cursor: pointer;
        float: right;
    }
</style>
