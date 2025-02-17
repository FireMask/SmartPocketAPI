import { ref, onMounted, computed, inject } from 'vue';
import { defineStore } from 'pinia';
import CardsAPI from '../api/CardsAPI';

export const useCardsStore = defineStore( 'cards', () => {

    const types = ref([])
    const typesToAdd = ref([])
    const userPaymentMethods = ref([]);
    const userCreditCards = ref([]);
    const toast = inject('toast')

    onMounted(async () => {
        try {
            await getTypes();
            await getCards();
        } catch (error) {
            console.log(error);
        }
    })

    const getTypes = async () => {
        const {data} = await CardsAPI.types()
        types.value = data.data.map(type => { return {label: type.name, value: type.id, id: type.id}})
        typesToAdd.value = types.value.filter(type => type.label != 'Cash');
    }

    const getCards = async () => {
        const {data} = await CardsAPI.all()
        userPaymentMethods.value = data.data.map(card => { card.label = card.name; card.value = card.id; return card})
        userCreditCards.value = userPaymentMethods.value.filter(type => type.isCreditCard);
    }

    const getCreditCardId = computed( () => {
            const creditCard = typesToAdd.value ? typesToAdd.value?.find(type => type.label === 'Credit Card') : {}
            return creditCard?.id
        }
    )

    async function addCard(cardData) {
        try {
            const { data } = await CardsAPI.create(cardData)
            
            if(data.success)
                await getCards();
            else 
                toast.open({ message: message, type: 'error' });
            return data;
        } catch (error)
        {
            console.log(error);
            toast.open({ message: error.response.data.message, type: 'error' })
        }
    }

    async function deleteCard(id) {
        if(confirm('¿Do you want to delete this card?')) {
            try {
                const { data } = await CardsAPI.delete(id)
                toast.open({
                    message: 'Deleted',
                    type: 'success'
                })

                await getCards();
            } catch (error)
            {
                console.log(error);
                toast.open({ message: error.response.data.message, type: 'error' })
            }
        }
    }

    async function updateCard(cardData) {
        try {
            const { data } = await CardsAPI.update(cardData)
            console.log("data", data);
            
            if(data.success)
                await getCards();
            else 
                toast.open({ message: message, type: 'error' });
            return data;
        } catch (error)
        {
            console.log(error);
            toast.open({ message: error.response.data.message, type: 'error' })
        }
    }

    return {
        types,
        typesToAdd,
        userPaymentMethods,
        userCreditCards,
        getCreditCardId,
        addCard,
        deleteCard,
        updateCard
    }
})