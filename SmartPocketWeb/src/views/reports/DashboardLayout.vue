<script setup>
	import { ref } from 'vue'
	import { useMovementsStore } from '../../stores/movements';
	import { formatShowDate, formatMoney } from '../../helpers';
	import { AiFillContainer, AiOutlineMinusCircle, AiOutlinePlusCircle } from 'vue-icons-plus/ai';
	import { GiWallet } from 'vue-icons-plus/gi';
	import { LuCopyCheck } from 'vue-icons-plus/lu';
	import { HiOutlineCursorClick } from 'vue-icons-plus/hi';
	import { useRouter } from 'vue-router';
	import ExpectedPaymentItem from '../../components/ExpectedPaymentItem.vue';
	import PageHeader from '@/components/PageHeader.vue';

	const headerButtons = [
		{ name: 'new-movement', text: 'New Movement' }
	];

	const router = useRouter()

	const store = useMovementsStore();
	store.reload();
</script>

<template>
	<div class="flex flex-col h-auto">

		<PageHeader title="Dashboard" :buttons="headerButtons" />

		<main class="">
			<section class="h-auto mt-4">
				<div class="flex flex-wrap -mx-4 ">

					<div class="px-3 mb-5 flex-grow">
						<div class="flex items-center px-3 py-4 bg-white rounded-md shadow-sm">
							<div class="p-3 bg-indigo-600 text-white bg-opacity-75 rounded-full">
								<GiWallet />
							</div>

							<div class="mx-5">
								<h4 class="text-2xl font-semibold text-gray-700">
									{{ formatMoney(store.monthIncome) }}
								</h4>
								<div class="text-gray-500">
									Income this month
								</div>
							</div>
						</div>
					</div>

					<div class="px-3 mb-5 flex-grow">
						<div class="flex items-center px-3 py-4 bg-white rounded-md shadow-sm">
							<div class="p-3 bg-blue-600 bg-opacity-75 rounded-full">
								<svg class="w-8 h-8 text-white" viewBox="0 0 28 28" fill="none"
									xmlns="http://www.w3.org/2000/svg">
									<path
										d="M4.19999 1.4C3.4268 1.4 2.79999 2.02681 2.79999 2.8C2.79999 3.57319 3.4268 4.2 4.19999 4.2H5.9069L6.33468 5.91114C6.33917 5.93092 6.34409 5.95055 6.34941 5.97001L8.24953 13.5705L6.99992 14.8201C5.23602 16.584 6.48528 19.6 8.97981 19.6H21C21.7731 19.6 22.4 18.9732 22.4 18.2C22.4 17.4268 21.7731 16.8 21 16.8H8.97983L10.3798 15.4H19.6C20.1303 15.4 20.615 15.1004 20.8521 14.6261L25.0521 6.22609C25.2691 5.79212 25.246 5.27673 24.991 4.86398C24.7357 4.45123 24.2852 4.2 23.8 4.2H8.79308L8.35818 2.46044C8.20238 1.83722 7.64241 1.4 6.99999 1.4H4.19999Z"
										fill="currentColor" />
									<path
										d="M22.4 23.1C22.4 24.2598 21.4598 25.2 20.3 25.2C19.1403 25.2 18.2 24.2598 18.2 23.1C18.2 21.9402 19.1403 21 20.3 21C21.4598 21 22.4 21.9402 22.4 23.1Z"
										fill="currentColor" />
									<path
										d="M9.1 25.2C10.2598 25.2 11.2 24.2598 11.2 23.1C11.2 21.9402 10.2598 21 9.1 21C7.9402 21 7 21.9402 7 23.1C7 24.2598 7.9402 25.2 9.1 25.2Z"
										fill="currentColor" />
								</svg>
							</div>

							<div class="mx-5">
								<h4 class="text-2xl font-semibold text-gray-700">
									{{ formatMoney(store.monthSpent) }}
								</h4>
								<div class="text-gray-500">
									Spend this month
								</div>
							</div>
						</div>
					</div>

					<div class="px-3 mb-5 flex-grow">
						<div class="flex items-center px-3 py-4 bg-white rounded-md shadow-sm">
							<div class="p-3 bg-pink-600 bg-opacity-75 text-white rounded-full">
								<AiFillContainer />
							</div>

							<div class="mx-5 w-full">
								<h4 class="text-2xl font-semibold text-gray-700">
									{{ store.monthMovementsCount }}
								</h4>
								<div class="text-gray-500">
									Movements count
								</div>
							</div>
						</div>
					</div>

					<div class="px-3 mb-5 flex-grow">
						<div class="flex items-center px-3 py-4 bg-white rounded-md shadow-sm">
							<div class="p-3 bg-amber-400 bg-opacity-75 text-white rounded-full">
								<LuCopyCheck />
							</div>

							<div class="mx-5 w-full">
								<div class="flex justify-between my-auto">
									<h4 class="text-2xl font-semibold text-gray-700">
										{{ store.pendingRecurringMovementsCount }}
									</h4>
									<RouterLink :to="{ name: 'pending' }"
										class="flex border p-1 px-3 rounded-2xl border-amber-400">
										<div class="w-5 h-5 rounded-full text-white bg-amber-400 flex items-center">
											<HiOutlineCursorClick />
										</div>
										<span class="text-gray-700 text-sm ms-2">Add</span>
									</RouterLink>
								</div>
								<div class="text-gray-500">
									Pending movements
								</div>
							</div>
						</div>
					</div>
				</div>
			</section>

			<section
				class="lg:h-[calc(80vh-8rem)] h-auto mt-4 lg:-mr-1 flex space-x-7 flex-col  xl:flex-row min-w-full space-y-4">
				<div class="flex flex-col w-full lg:w-3/4">
					<p class="font-medium text-xl text-gray-700 pb-4">Recent movements:</p>
					<div class="my-1 lg:pr-1 overflow-auto lg:scroll-pr-2 min-w-full">
						<div class="align-middle border-b border-gray-200 shadow sm:rounded-lg ">
							<table class="min-w-full">
								<thead>
									<tr>
										<th
											class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
											Movement
										</th>
										<th
											class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
											Payment Method
										</th>
										<th
											class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
											type
										</th>
										<th
											class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
											Amount
										</th>
									</tr>
								</thead>

								<tbody class="bg-white">
									<tr v-for="(m, index) in store.topMovements" :key="index">
										<td class="px-6 py-4 border-b border-gray-200 whitespace-nowrap">
											<div class="flex items-center">
												<div v-if="m.movementTypeId == store.incomeTypeId"
													class="w-5 h-5 rounded-full text-lime-600 bg-green-100 flex items-center">
													<AiOutlinePlusCircle />
												</div>
												<div v-else
													class="w-5 h-5 rounded-full text-red-600 bg-red-100 flex items-center">
													<AiOutlineMinusCircle />
												</div>

												<div class="ml-4">
													<div class="text-sm font-medium leading-5 text-gray-900">
														{{ m.category.name }}
													</div>
													<div class="text-sm leading-5 text-gray-500">
														{{ m.description }}
													</div>
												</div>
											</div>
											<div class="text-sm leading-5 text-gray-800"> {{
												formatShowDate(m.movementDate) }} </div>
										</td>

										<td class="px-6 py-4 border-b border-gray-200 whitespace-nowrap">
											<div class="text-sm leading-5 text-gray-900">
												{{ m.paymentMethod?.name }}
											</div>
											<div class="text-sm leading-5 text-gray-500" v-if="m.installmentNumber">
												<span v-if="m.recurringPayment?.installmentCount > 0"> Installment {{
													m.installmentNumber }} / {{ m.recurringPayment?.installmentCount }}
												</span>
												<span v-else>Payment # {{ m.installmentNumber }}</span>
											</div>
										</td>

										<td class="px-6 py-4 border-b border-gray-200 whitespace-nowrap">
											<div class="inline-flex text-xs font-semibold leading-5 ">{{
												m.movementType?.name }}</div>
											<div class="text-sm leading-5 text-gray-500" v-if="m.creditCardPaymentId">
												{{ m.creditCardPayment?.name }}
											</div>
										</td>

										<td
											class="px-6 py-4 text-sm leading-5 font-semibold border-b border-gray-200 whitespace-nowrap">
											{{ formatMoney(m.amount) }}
										</td>
									</tr>
									<tr class="cursor-pointer">
										<td class="px-6 py-4 border-b border-gray-200 whitespace-nowrap"
											@click="router.push({ name: 'movements' })">
											<div class="flex items-center">
												See more...
											</div>
										</td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
				
				<div class="flex flex-col w-full lg:w-1/4">
					<p class="font-medium text-xl text-gray-700 pb-4">Next expected payments:</p>
					<div class="my-1 pr-1 overflow-auto lg:scroll-pr-2 min-w-full">
						<div class="align-middle ">
							<ExpectedPaymentItem v-for="payment in store.summaryByPaymentMethods"
								:key="payment.cardName" :payment="payment" />
						</div>
					</div>
				</div>
			</section>

		</main>
	</div>
</template>