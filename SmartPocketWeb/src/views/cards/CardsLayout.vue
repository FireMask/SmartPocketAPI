<script setup>
    import CardItem from '../../components/CardItem.vue';
    import { useCardsStore } from '../../stores/cards';
    const store = useCardsStore();
</script>

<template>    
    <div class="flex justify-between items-center">
        <div >
            <h1 class="text-2xl lg:text-6xl font-black text-gray-800">Cards</h1>
        </div>
        <div class="flex flex-col items-center">
            <nav class="flex gap-2 items-center justify-end">
                <RouterLink :to="{ name: 'new-card' }"
                    class="p-3 text-gray-200 uppercase text-xs font-black rounded-lg bg-teal-500">
                    New card
                </RouterLink>
            </nav>
        </div>
    </div>

    <div class="grid grid-cols-2 gap-5 mt-5">
        <CardItem
            v-for="paymentMethod in store.userPaymentMethods"
            :key="paymentMethod._id"
            :paymentMethod="paymentMethod"
        />
    </div>
</template>