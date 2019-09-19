<template>
    <div class="deliverableBox">
        <div class="deliverable-information">
            <ul>
                <li>deliverable #{{singleDeliverable.deliverableId}}</li>
                <li>Priority: {{singleDeliverable.priority}}</li>
                <li>Lead: {{singleDeliverable.lead}}</li>
                <li>DateSchedulesStart: {{singleDeliverable.dateScheduledStart}}</li>
                <li>Time Estimation: {{singleDeliverable.timeEstimation}}</li>
            </ul>
        </div>
        <div class="deliverable-description">
            <div>
                {{singleDeliverable.heading}}
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'single-deliverable',
    props: ['singleDeliverable']
}
</script>

<style scoped>
    .deliverableBox{
        width: 100%;
        display: flex;
        flex-direction: row;
        flex: 1 100%;
    }
    .deliverable-information{
        flex-grow: 1;
        padding: 5px;
        flex: none;
    }
    .deliverable-description{
        flex-grow: 2;
        padding: 2px;
        border: 2px solid black;
        margin: 5px;
    }
</style>