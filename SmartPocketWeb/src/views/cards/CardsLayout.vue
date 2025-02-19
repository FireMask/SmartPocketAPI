<script setup>
    import { useCardsStore } from '../../stores/cards';
    import { formatShowDate } from '../../helpers';
    import CardItem from '../../components/CardItem.vue';
    
    const store = useCardsStore();
    const today = new Date();
</script>

<template>    
    <header class="flex sticky justify-between mb-4 flex-col lg:flex-row">
          <h3 class="text-3xl font-medium text-gray-700">
            Cards
          </h3>
          <div class="w-auto text-lg lg:text-xl mt-3 lg:mt-0">
            <p class="font-medium text-gray-700">
              {{ formatShowDate(today) }}
            </p>
            <p>
                <RouterLink :to="{ name: 'new-card' }" class="text-indigo-600 hover:text-indigo-900 font-medium">
                    New card
                </RouterLink>
            </p>
          </div>
      </header >

    <div class="flex flex-wrap gap-12 m-5">
        <CardItem
            v-for="paymentMethod in store.userCards"
            :key="paymentMethod._id"
            :paymentMethod="paymentMethod"
        />
    </div>
</template>