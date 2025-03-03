<script setup>
    import { ref, watch } from 'vue';
    import { useRouter } from 'vue-router';
    import { reset } from '@formkit/vue';
    import { useCardsStore } from '../../stores/cards';
    import { FormKitSchema } from '@formkit/vue';
    import PageHeader from '@/components/PageHeader.vue';

    const store = useCardsStore();
    const router = useRouter()

    const headerButtons = [
        { name: 'cards', text: 'Cards' },
    ]

    const schema = [
        {
            $formkit: "text",
                    name: "name",
                    label: "Alias",
                    placeholder: "Alias for this card.",
                    validation: "required",
                    validationMessages: { required: 'The alias is required.', },
        },

        {
            $formkit: "radio",
            name: "paymentMethodTypeId",
            label: "Type of card",
            options: store.typesToAdd,
            validation: "required",
            validationMessages: { required: 'Type of card is required.', }
        },
        {
            $formkit: "text",
            name: "bank",
            label: "Bank [optional]",
            placeholder: "Bank of this card [optional].",
        },
        {
            $formkit: "text",
            name: "dueDate",
            label: "Due day",
            if: "$isCreditCard",
            placeholder: "Example: on the 12th of each month, enter 12.",
            validation: "required|length:1,2|number",
            validationMessages: {
                required: 'Due day is required.',
                length: 'Please enter a valid day of the month.',
                number: 'The due day must have only numbers.'
            }
        },
        {
            $formkit: "text",
            name: "transactionDate",
            label: "Transaction day",
            if: "$isCreditCard",
            placeholder: "Example: on the 27th of each month, enter 27.",
            validation: "required|length:1,2|number",
            validationMessages: {
                required: 'Transaction day is required.',
                length: 'Please enter a valid day of the month.',
                number: 'The transaction day must have only numbers.'
            }
        },
        {
            $formkit: "text",
            name: "defaultInterestRate",
            label: "Interest rate % [optional]",
            if: "$isCreditCard",
            placeholder: "Example: 2.4%, enter 2.4.",
            validation: "number|regex:/^\\d*\\.?\\d+$/|length:1,5",
            validationMessages: {
                regex: 'The interest rate must be a valid number.',
                number: 'The interest rate must have only numbers.',
                length: 'The interest rate must be between 1 and 5 characters long.'
            }
        },
        {
            $formkit: "submit",
            label: "Save"
        },
    ];

    const data = ref({
        name: null,
        paymentMethodTypeId: null,
        bank: '',
        dueDate: null,
        transactionDate: null,
        defaultInterestRate: null,
        isCreditCard: false
    });

    const handleSubmit = async (formData) => {
        if(!formData.dueDate)
            formData.dueDate = 0
        if(!formData.transactionDate)
            formData.transactionDate = 0
        if(!formData.defaultInterestRate)
            formData.defaultInterestRate = 0

        const response = await store.addCard(formData)

        if (response.success) {
            reset('cardForm')
            router.push({ name: 'cards' });
        }
    }

    watch(data, (newValue) => {
        data.value.isCreditCard = newValue.paymentMethodTypeId === store.getCreditCardId
    });

</script>

<template>
    <PageHeader title="New card" :buttons="headerButtons"/>

    <div class="mt-4">
        <div class="p-6 bg-white rounded-md shadow-md max-w-md">
            <FormKit 
                id="cardForm"
                type="form" 
                v-model="data" 
                :actions="false"
                @submit="handleSubmit"
                incomplete-message="Error sending the data, please, see the notifications.">
                <FormKitSchema :schema="schema" :data="data" />
            </FormKit>
        </div>
    </div>
</template>
