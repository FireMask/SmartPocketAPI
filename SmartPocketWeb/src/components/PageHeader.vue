<script setup>
    import { ref } from 'vue';

    const activeKey = defineModel('activeKey');

    defineProps({
        title: {
            type: String
        },
        subtitle: {
            type: String
        },
        buttons: {
            type: Object
        },
        tabs: {
            type: Object
        },
    })

</script>

<template>
    <a-page-header
        class="smart-page-header rounded-md mb-4 bg-gradient-to-b from-slate-50 to-gray-200 shadow-slate-300 shadow-[2px_-10px_30px_4px_#9b2c2c]"
        :ghost="false"
        :title="title"
        :sub-title="subtitle"
    >
        <template #extra>
            <a-button v-for="btn in buttons" type="primary"> <RouterLink :to="{ name: btn.name }">{{ btn.text }} </RouterLink></a-button>
        </template>
        <template #footer>
            <a-tabs v-model:activeKey="activeKey">
                <a-tab-pane v-for="tab in tabs" :tab="tab.text" :key="tab.key"/>
            </a-tabs>
        </template>
    </a-page-header>
</template>

<style scoped>
    .smart-page-header :deep(tr:last-child td) {
        padding-bottom: 0;
    }
</style>