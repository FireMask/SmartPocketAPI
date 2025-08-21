<script setup>
import { ref, computed } from 'vue';
import { formatShowDate, formatMoney, formatShowMothYear } from '../../helpers';
import { AlignBy } from '../../enums'
import { useReportsStore } from '@/stores/reports';

const store = useReportsStore()
const montsBefore = ref(-2);
const montsAhead = ref(4);
const selected = ref([]);
const loading = ref(false);

const alignBy = ref(AlignBy.START_DATE);

const handleMonthsChange = async () => {
	loading.value = true;
	store.filters.monthsAhead = montsAhead.value;
	store.filters.monthsBefore = montsBefore.value;
	await store.getProjections();
	loading.value = false;
};

const handleAlignBy = () => {
	loading.value = true;
	store.alignProjectionItems(alignBy.value);
	loading.value = false;
};

const handleCheckedChange = (projection) => {
	if (projection.checked)
		selected.value.push(projection)
	else{
		var index = selected.value.indexOf(projection);
		if (index > -1) {
			selected.value.splice(index, 1);
		}
	}
};

const resetSelection = () => {
	selected.value = []
	store.resetProjectionsCheck();
}

const selectedSum = computed(() => {
	return selected.value ? selected.value?.reduce((sum, projection) => sum + projection.amount, 0) : '0'
});

</script>

<template>
	<div class="overflow-auto h-fit">
		<div class="flex justify-between">
			<div class="flex justify-between">
				<a-form-item class="w-fit mb-4 ps-2 mt-1" label="Sort by: " name="dates">
					<a-radio-group v-model:value="alignBy" @change="handleAlignBy">
						<a-radio-button :value="AlignBy.START_DATE">
						Start Period
						</a-radio-button>
						<a-radio-button :value="AlignBy.END_DATE">
						End Period
						</a-radio-button>
						<a-radio-button :value="AlignBy.DUE_DATE">
						Due Date
						</a-radio-button>
					</a-radio-group>
				</a-form-item>
				<a-form-item class="w-fit mb-4 ps-4 mt-1" label="Moths before: " name="dates">
					<a-input-number v-model:value="montsBefore" :min="-12" :max="12" @change="handleMonthsChange"/>
				</a-form-item>
				<a-form-item class="w-fit mb-4 ps-4 mt-1" label="Moths ahead: " name="dates">
					<a-input-number v-model:value="montsAhead" :min="-12" :max="12" @change="handleMonthsChange"/>
				</a-form-item>
			</div>
			
			<div class="flex flex-col">
				<span>Starting projection at: {{ formatShowMothYear(store.earliestDateProjection) }}</span>
			</div>
			
			<div class="flex flex-col">
				<span>Sum selected: {{ formatMoney(selectedSum) }}</span>
				<a-button type="link" class="p-0" @click='resetSelection'>Reset selection</a-button>
			</div>
		</div>
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
			<div v-for="paymentMethod in store.paymentMethodsProjection" class="items-center">
				<p class="font-semibold text-xl -mt-9 fixed w-fit">{{ paymentMethod.name }}</p>
				<div class="flex mt-10">
					<div v-for="projection in paymentMethod.periods">
						<div v-if="projection.empty" class="min-w-56">

						</div>
						<div v-else class="h-auto px-4 py-2 shadow-lg bg-slate-50 mb-4 text-center min-w-56">
							<!-- Top Section -->
							<div class="text-sm font-semibold">
								<p>{{ formatShowDate(projection.startDate) }}</p>
								<p>to <span>{{ formatShowDate(projection.endDate) }}</span></p>
							</div>

							<!-- Bottom Section -->
							<div class="border-t border-gray-300 pt-2 text-gray-600 flex flex-col gap-1 text-center">
								<a-popover title="Movements" trigger="click">
									<template #content class="m-48">
										<div class="pr-2 max-h-40 overflow-auto">
											<p :class="movement.pending ? 'text-neutral-400' : 'text-neutral-900'"
												v-for="movement in projection.movements">
												{{ formatShowDate(movement.movementDate) }} - {{ movement.category }} {{
													movement.description }} -
												<span class="font-semibold">{{ formatMoney(movement.amount) }}</span>
											</p>
										</div>
									</template>
									<div class="font-bold text-emerald-600">{{ formatMoney(projection.amount) }}</div>
								</a-popover>
								<div class="font-semibold text-sm flex justify-between">
									<a-checkbox v-model:checked="projection.checked"
										@change="handleCheckedChange(projection)"></a-checkbox>
									<span>Due Date: <span class="font-normal">{{ formatShowDate(projection.dueDate,
										paymentMethod.name) }}</span></span>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>