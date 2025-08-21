<script setup>
    import { ref, onMounted, computed } from 'vue';
    import { reset, FormKitSchema } from '@formkit/vue';
    import { useRouter, useRoute } from 'vue-router';
    import { useMovementsStore } from '../../stores/movements';
    import { useCardsStore } from '../../stores/cards';
    import { useRecurringPaymentsStore } from '@/stores/recurringPayments';
    import NewCategoyModal from '../../components/NewCategoyModal.vue';
    import TicketLayout from '@/components/TicketLayout.vue';
    import PageHeader from '@/components/PageHeader.vue';

    const router = useRouter()
    const route = useRoute()
    const store = useMovementsStore();
    const cardsStore = useCardsStore();
    const recurringStore = useRecurringPaymentsStore()
    const openNewCategory = ref(false)
    const today = new Date();
    const isEditing = ref(false);

    const headerButtons = [
        { name: 'movements', text: 'Movements' },
        { name: 'dashboard', text: 'Dashboard' },
    ]

    const cashId = 1
    
    const data = ref({
        isEditing: false,

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

    onMounted(async() => {
        if (route.params?.id) {
            isEditing.value = true;
            const {recurringPaymentId, recurringPayment, ...movement} = store.movementToUpdate.value;
            
            data.value = {
                ...data.value, 
                ...movement, 
                isEditing: true,
                isInstallment: recurringPaymentId !== null,
                frequencyId: recurringPayment?.frequencyId,
                installmentCount: recurringPayment?.installmentCount,
                isInterestFreePayment: recurringPayment?.isInterestFreePayment,
            }
        }

    });

    const schema = computed(() => [
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
            $el: 'div',
            if: "store.topCategories",
            children: [
                {
                    $el: 'label',
                    attrs: { class: 'block mb-2 font-semibold' },
                    children: 'Quick select top categories:'
                },
                {
                    $el: 'div',
                    attrs: { class: 'flex gap-2 mb-4' },
                    children: store.topCategories.map(category => ({
                        $el: 'button',
                        attrs: {
                            type: 'button',
                            class: 'w-full px-10 py-4 text-lg bg-gray-400 rounded hover:bg-gray-300',
                            style: 'flex: 1 1 0;',
                            onClick: () => {
                                data.value.categoryId = category.id;
                            }
                        },
                        children: category.label
                    }))
                }
            ]
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
                        '$classes.prefix + " px-1 py-1 stretch flex items-center bg-gray-100 mr-1 rounded"',
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
            disabled: isEditing.value,
            if: "$isEditing === false && $movementTypeId == $purchaseTypeId", //TODO: Solo mostrar si el método de pago es tarjeta de credito
        },
        {
            $el: 'div',
            name: 'group',
            if: "$isEditing === false && $movementTypeId == $purchaseTypeId && $isInstallment",
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
            if: "$isEditing === false && $movementTypeId == $purchaseTypeId && $isInstallment",
            placeholder: "Select one.",
            options: recurringStore.frequencies,
            validation: "required",
            validationMessages: { required: 'The installment frequency is required.', },
            disabled: isEditing.value,
        },
        {
            $formkit: "text",
            name: "installmentCount",
            label: "Installment count",
            if: "$isEditing === false && $movementTypeId == $purchaseTypeId && $isInstallment",
            placeholder: "Quantity of installments.",
            validation: "required|number",
            validationMessages: { 
                required: 'The installment count is required.', 
                number: 'Only numbers.'
            },
            disabled: isEditing.value,
        },
        {
            $formkit: "checkbox",
            name: "isInterestFreePayment",
            label: "Is Interest free? [Optional]",
            if: "$isEditing === false && $movementTypeId == $purchaseTypeId && $isInstallment",
            disabled: isEditing.value,
        },
        {
            $formkit: "submit",
            label: "Save"
        },
    ]);

    const handleSubmit = async (formData) => {
        if (!formData.isInstallment){
            formData.installmentCount = 0
            formData.frequencyId = 0
        }

        let resp;
        if (isEditing.value) 
            resp = await store.updateMovement(formData);
        else
            resp = await store.addMovement(formData);

        if (resp?.success) {
            reset('movementForm')
            router.go(-1)
        }
    }

    const type = computed(() => {
      const selected = store.types
        ? store.types.find((option) => option.id === data.value.movementTypeId)
        : null;
      return selected ? selected.label : ''; 
    });

    const category = computed(() => {
      const selected = store.categories
        ? store.categories.find((option) => option.id === data.value.categoryId)
        : null;
      return selected ? selected.label : ''; 
    });

    const payment = computed(() => {
        if(data.value.paymentMethodId === cashId)
        return "";
        const selected = cardsStore.userPaymentMethods
        ? cardsStore.userPaymentMethods.find((option) => option.id === data.value.paymentMethodId)
        : null;
        return selected ? selected.label : ''; 
    });

    const cash = computed(() => {
        return (data.value.paymentMethodId === cashId) ? "Cash" : null;
    });

    const frequency = computed(() => {
      const selected = recurringStore.frequencies
        ? recurringStore.frequencies.find((option) => option.id === data.value.frequencyId)
        : null;
      return selected ? selected.label : ''; 
    });
</script>

<template>
    <PageHeader title="New movement" :buttons="headerButtons"/>

    <div class="mt-4 flex flex-wrap space-x-0 md:space-x-14 space-y-14 items-start">
        <div class="p-6 bg-white rounded-md shadow-md w-full max-w-md flex-shrink-0">
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

        <TicketLayout :data="data" :category="category" :type="type" :payment="payment" :cash="cash" :frequency="frequency" class="w-full max-w-sm" />
    </div>
    <NewCategoyModal v-model:open="openNewCategory" />
</template>