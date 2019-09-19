<template>
    <label class="radio">
        <input type="radio" :value="label" :name="name" v-model="radioButtonValue">
        <label :for="name">{{textDisplay}}</label>
    </label>
</template>

<script>
export default {
    props: ['name', 'label','value','textDisplay'],
    computed: {
        radioButtonValue: {
            get: function() {
                return this.value
            },
            set: function() {
                // Communicate the change to parent component so that selectedValue can be updated
                this.$emit("change", this.label)
            }
        }
    },

}
</script>

<style scoped>
    body {
    margin: 20px;
    font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
    }
    label.radio {
    margin-right: 20px;
    }
    .result {
    margin-top: 15px;
    border-top: 1px solid #DDD;
    padding-top: 15px;
    }

</style>