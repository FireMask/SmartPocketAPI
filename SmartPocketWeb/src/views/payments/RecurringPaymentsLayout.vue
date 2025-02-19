<script setup>
    import { formatShowDate, formatMoney } from '../../helpers';
    import { useRecurringPaymentsStore } from '../../stores/recurringPayments';
    import { GrEdit, GrTrash } from 'vue-icons-plus/Gr';

    const store = useRecurringPaymentsStore()
</script>

<template>
    <div class="overflow-auto h-full">
        <table class="w-full h-max align-middle border-b border-gray-200 shadow sm:rounded-lg">
            <thead>
                <tr>
                    <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
                        Payment
                    </th>
                    <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
                        Payment Method
                    </th>
                    <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
                        Dates
                    </th>
                    <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
                        Frequency
                    </th>
                    <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
                        Amount
                    </th>
                    <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
                        Paid count
                    </th>
                    <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50"></th>
                </tr>
            </thead>

            <tbody class="bg-white">
                <tr v-for="(m, id) in store.userRecurringPayments" :key="id">
                    <td class="px-6 py-4 border-b border-gray-200 whitespace-nowrap">
                        <div class="flex items-center">
                            <div class="ml-4">
                                <div class="text-sm font-medium leading-5 text-gray-900">
                                    {{ m.categoryName }}
                                </div>
                                <div class="text-sm leading-5 text-gray-500">
                                    {{ m.description }}
                                </div>
                            </div>
                        </div>
                    </td>

                    <td class="px-6 py-4 border-b border-gray-200 whitespace-nowrap">
                        <div class="text-sm leading-5 text-gray-900">
                            {{ m.paymentMethodName }}
                        </div>
                    </td>

                    <td class="px-6 py-4 border-b border-gray-200 whitespace-nowrap">
                        <span class="inline-flex text-xs font-normal leading-5">
                            <div class="ml-4">
                                <div class="text-sm font-medium leading-5 text-gray-900">
                                    <span>Start:</span>{{ formatShowDate(m.startDate) }}
                                </div>
                                <div v-if="!!m.lastInstallmentDate" class="text-sm leading-5 text-gray-500">
                                    Last payment:{{ formatShowDate(m.lastInstallmentDate) }}
                                </div>
                                <div class="text-sm leading-5 text-gray-500">
                                    <span>Next payment:</span> {{ m.nextInstallmentDate }}
                                </div>
                            </div>
                        </span>
                    </td>

                    <td class="px-6 py-4 border-b border-gray-200 whitespace-nowrap">
                        <span class="inline-flex text-xs font-semibold leading-5">
                            {{ m.frequencyName }}</span>
                    </td>

                    <td class="px-6 py-4 text-sm leading-5 font-bold border-b border-gray-200 whitespace-nowrap">
                        {{ formatMoney(m.installmentAmountPerPeriod) }}
                    </td>

                    <td class="px-6 py-4 text-sm font-medium leading-5 text-right border-b border-gray-200 whitespace-nowrap">
                        {{ m.paidCount }}
                    </td>

                    <td class="px-6 py-4 text-sm font-medium leading-5 text-right border-b border-gray-200 whitespace-nowrap text-gray-500 ">
                        <div class="text-gray-500 flex flex-row space-x-2 h-fit">
                            <RouterLink :to="{ name: 'new-recurring-payment' }" class="cursor-pointer rounded-full bg-emerald-100 p-2 h-fit">
                                <GrEdit size="20"/>
                            </RouterLink>
                            <div @click="store.deleteRecurringPayment(m.id)" class="cursor-pointer rounded-full bg-rose-200 p-2 h-fit">
                                <GrTrash size="20"/>
                            </div>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>