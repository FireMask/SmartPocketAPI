<script setup>
    import { formatMoney, formatShowDate } from '../helpers';

    defineProps({
        payment: {
            type: Object
        }
    })


</script>

<template>
    <div class="h-auto p-4 rounded-lg shadow-md bg-emerald-50 flex flex-col justify-between mb-4 bg-[url('/img/bill.svg')] bg-center bg-no-repeat">
        <!-- Top Section -->
        <div class="flex justify-between items-center">
            <div class="font-bold text-black text-lg">{{ payment.cardName}}</div>
            <a-popover placement="leftBottom">
                <template #title>
                    <p class="text-lg font-bold">{{formatMoney(payment.totalSum)}} <span class="text-xs text-cyan-800"> Total</span></p>
                </template>
                <template #content>
                    <p class="text-cyan-600 text-lg font-semibold">{{formatMoney(payment.thisPeriodAmount)}} <span class="text-xs text-cyan-900"> Registered</span></p>
                    <p class="text-yellow-600 text-lg font-semibold">{{formatMoney(payment.pendingMovementsAmount)}} <span class="text-xs text-cyan-900"> Pending</span></p>
                </template>
                <div class="text-xl font-bold text-emerald-600">{{ formatMoney(payment.totalSum) }}</div>
            </a-popover>
        </div>

        <!-- Bottom Section -->
        <div class="border-t border-gray-300 pt-2 text-sm text-gray-600 flex flex-col gap-1">
            <div class="font-bold">
                Due Date:
                <span class="font-normal">{{ formatShowDate(payment.dueDate) }}</span>
            </div>
            <div class="font-bold">
                Calculation period start:
                <span class="font-normal">{{ formatShowDate(payment.transactionStartDate) }}</span>
            </div>
            <div class="font-bold">
                Calculation period end:
                <span class="font-normal">{{ formatShowDate(payment.transactionEndDate) }}</span>
            </div>
        </div>
    </div>
</template>