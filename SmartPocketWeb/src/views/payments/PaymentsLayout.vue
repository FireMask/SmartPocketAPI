<script setup>
    import { ref, watch } from 'vue';
    import { formatShowDate, formatMoney } from '../../helpers';
    import { RouterLink, RouterView, useRoute } from 'vue-router'

    const today = new Date();
    const route = useRoute()
    const recurringPage = ref(route.name == "recurring-payments");
    
    watch(route, (newRoute) => {
        if(newRoute?.name)
            recurringPage.value = route.name == "recurring-payments";
    });
</script>

<template>
    <header class="flex sticky justify-between mb-4 flex-col lg:flex-row">
        <h3 class="text-3xl font-medium text-gray-700">
            Payments
        </h3>
        <div class="w-auto text-lg lg:text-xl mt-3 lg:mt-0">
            <p class="font-medium text-gray-700">
                {{ formatShowDate(today) }}
            </p>
            <p>
                <RouterLink :to="{ name: 'new-recurring-payment' }"
                    class="text-indigo-600 hover:text-indigo-900 font-medium">
                    New Recurring payment
                </RouterLink>
            </p>
        </div>
    </header>
    <div class="">
        <div class="border-b border-gray-200">
            <nav class="-mb-px flex gap-6" aria-label="Tabs">
                <RouterLink :to="{ name: 'recurring-payments' }" aria-current="page"
                    :class="recurringPage? 'text-sky-600 border-gray-300' : 'text-gray-500 hover:border-gray-300 hover:text-gray-700'"
                    class="shrink-0 border-b-2 border-transparent px-1 pb-4 text-xl font-medium ">
                    Recurring Payments
                </RouterLink>

                <RouterLink :to="{ name: 'pending' }"
                    :class="recurringPage? 'text-gray-500 hover:border-gray-300 hover:text-gray-700' : 'text-sky-600 border-gray-300'"
                    class="shrink-0 border-b-2 border-transparent px-1 pb-4 text-xl font-medium ">
                    Pending Movements
                </RouterLink>
            </nav>
        </div>
    </div>
    <div class="h-[calc(93vh-8rem)] w-full mb-10">
        <RouterView />
    </div>
</template>