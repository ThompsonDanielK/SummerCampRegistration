<template>
    <aside>
        <h2>Additional Information</h2>
        <article v-for="note in camper.notes" v-bind:key="note.noteId">
        </article>
    </aside>
</template>

<script>
export default {
    props:{
        camper: Object,
    }
}
</script>

<style>

</style>