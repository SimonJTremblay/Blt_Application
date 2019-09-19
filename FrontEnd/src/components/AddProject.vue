<template>
    <div>
        <form @submit="addProject">
            <input type="text" v-model="title" name="title" placeholder="Add Project">
            <input type="submit" value="Submit" class="btn">
        </form>
    </div>
</template>

<script>
//import uuid from 'uuid';
export default {
    name:"AddProject",
    data(){
        return{
            title: ''
        }
    },
    methods: {
        addProject(e){
            //stop default behaviour, ie saving to a file
            e.preventDefault();
            const newProject = {
                title: this.title,
                completed: false
            }
            //send up to parent
            this.$emit('add-project', newProject);

            this.title = '';
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


