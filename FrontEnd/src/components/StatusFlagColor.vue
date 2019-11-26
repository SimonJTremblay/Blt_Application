<template>
    <div class="center">        
        <span class="dot" :class="minBlufValue"></span>
    </div>
</template>

<script>
export default {
    name:"StatusFlagColor",
    props:{
        minBlufValue: String,
    },    
}
</script>

<style scoped>
 .dot{
        margin: 2px;
        height: 1.5em;
        width: 1.5em;
        border-radius: 50%;
        display: inline-block;
    }
</style>