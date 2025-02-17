<script setup>
    import { ref, watch } from 'vue';
    import { useCardsStore } from '../../stores/cards';
    import { reset } from '@formkit/vue';
    import { RouterLink, useRouter } from 'vue-router'
    
    const store = useCardsStore();
    const isCreditCard = ref(false);
    const selectedCardType = ref({});
    const router = useRouter()

    const handleSubmit = async (formData) => {
        formData.isCreditCard = isCreditCard.value
        const response = await store.addCard(formData)
        console.log(response)
        if(response.success)
        {
            reset('cardForm')
            router.push({name:'cards'});
        }
    }

    watch(selectedCardType, (newValue) => {
        handleCardTypeChange(newValue);
    });

    const handleCardTypeChange = (newValue) => {
        isCreditCard.value = (newValue === store.getCreditCardId);
    }
</script>

<template>    
    <div class="flex justify-between items-center">
        <div>
            <h1 class="text-2xl lg:text-6xl font-black text-gray-800">New card</h1>
        </div>
        <div class="flex flex-col items-center">
            <nav class="flex gap-2 items-center justify-end">
                <RouterLink :to="{ name: 'cards' }"
                    class="p-3 text-gray-200 uppercase text-xs font-black rounded-lg bg-teal-500">
                    Cards
                </RouterLink>
            </nav>
        </div>
    </div>

    <div class="mt-10 lg:w-1/2">
        <FormKit
            id="cardForm"
            type="form"
            :actions="false"
            incomplete-message="Error sending the data, please, see the notifications."
            @submit="handleSubmit"
        >
            <FormKit
                type="text"
                label="Alias"
                name="name"
                placeholder="Alias for this card."
                validation="required"
                :validation-messages="{
                        required: 'The alias is required.',
                    }"
            />
            <FormKit
                name="paymentMethodTypeId"
                type="radio"
                label="Type of card"
                v-model="selectedCardType"
                :options="store.typesToAdd"
                validation="required"
                :validation-messages="{
                    required: 'Type of card is required.',
                }"
            />

            <FormKit
                type="text"
                label="Bank [optional]"
                name="bank"
                placeholder="Bank of this card [optional]."
            />

            <div v-if="isCreditCard" class="flex items-center justify-between space-x-4">
                <div class="w-1/3">
                    <FormKit
                        type="text"
                        label="Due day [optional]"
                        name="dueDate"
                        placeholder="Example: on the 12th of each month, enter 12."
                        validation="length:1,2|number"
                        :validation-messages="{
                            length: 'Please enter a valid day of the month.',
                            number: 'The due day must have only numbers.'
                        }"
                    />
                </div>
                <div class="w-1/3">
                <FormKit
                    type="text"
                    label="Transaction day [optional]"
                    name="transactionDate"
                    placeholder="Example: on the 27th of each month, enter 27."
                    validation="length:1,2|number"
                    :validation-messages="{
                        length: 'Please enter a valid day of the month.',
                        number: 'The transaction day must have only numbers.'
                    }"
                />
                </div>
                <div class="w-1/3">
                <FormKit
                    type="text"
                    label="Interest rate % [optional]"
                    name="defaultInterestRate"
                    placeholder="Example: 2.4%, enter 2.4."
                    validation="number|regex:/^\\d*\\.?\\d+$/|length:1,5"
                    :validation-messages="{
                        regex: 'The interest rate must be a valid number.',
                        number: 'The interest rate must have only numbers.',
                        length: 'The interest rate must be between 1 and 5 characters long.'
                    }"
                />
                </div>
            </div>

            <FormKit type="submit">Save</FormKit>
        </FormKit>
    </div>
</template>
