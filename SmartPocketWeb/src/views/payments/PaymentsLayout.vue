<script setup>
    import { ref, watch } from 'vue';
    import { RouterView, useRoute, useRouter } from 'vue-router'
    import PageHeader from '@/components/PageHeader.vue';

    const route = useRoute()
    const router = useRouter();
    
    const headerButtons = [
        { name: 'new-recurring-payment', text: 'New Recurring payment' },
    ]

    const headerTabs = [
        { name: 'recurring-payments', text: 'Recurring Payments', key:'1' },
        { name: 'pending', text: 'Pending Movements', key:'2' },
    ];
    
    const activeTabKey = ref(headerTabs.find(tab => tab.name === route.name).key);

    watch(activeTabKey, (newKey) => {
        const routeName = headerTabs.find(tab => tab.key === newKey).name
        if(routeName !== route.name)
            router.push({name: routeName});
    });

    watch(route, (newRoute) => {
        const routeKey = headerTabs.find(tab => tab.name === newRoute.name).key
        if(routeKey !== activeTabKey.value)
            activeTabKey.value = routeKey
    });

</script>

<template>
    <PageHeader title="Payments" :buttons="headerButtons" :tabs="headerTabs" v-model:activeKey="activeTabKey"/>

    <div class="w-full mb-10">
        <RouterView />
    </div>
</template>