<script setup>
    import { useCardsStore } from '../stores/cards';
    import { GrEdit, GrTrash } from 'vue-icons-plus/Gr';
    const store = useCardsStore();

    defineProps({
        paymentMethod: {
            type: Object
        }
    })
</script>

<template>
    <div class="max-w-80 min-w-80 w-80 sm:w-64 sm:min-w-64mt-6 overflow-hidden bg-white rounded-lg shadow-lg min-h-52">
        <div class="px-6 py-4">
            <div class="flex justify-between border-b border-gray-300">
                <div class="mb-2 text-xl font-bold text-gray-900"> 
                    {{ paymentMethod.name }} 
                </div>
                <span class="inline-block px-3 py-1 mb-2 mr-2 text-sm font-semibold text-gray-700 bg-gray-200 rounded-full">
                    {{ paymentMethod.paymentMethodType.name }}
                </span>
            </div>
            <div class="flex justify-between flex-col sm:flex-row">
                <div>    
                    <p v-if="paymentMethod?.bank" class="text-base text-gray-700 my-2">
                        {{ paymentMethod.bank }}
                    </p>
                    <div v-if="paymentMethod?.isCreditCard" class="flex space-x-3" >
                        <span class="font-normal">Due date:</span>
                        <span class="font-light">{{ paymentMethod.dueDate }}</span>
                    </div>
                    <div v-if="paymentMethod?.isCreditCard" class="flex space-x-3" >
                        <span class="font-normal">Transaction date:</span>
                        <span class="font-light">{{ paymentMethod.transactionDate }}</span>
                    </div>
                    <div v-if="paymentMethod?.isCreditCard && paymentMethod.defaultInterestRate !=0" class="flex space-x-3" >
                        <span class="font-normal">Interest rate:</span>
                        <span class="font-light">{{ paymentMethod.defaultInterestRate }}%</span>
                    </div>
                </div>
                <div class="mt-3 text-gray-500 flex flex-row space-x-2 h-fit">
                    <RouterLink :to="{ name: 'new-card' }" class="cursor-pointer rounded-full bg-emerald-100 p-2">
                        <GrEdit/>
                    </RouterLink>
                    <div @click="store.deleteCard(paymentMethod.id)" class="cursor-pointer rounded-full bg-rose-200 p-2">
                        <GrTrash/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>