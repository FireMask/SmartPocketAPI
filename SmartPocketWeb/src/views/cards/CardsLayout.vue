<script setup>
	import { useCardsStore } from '../../stores/cards';
	import { formatShowDate } from '../../helpers';
	import CardItem from '../../components/CardItem.vue';
	import PageHeader from '@/components/PageHeader.vue';

	const store = useCardsStore();
	const today = new Date();

	const headerButtons = [
        { name: 'new-card', text: 'New card' },
    ]
</script>

<template>
	<PageHeader title="Cards" :buttons="headerButtons"/>

	<div class="flex flex-wrap gap-12 m-5">
		<CardItem v-for="paymentMethod in store.userCards" :key="paymentMethod._id" :paymentMethod="paymentMethod" />
	</div>
</template>