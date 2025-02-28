<script setup>
    import { watch, ref, onMounted } from 'vue';
    import { reset, FormKitSchema } from '@formkit/vue';
    import { useRouter, useRoute } from 'vue-router';
    import { useMovementsStore } from '../../stores/movements';
    import { useCardsStore } from '../../stores/cards';
    import { useRecurringPaymentsStore } from '@/stores/recurringPayments';
    import { formatAPIDate, formatMoney, formatterDate } from '../../helpers';
    import NewCategoyModal from '../../components/NewCategoyModal.vue';

    const router = useRouter()
    const route = useRoute()
    const store = useMovementsStore();
    const cardsStore = useCardsStore();
    const recurringStore = useRecurringPaymentsStore()
    const openNewCategory = ref(false)
    const today = new Date();
    const isEditing = ref(false);

    onMounted(async() => {
        
        if (route.state?.movement) {
            isEditing.value = true;
            console.log(isEditing.value, movement);
        }
        await cardsStore.getCards();
    });
    
    const data = ref({
        isInstallment: false,
        movementDate: today.toISOString().split('T')[0],
        description: "",
        amount: null,
        categoryId: null,
        paymentMethodId: null,
        movementTypeId: null,
        installmentCount: null,
        isInterestFreePayment: false,
        frequencyId: null,
        creditCardPaymentId: null,
        creditCardPaymentTypeId: store.creditCardPaymentTypeId,
        purchaseTypeId: store.purchaseTypeId,
        handleAddCategory : () => {
            openNewCategory.value = true
        },
        attributesNMSI: {
            class: 'cursor-pointer py-0 px-2 text-md bg-gray-200 mr-2 rounded',
            onClick: (d) => {                
                data.value.installmentCount = d.srcElement.innerText.replace('MSI',''),
                data.value.frequencyId = 3,
                data.value.isInterestFreePayment = true
            },
        },
        attributesShortcutMSI: {
            class: 'flex items-end mb-3',
        },
    });

    const schema = [
        {
            $formkit: "select",
            name: "movementTypeId",
            label: "Type of movement",
            placeholder: "Select the type of movement.",
            options: store.types,
            validation: "required",
            validationMessages: { required: 'The type of movement is required.', },
        },
        {
            $formkit: "select",
            name: "creditCardPaymentId",
            label: "Credit card",
            if: "$movementTypeId == $creditCardPaymentTypeId",
            placeholder: "Select one.",
            options: cardsStore.userCreditCards,
            validation: "required",
            validationMessages: {
                required: 'Credit card is required. Please select one.',
            }
        },
        {
            $formkit: "select",
            name: "categoryId",
            label: "Category",
            placeholder: "Select a category.",
            options: store.categories,
            suffixIcon: 'add',
            onSuffixIconClick: '$handleAddCategory',
            validation: "required",
            validationMessages: { required: 'The type of category is required.', },
        },
        {
            $formkit: "date",
            name: "movementDate",
            label: "Transaction date",
            disableDates:"disableDate",
            validation: "required",
            validationMessages: {
                required: 'Transaction date is required.',
            }
        },
        {
            $formkit: "text",
            name: "description",
            label: "Description [Optional]",
            placeholder: "Describe your movement.",
            validation: "length:0,200",
            validationMessages: {
                length: 'Please enter a shorter description. Max length: 200.',
            }
        },
        {
            $formkit: "text",
            name: "amount",
            label: "Amount",
            placeholder: "Amount of movement.",
            validation: "required|number|regex:/^\\d*\\.?\\d+$/",
            validationMessages: {
                regex: 'The interest rate must be a valid number.',
                number: 'The interest rate must have only numbers.',
                required: 'The amount is required.'
            },
            __raw__sectionsSchema: {
                prefix: {
                $el: 'div',
                attrs: {
                    class:
                    '$classes.prefix + "p-2 stretch flex items-center bg-gray-100 mr-2 rounded"',
                },
                children: '$',
                },
            },
        },
        {
            $formkit: "select",
            name: "paymentMethodId",
            label: "Payment method",
            placeholder: "Select a payment method.",
            options: cardsStore.userPaymentMethods,
            validation: "required",
            validationMessages: { required: 'The payment method is required.', },
        },
        {
            $formkit: "checkbox",
            name: "isInstallment",
            label: "Is installment?",
            if: "$movementTypeId == $purchaseTypeId", //TODO: Solo mostrar si el método de pago es tarjeta de credito
        },
        {
            $el: 'div',
            name: 'group',
            if: "$movementTypeId == $purchaseTypeId && $isInstallment",
            bind: "$attributesShortcutMSI",
            children: [
            {
                $el: 'h1',
                bind: '$attributesNMSI',
                children: '3MSI',
            },
            {
                $el: 'h1',
                bind: "$attributesNMSI",
                children: '6MSI'
            },
            {
                $el: 'h1',
                bind: "$attributesNMSI",
                children: '9MSI'
            },
            {
                $el: 'h1',
                bind: "$attributesNMSI",
                children: '12MSI'
            },
            {
                $el: 'h1',
                bind: "$attributesNMSI",
                children: '18MSI'
            },
            ]
        },
        {
            $formkit: "select",
            name: "frequencyId",
            label: "Frequency",
            if: "$movementTypeId == $purchaseTypeId && $isInstallment",
            placeholder: "Select one.",
            options: recurringStore.frequencies,
            validation: "required",
            validationMessages: { required: 'The installment frequency is required.', },
        },
        {
            $formkit: "text",
            name: "installmentCount",
            label: "Installment count",
            if: "$movementTypeId == $purchaseTypeId && $isInstallment",
            placeholder: "Quantity of installments.",
            validation: "required|number",
            validationMessages: { 
                required: 'The installment count is required.', 
                number: 'Only numbers.'
            },
        },
        {
            $formkit: "checkbox",
            name: "isInterestFreePayment",
            label: "Is Interest free? [Optional]",
            if: "$movementTypeId == $purchaseTypeId && $isInstallment",
        },
        {
            $formkit: "submit",
            label: "Save"
        },
    ];

    const handleSubmit = async (formData) => {
        if (!formData.isInstallment){
            formData.installmentCount = 0
            formData.frequencyId = 0
        }

        const resp = await store.addMovement(formData);

        if (resp?.success) {
            reset('movementForm')
            router.push({ name: 'dashboard' });
        }
    }
</script>

<template>
        <header class="flex sticky justify-between mb-4 flex-col lg:flex-row">
        <h3 class="text-3xl font-medium text-gray-700">
            New Movement
        </h3>
        <div class="w-auto text-lg lg:text-xl mt-3 lg:mt-0">
            <p>
                <RouterLink :to="{ name: 'movements' }" class="text-indigo-600 hover:text-indigo-900 font-medium">
                    Movements
                </RouterLink>
            </p>
        </div>
    </header>

    <div class="mt-4">
        <div class="p-6 bg-white rounded-md shadow-md max-w-md">
            <FormKit 
                id="movementForm"
                type="form" 
                v-model="data" 
                :actions="false"
                @submit="handleSubmit"
                incomplete-message="Error sending the data, please, see the notifications.">
                <FormKitSchema :schema="schema" :data="data" />
            </FormKit>
        </div>
    </div>
    <NewCategoyModal v-model:open="openNewCategory" />
</template>