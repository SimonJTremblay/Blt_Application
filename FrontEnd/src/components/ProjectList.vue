<template>
    <div>
        <ul class="project-list">
            <project-list-item
                v-for="(item, i) in projectsArray"
                :project="item"
                :key="i"
            />
        </ul>
    </div>
</template>

<script>
import ProjectListItem from './ProjectListItem'

export default {
    name: 'ProjectList',
    props: ['projectsArray'],
    components: {
        'project-list-item': ProjectListItem
    }
}
</script>

<style scoped>

</style>


