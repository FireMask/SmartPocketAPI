<script setup>
import { ref, computed } from 'vue';
import { formatShowDate, formatMoney } from '../../helpers';
import { GrEdit, GrTrash } from 'vue-icons-plus/Gr';
import { useReportsStore } from '@/stores/reports';

const store = useReportsStore()
const monts = ref(6);
const filteredInfo = ref({});
const loading = ref(false);

const columns = computed(() => {
	return [
		{
			title: 'Start Date',
			key: 'startDate',
			name: 'startDate',
			dataIndex: 'startDate',
		},
		{
			title: 'End Date',
			key: 'endDate',
			name: 'endDate',
			dataIndex: 'endDate',
		},
		{
			title: 'amount',
			key: 'amount',
			name: 'amount',
			dataIndex: 'amount',
		},
	]
});

const handleMonthsChange = async () => {
	loading.value = true;
	await store.getProjections(monts.value);
	loading.value = false;
};

</script>

<template>
	<div class="overflow-auto h-fit">
		<a-form-item class="w-fit mb-4 ps-2 mt-1" label="Number of months to project" name="dates">
			<a-input-number v-model:value="monts" :min="1" :max="12" @change="handleMonthsChange" />
		</a-form-item>

		<div v-for="paymentMethod in store.paymentMethodsProjection" v-if="false">
			<p class="font-semibold text-xl my-1">{{ paymentMethod.name }}</p>
			<div class="flex mb-4">
				<a-card v-for="projection in paymentMethod.periods" size="small" class="w-48 min-w-48 text-center">
					<template #title>
						<p>{{ formatShowDate(projection.startDate) }} </p>
						<p>to {{ formatShowDate(projection.endDate) }}</p>
					</template>
					<p>{{ formatMoney(projection.amount) }}</p>
					<p> Due date {{ projection.amount }}</p>
				</a-card>
			</div>
		</div>
		<div class="overflow-x-auto">
			<div v-for="paymentMethod in store.paymentMethodsProjection" class="">
				<p class="font-semibold text-xl my-1">{{ paymentMethod.name }}</p>
				<div class="flex">
					<div v-for="projection in paymentMethod.periods"
						class="h-auto px-4 py-2 shadow-lg bg-slate-50 mb-4 text-center min-w-56">
						<!-- Top Section -->
						<div class="text-sm font-semibold">
							<p>{{ formatShowDate(projection.startDate) }}</p>
							<p>to <span>{{ formatShowDate(projection.endDate) }}</span></p>
						</div>

						<!-- Bottom Section -->
						<div class="border-t border-gray-300 pt-2 text-gray-600 flex flex-col gap-1 text-center ">
							<a-popover title="Movements">
								<template #content>
									<p :class="movement.pending ? 'text-neutral-400' : 'text-neutral-900'" v-for="movement in projection.movements">
										{{ formatShowDate(movement.movementDate) }} - {{ movement.category }} {{ movement.description }} - 
										<span class="font-semibold">{{ formatMoney(movement.amount) }}</span>
									</p>
								</template>
								<div class="font-bold text-emerald-600">{{ formatMoney(projection.amount) }}</div>
							</a-popover>
							<div class="font-semibold text-sm">
								Due Date:
								<span class="font-normal">{{ formatShowDate(projection.dueDate) }}</span>
							</div>

						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>