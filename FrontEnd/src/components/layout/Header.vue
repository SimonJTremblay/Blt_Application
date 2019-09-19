<template>
    <header class="header">
        <h1>The BLT</h1>
        <div id="nav">
            <router-link to="/">Dashboard</router-link> |
            <router-link to="ProjectView">Project-View</router-link> |
            <router-link to="DeliverableView">Deliverables</router-link>
        </div>
    </header>
</template>

<script>
export default {
    name:"Header"
}
</script>

<style scoped>
    .header{
        background: #333;
        color: #fff;
        text-align: center;
        padding: 10px;
    }
    .header a{
        color: #fff;
        padding-right: 10px;
        text-decoration: none;
        cursor: pointer;
    }
</style>




