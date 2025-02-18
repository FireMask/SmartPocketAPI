<script setup>
    import { formatShowDate, formatMoney } from '../../helpers';
    import { useMovementsStore } from '@/stores/movements';

    const store = useMovementsStore()
</script>

<template>
    <div class="overflow-auto h-full">
        <table class="w-full h-max align-middle border-b border-gray-200 shadow sm:rounded-lg">
                <thead>
                  <tr>
                    <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
                      Date
                    </th>
                    <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
                      Movement
                    </th>
                    <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
                      Payment Method
                    </th>
                    <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
                      type
                    </th>
                    <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
                      Amount
                    </th>
                    <th class="px-6 py-3 border-b border-gray-200 bg-gray-50"></th>
                  </tr>
                </thead>

                <tbody class="bg-white">
                  <tr v-for="(m, recurringPaymentId) in store.userPendingMovements" :key="recurringPaymentId">
                    
                    <td class="px-6 py-4 border-b border-gray-200 whitespace-nowrap">
                      <div class="text-sm leading-5 text-gray-800"> {{ formatShowDate(m.movementDate) }} </div>
                    </td>

                    <td class="px-6 py-4 border-b border-gray-200 whitespace-nowrap">
                        <div class="text-sm font-medium leading-5 text-gray-900">
                        {{ m.categoryName }}
                        </div>
                        <div class="text-sm leading-5 text-gray-500">
                        {{ m.description }}
                        </div>
                    </td>

                    <td class="px-6 py-4 border-b border-gray-200 whitespace-nowrap">
                      <div class="text-sm leading-5 text-gray-900">
                        {{ m.paymentMethodName }}
                      </div>
                      <div class="text-sm leading-5 text-gray-500">
                        <div v-if="m.installmentCount > 0">
                            Installment {{ m.installmentNumber }} / {{ m.installmentCount }}
                        </div>
                        <div v-else>
                            Payment # {{ m.installmentNumber }}
                        </div>
                      </div>
                    </td>

                    <td class="px-6 py-4 border-b border-gray-200 whitespace-nowrap">
                      <div class="text-sm font-medium leading-5 text-gray-900">
                        {{ m.movementTypeName }}
                        </div>
                        <div class="text-sm leading-5 text-gray-500">
                        {{ m.frequencyName }}
                        </div>
                    </td>

                    <td class="px-6 py-4 text-sm leading-5 font-semibold border-b border-gray-200 whitespace-nowrap">
                      {{ formatMoney(m.amount) }}
                    </td>
                    <td class="px-6 py-4 text-sm leading-5 font-semibold border-b border-gray-200 whitespace-nowrap">
                        <button type="button" @click="store.AddPendingMovement(m)"
                            class="w-full px-4 py-2 text-sm text-center text-white bg-indigo-600 rounded-md focus:outline-none hover:bg-indigo-500">
                            Add
                        </button>
                    </td>
                  </tr>
                </tbody>
              </table>
    </div>
</template>