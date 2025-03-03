<script setup>
    import { ref, watch } from 'vue';
    import { RouterView, useRoute, useRouter } from 'vue-router'
    import PageHeader from '@/components/PageHeader.vue';

    const route = useRoute()
    const router = useRouter();
    
    const headerButtons = [
    ]

    const headerTabs = [
        { name: 'payments-projection', text: 'Payments projection by payment method', key:'1' },
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
    <PageHeader title="Reports" :buttons="headerButtons" :tabs="headerTabs" v-model:activeKey="activeTabKey"/>

    <div class="w-full mb-10">
        <RouterView />
    </div>
</template>