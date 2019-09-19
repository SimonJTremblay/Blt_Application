<template>
<!--To use this component, you must:
    - pass "returnTo" property with the route of the parent
    - define the "next" function
    - define the "previous" function    
-->
    <div class="nav-buttons">
        <button
            class="btn firstRow"
            name="modelViewPreviousProjectBtn"
            @click="$emit('previous')"            
        >Previous</button>

        <button
            class="btn firstRow"
            name="modelViewNextProjectBtn"            
            @click="$emit('next')"   
        >Next</button>

        <button
            class="btn secondRow"
            name="modelViewReturnToParentBtn"            
            @click="redirectToParent"   
        >Return to {{returnTo}}</button>
    </div>
</template>

<script>
export default {
    name: 'project-view-navigation',
    props: ['returnTo'],
    methods: {
        redirectToParent(){
            this.$router.push(
                {
                    name: this.returnTo
                })
        }
    } // methods
}
</script>

<style scoped>
    .nav-buttons{
        display: grid;
        grid-gap: 5px;
        grid-template-columns: minmax(100px,1fr);
    }
    .secondRow{
        grid-column: span 2;
    }
</style>


