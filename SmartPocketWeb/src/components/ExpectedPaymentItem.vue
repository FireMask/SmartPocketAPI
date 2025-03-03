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
            <div class="text-xl font-bold text-emerald-600">{{ formatMoney(payment.totalSum) }}</div>
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