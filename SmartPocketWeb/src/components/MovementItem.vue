<script setup>
    import { formatMoney, formatShowDate } from '../helpers';
    import { useMovementsStore } from '../stores/movements'; 
    const store = useMovementsStore();

    defineProps({
        movement: {
            type: Object
        }
    })
</script>

<template>
    <div class="p-5 space-y-1 rounded-sm bg-blue-gray-300">
        <div class="flex space-x-7">
            <div class="flex justify-between space-x-2 w-5/6">
                <p class="text-2xl font-normal">{{ movement.movementType.name }}</p>
                <p class="text-xl font-light">{{ movement.category.name }}</p>
            </div>
            <div class="flex space-x-2 w-1/6">
                <RouterLink :to="{ name: 'new-movement' }">
                    <div class="p-3 text-gray-200 uppercase text-xs font-black rounded-lg bg-teal-300 cursor-pointer" >
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 26 26" class="w-5 h-5 fill-current dark:text-teal-800">
                            <path d="M20.094.25a2.245 2.245 0 0 0-1.625.656l-1 1.031l6.593 6.625l1-1.03a2.319 2.319 0 0 0 0-3.282L21.75.937A2.36 2.36 0 0 0 20.094.25zm-3.75 2.594l-1.563 1.5l6.875 6.875L23.25 9.75l-6.906-6.906zM13.78 5.438L2.97 16.155a.975.975 0 0 0-.5.625L.156 24.625a.975.975 0 0 0 1.219 1.219l7.844-2.313a.975.975 0 0 0 .781-.656l10.656-10.563l-1.468-1.468L8.25 21.813l-4.406 1.28l-.938-.937l1.344-4.593L15.094 6.75L13.78 5.437zm2.375 2.406l-10.968 11l1.593.343l.219 1.47l11-10.97l-1.844-1.843z"/>
                        </svg>
                    </div>
                </RouterLink>
                <div 
                    class="p-3 text-gray-200 uppercase text-xs font-black rounded-lg bg-red-300 cursor-pointer" 
                    @click="store.deleteMovement(movement.id)"
                    >
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32" class="w-5 h-5 fill-current dark:text-red-800">
                        <g data-name="Layer 17" id="Layer_17">
                            <path class="cls-1" d="M24,31H8a3,3,0,0,1-3-3V9A1,1,0,0,1,7,9V28a1,1,0,0,0,1,1H24a1,1,0,0,0,1-1V9a1,1,0,0,1,2,0V28A3,3,0,0,1,24,31Z"/>
                            <path class="cls-1" d="M28,7H4A1,1,0,0,1,4,5H28a1,1,0,0,1,0,2Z"/>
                            <path class="cls-1" d="M20,7a1,1,0,0,1-1-1V3H13V6a1,1,0,0,1-2,0V2a1,1,0,0,1,1-1h8a1,1,0,0,1,1,1V6A1,1,0,0,1,20,7Z"/>
                            <path class="cls-1" d="M16,26a1,1,0,0,1-1-1V11a1,1,0,0,1,2,0V25A1,1,0,0,1,16,26Z"/>
                            <path class="cls-1" d="M21,24a1,1,0,0,1-1-1V13a1,1,0,0,1,2,0V23A1,1,0,0,1,21,24Z"/>
                            <path class="cls-1" d="M11,24a1,1,0,0,1-1-1V13a1,1,0,0,1,2,0V23A1,1,0,0,1,11,24Z"/>
                        </g>
                    </svg>
                </div>
            </div>
        </div>
        <div class="flex space-x-3 justify-between">
            <p class="text-xl font-light">{{ movement.description }}</p>
            <p class="text-xl font-normal">{{ formatMoney(movement.amount) }}</p>
        </div>
        <div class="flex space-x-3 justify-between">
            <div class="flex space-x-3">
                <span class="text-xl font-normal">Payment Method:</span>
                <p class="text-xl font-light">{{ movement?.paymentMethod?.name }}</p>
            </div>
            <p class="text-xl font-normal">{{ formatShowDate(movement.movementDate) }}</p>
        </div>
        <div class="flex space-x-3" v-if="movement.recurringPaymentId && movement.installmentNumber">
            <span class="text-xl font-normal">Recurring Payment:</span>
            <p class="text-xl font-light">{{ movement.installmentNumber }} / {{ movement.recurringPayment?.installmentCount }}</p>
        </div>
    </div>
</template>