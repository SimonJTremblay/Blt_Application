<template>
    <div>
        <li>
            <div>{{project.projectId}}</div>
            <div>{{project.name}}</div>
            <div>{{project.description}}</div>
            <div>{{project.lead}}</div>
        </li>

        <router-link :to="{name:'ProjectView', params: { project: project }}">View</router-link>  
    </div>  
</template>

<script>
export default {
    name: 'ProjectListItem',
    props:['project'],
}
</script>

