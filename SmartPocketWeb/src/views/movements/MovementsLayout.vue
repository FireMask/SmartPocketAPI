<script setup>
    import { formatShowDate } from '../../helpers';
    import { useMovementsStore } from '../../stores/movements';

    const store = useMovementsStore()
    const today = new Date();
</script>

<template>    
    <header class="flex sticky justify-between mb-4 flex-col lg:flex-row">
          <h3 class="text-3xl font-medium text-gray-700">
              Movements
          </h3>
          <div class="w-auto text-lg lg:text-xl mt-3 lg:mt-0">
            <p class="font-medium text-gray-700">
              {{ formatShowDate(today) }}
            </p>
            <p>
                <RouterLink :to="{ name: 'new-movement' }" class="text-indigo-600 hover:text-indigo-900 font-medium">
                New Movement
                </RouterLink>
            </p>
            <p>
                <RouterLink :to="{ name: 'new-recurring-payment' }" class="text-indigo-600 hover:text-indigo-900 font-medium">
                New Recurring payment
                </RouterLink>
            </p>
          </div>
      </header >
  
      <main class=""></main>
</template>