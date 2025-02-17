<script setup>
    import { ref } from 'vue';
    import { reset } from '@formkit/vue';
    import { formatShowDate, formatAPIDate, formatMoney, formatterDate } from '../../helpers';
    import { useRecurringPaymentsStore } from '../../stores/recurringPayments';
    import { useMovementsStore } from '../../stores/movements';
    import { useCardsStore } from '../../stores/cards';
    import { CgAddR } from 'vue-icons-plus/cg';
    import { useRouter } from 'vue-router';
    import VueTailwindDatepicker from 'vue-tailwind-datepicker'
    import NewCategoyModal from '../../components/NewCategoyModal.vue';

    const router = useRouter()
    const store = useRecurringPaymentsStore();
    const movementsSttore = useMovementsStore();
    const cardsStore = useCardsStore();
    const today = new Date();
    const openNewCategory = ref(false)
    const formattedDate = new Intl.DateTimeFormat('en-GB', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
    }).format(today);

    const start = ref(formattedDate)

    const handleSubmit = async (formData) => {        
        formData.startDate = formatAPIDate(start.value);
        formData.isInterestFreePayment = false;
        formData.installmentCount = null;
        formData.movementTypeId = movementsSttore.paymentTypeId;
        formData.isActive = true;
        
        const resp = await store.addRecurringPayment(formData);

        console.log(resp)

        if (resp?.success) {
            reset('paymentForm')
            router.push({ name: 'dashboard' });
        }
    }
</script>

<template>
    <header class="flex sticky justify-between mb-4 flex-col lg:flex-row">
        <h3 class="text-3xl font-medium text-gray-700">
            New recurring payment
        </h3>
        <div class="w-auto text-lg lg:text-xl mt-3 lg:mt-0">
        <p class="font-medium text-gray-700">
            {{ formatShowDate(today) }}
        </p>
        <p>
            <RouterLink :to="{ name: 'dashboard' }" class="text-indigo-600 hover:text-indigo-900 font-medium">
            Dashboard
            </RouterLink>
        </p>
        <p>
            <RouterLink :to="{ name: 'new-movement' }" class="text-indigo-600 hover:text-indigo-900 font-medium">
            New Movement
            </RouterLink>
        </p>
        </div>
    </header >

    <div class="mt-4">
        <NewCategoyModal v-model:open="openNewCategory" />
        <div class="p-6 bg-white rounded-md shadow-md">
            <FormKit id="paymentForm" type="form" :actions="false"
                incomplete-message="Error sending the data, please, see the notifications." 
                @submit="handleSubmit">

                <div class="grid grid-cols-1 gap-6 mt-4 sm:grid-cols-2">
                    <FormKit
                        type="text"
                        label="Description"
                        name="description"
                        placeholder="Short description of your payment."
                        validation="required"
                            :validation-messages="{
                                required: 'The type of category is required.'
                            }"
                    />
                <div>
                    
                <FormKit
                    type="text"
                    label="Amount $"
                    name="installmentAmount"
                    placeholder="Amount of payment."
                    validation="required|number|regex:/^\\d*\\.?\\d+$/"
                    :validation-messages="{
                        regex: 'The interest rate must be a valid number.',
                        number: 'The interest rate must have only numbers.',
                        required: 'The amount is required.'
                    }"
                />
              </div>

              <div class="flex">
                    <div class="flex-1 w-full">
                        <FormKit
                            type="select"
                            label="Category"
                            name="categoryId"
                            placeholder="Select a category."
                            :options="movementsSttore.categories"
                            validation="required"
                            :validation-messages="{
                                required: 'The type of category is required.'
                            }"
                        />
                    </div>
                    <div class="text-indigo-600 hover:text-indigo-900 my-auto pt-5 ms-2 flex-none" @click="openNewCategory = true">
                        <CgAddR/>
                    </div>
                </div>

                

                <div>
                    <p class="block mb-1 font-bold text-lg text-gray-800">Start date</p>
                    <div class="w-full mb-3">
                        <VueTailwindDatepicker
                            as-single
                            :formatter="formatterDate"
                            v-model="start"
                        />
                    </div>
                    <FormKit
                        type="date"
                        v-model="start"
                        :formatter="formatterDate"
                        validation="required"
                            :validation-messages="{
                                required: 'The transaction date is required.'
                            }"
                        />
                </div>
                <FormKit
                    type="select"
                    label="Payment Method"
                    name="paymentMethodId"
                    placeholder="Select the payment method."
                    :options="cardsStore.userPaymentMethods"
                    validation="required"
                    :validation-messages="{
                        required: 'The payment method is required.'
                    }"
                />
                <FormKit
                    type="select"
                    label="Frequency"
                    name="frecuencyId"
                    placeholder="Select the payment frequency."
                    :options="store.frecuencies"
                    validation="required"
                    :validation-messages="{
                        required: 'The payment payment frequency is required.'
                    }"
                />
            </div>

            <div class="flex justify-end mt-4">
              <button
                class="px-4 py-2 text-gray-200 bg-gray-800 rounded-md hover:bg-gray-700 focus:outline-none focus:bg-gray-700"
              >
                Save
              </button>
            </div>
            </FormKit>
        </div>
      </div>
</template>