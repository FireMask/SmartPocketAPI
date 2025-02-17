<script setup>
    import { watch, ref, inject } from 'vue';
    import { reset } from '@formkit/vue';
    import { useRouter } from 'vue-router';
    import { useMovementsStore } from '../../stores/movements';
    import { useCardsStore } from '../../stores/cards';
    import { formatAPIDate, formatMoney, formatterDate } from '../../helpers';
    import { CgAddR } from 'vue-icons-plus/cg';
    import VueTailwindDatepicker from 'vue-tailwind-datepicker'
    import NewCategoyModal from '../../components/NewCategoyModal.vue';

    const toast = inject('toast')
    const router = useRouter()
    const store = useMovementsStore();
    const cardsStore = useCardsStore();
    const selectedMovementType = ref(null);
    const amountLabel = ref('Amount $');
    const openNewCategory = ref(false)

    const today = new Date();
    const formattedDate = new Intl.DateTimeFormat('en-GB', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
    }).format(today);

    const newMovement = ref({
        isInstallment: false,
        movementDate: formattedDate,
        description: "",
        amount: null,
        categoryId: null,
        paymentMethodId: null,
        movementTypeId: null,
        installmentCount: null,
        isInterestFreePayment: false,
        frecuencyId: null,
        creditCardPaymentId: null
    });

    const disableDate = (date) => {
        const today = new Date()
        return date > today
    }

    const handleSubmit = async (formData) => {
        newMovement.value.movementTypeId = selectedMovementType.value;
        newMovement.value.movementDate = formatAPIDate(newMovement.value.movementDate);
        newMovement.value.amount = formatMoney(newMovement.value.amount);
        if (!newMovement.value.installmentCount)
            newMovement.value.installmentCount = 0
        if (!newMovement.value.frecuencyId)
            newMovement.value.frecuencyId = 0

        const resp = await store.addMovement(newMovement.value);

        if (resp?.success) {
            reset('movementForm')
            router.push({ name: 'dashboard' });
        }
    }

    watch(selectedMovementType, (newValue) => {
        if (newMovement.value.isInstallment)
            amountLabel.value = 'Amount -$ (total amount of your purchase)'
        else {
            if (newValue === store.incomeTypeId)
                amountLabel.value = 'Amount +$'
            else
                amountLabel.value = 'Amount -$'
        }
    });

    watch(newMovement.value.movementDate, (newValue) => {
        console.log('newMovement.movementDate',newValue)
    });
</script>

<template>
    <div class="flex items-center justify-center h-auto px-6 bg-gray-200">
        <div class="w-full max-w-sm p-6 bg-white rounded-md shadow-md">
            <div class="flex items-center justify-center">
                <span class="text-2xl font-semibold text-gray-700">New Movement</span>
            </div>

            <FormKit id="movementForm" type="form" :actions="false"
                incomplete-message="Error sending the data, please, see the notifications." 
                @submit="">

                <FormKit
                    type="select"
                    label="Type of movement"
                    name="movementTypeId"
                    v-model="selectedMovementType"
                    placeholder="Select the type of movement"
                    :options="store.types"
                    validation="required"
                    :validation-messages="{
                        required: 'The type of movement is required.'
                    }"
                />

                <div v-if="selectedMovementType === store.creditCardPaymentTypeId">
                    <FormKit
                        type="select"
                        label="Credit card"
                        name="creditCardPaymentId"
                        v-model="newMovement.creditCardPaymentId"
                        placeholder="Select one"
                        :options="cardsStore.userCreditCards"
                    />
                </div>

                <div class="flex">
                    <div class="flex-1 w-full">
                        <FormKit
                            type="select"
                            label="Category"
                            name="categoryId"
                            v-model="newMovement.categoryId"
                            placeholder="Select a category"
                            
                            :options="store.categories"
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

                <NewCategoyModal v-model:open="openNewCategory" />

                <p class="block mb-1 font-bold text-lg text-gray-800">Transaction date</p>
                <div class="w-full mb-3">
                    <VueTailwindDatepicker
                        :disable-date="disableDate"
                        as-single
                        :formatter="formatterDate"
                        v-model="newMovement.movementDate"
                        validation="required"
                        :validation-messages="{
                            required: 'The type of movement is required.'
                        }"
                    />
                </div>

                <FormKit
                    type="date"
                    v-model="newMovement.movementDate"
                    :disable-date="disableDate"
                    :formatter="formatterDate"
                    validation="required"
                        :validation-messages="{
                            required: 'The transaction date is required.'
                        }"
                    />

                <FormKit
                    type="text"
                    label="Description [Optional]"
                    name="description"
                    placeholder="Describe your movement."
                    v-model="newMovement.description"
                />

                <FormKit
                    type="text"
                    :label=amountLabel
                    name="amount"
                    placeholder="Amount of movement."
                    v-model="newMovement.amount"
                    validation="required|number|regex:/^\\d*\\.?\\d+$/"
                    :validation-messages="{
                        regex: 'The interest rate must be a valid number.',
                        number: 'The interest rate must have only numbers.',
                        required: 'The amount is required.'
                    }"
                />
                <FormKit
                    type="select"
                    label="Payment Method"
                    name="paymentMethodId"
                    placeholder="Select one"
                    :options="cardsStore.userPaymentMethods"
                    v-model="newMovement.paymentMethodId"
                    validation="required"
                    :validation-messages="{
                        required: 'The payment method is required.'
                    }"
                />

                <div v-if="selectedMovementType === store.purchaseTypeId">  <!--TODO: Solo mostrar si el método de pago es tarjeta de credito-->
                    <FormKit
                        type="checkbox"
                        label="Is installment?"
                        name="isInstallment"
                        v-model="newMovement.isInstallment"
                    />
                </div>

                <div v-if="selectedMovementType === store.purchaseTypeId && newMovement.isInstallment">
                    <div class="flex items-center space-x-2">
                        <div class="w-1/2">
                            <FormKit
                                type="select"
                                label="Frecuency"
                                name="frecuencyId"
                                placeholder="Select one"
                                v-model="newMovement.frecuencyId"
                                :options="store.frequencies"
                            />
                        </div>
                        <div class="w-1/2">
                            <FormKit
                                type="text"
                                label="Installment Count"
                                name="installmentCount"
                                placeholder="Quantity of installments."
                                v-model="newMovement.installmentCount"
                                validation="number"
                                :validation-messages="{
                                    number: 'Only numbers.'
                                }"
                            />
                        </div>
                    </div>
                    <FormKit
                        type="checkbox"
                        label="Is Interest free? [Optional]"
                        name="isInterestFreePayment"
                        v-model="newMovement.isInterestFreePayment"
                    />
                </div>


                <div class="mt-6">
                    <button type="button" @click=handleSubmit class="w-full px-4 py-2 text-sm text-center text-white bg-indigo-600 rounded-md focus:outline-none hover:bg-indigo-500">
                        Save
                    </button>
                </div>
            </FormKit>
        </div>
    </div>
</template>