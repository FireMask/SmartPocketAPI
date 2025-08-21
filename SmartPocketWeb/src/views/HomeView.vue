<script setup>
	import { ref } from "vue";
	import { useUserStore } from '../stores/user'
	import { IpBill } from 'vue-icons-plus/ip';
	import { AiFillPieChart } from "vue-icons-plus/ai";
	import { TbTableShare, TbReportMoney } from "vue-icons-plus/tb";

	const user = useUserStore()

	const isOpen = ref(false);

</script>

<template>
	<div class="flex h-screen overflow-hidden">
		<div :class="isOpen ? 'w-1/6' : 'w-15'" class="flex flex-col h-screen p-3 dark:bg-gray-50 dark:text-gray-800">
			<div class="space-y-3">
				<div class="flex items-center justify-between">
					<button class="p-2" @click="isOpen = !isOpen">
						<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"
							class="w-5 h-5 fill-current dark:text-gray-800">
							<rect width="352" height="32" x="80" y="96" />
							<rect width="352" height="32" x="80" y="240" />
							<rect width="352" height="32" x="80" y="384" />
						</svg>
					</button>
					<h2 v-show="isOpen" class="text-black text-xl pr-5">Smart Pocket</h2>
				</div>
				<div class="flex-1">
					<ul class="pt-2 pb-4 space-y-1 text-sm">
						<li class="rounded-sm hover:bg-gray-100">
							<RouterLink :to="{ name: 'dashboard' }" class="flex items-center p-2 space-x-3 rounded-md">
								<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"
									class="w-5 h-5 fill-current dark:text-gray-600">
									<path
										d="M469.666,216.45,271.078,33.749a34,34,0,0,0-47.062.98L41.373,217.373,32,226.745V496H208V328h96V496H480V225.958ZM248.038,56.771c.282,0,.108.061-.013.18C247.9,56.832,247.756,56.771,248.038,56.771ZM448,464H336V328a32,32,0,0,0-32-32H208a32,32,0,0,0-32,32V464H64V240L248.038,57.356c.013-.012.014-.023.024-.035L448,240Z" />
								</svg>
								<span v-show="isOpen">Home</span>
							</RouterLink>
						</li>
						<li class="rounded-sm hover:bg-gray-100">
							<RouterLink :to="{ name: 'movements' }" class="flex items-center p-2 space-x-3 rounded-md">
								<div class="w-5 h-5 fill-current dark:text-gray-600 items-center ">
									<TbTableShare  size="19"/>
								</div>
								<span v-show="isOpen">Movements</span>
							</RouterLink>
						</li>
						<li class="rounded-sm hover:bg-gray-100">
							<RouterLink :to="{ name: 'recurring-payments' }" class="flex items-center p-2 space-x-3 rounded-md">
								<div class="w-5 h-5 fill-current dark:text-gray-600 items-center ">
									<IpBill/>
								</div> 
								<span v-show="isOpen">Payments</span>
							</RouterLink>
						</li>
						<li class="rounded-sm hover:bg-gray-100">
							<RouterLink :to="{ name: 'charts' }" class="flex items-center p-2 space-x-3 rounded-md">
								<div class="w-5 h-5 fill-current dark:text-gray-600 items-center ">
									<AiFillPieChart/>
								</div> 
								<span v-show="isOpen">Charts</span>
							</RouterLink>
						</li>
						<li class="rounded-sm hover:bg-gray-100">
							<RouterLink :to="{ name: 'payments-projection' }" class="flex items-center p-2 space-x-3 rounded-md">
								<div class="w-5 h-5 fill-current dark:text-gray-600 items-center ">
									<TbReportMoney/>
								</div> 
								<span v-show="isOpen">Reports</span>
							</RouterLink>
						</li>
						<li class="rounded-sm hover:bg-gray-100">
							<RouterLink :to="{ name: 'cards' }" class="flex items-center p-2 space-x-3 rounded-md">
								<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 47 47"
									class="w-5 h-5 fill-current dark:text-gray-600">
									<g id="SVGRepo_bgCarrier" stroke-width="0" />
									<g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round" />
									<g id="SVGRepo_iconCarrier">
										<g>
											<g id="Layer_1_90_">
												<g>
													<path
														d="M42.539,8.23L20.046,2.203c-0.524-0.14-1.062-0.211-1.601-0.211c-2.652,0-4.963,1.714-5.621,4.169L9.902,17.065h4.285 l1.162-4.335l25.992,6.965l-2.276,8.499c-0.188,0.697-0.977,1.16-1.807,1.09v4.14c0.062,0.002,0.123,0.013,0.184,0.013 c2.654,0,4.965-1.717,5.623-4.17l3.75-13.997C47.625,12.236,45.708,9.079,42.539,8.23z M42.814,14.197l-0.401,1.5L16.42,8.732 l0.402-1.499c0.174-0.649,0.841-1.102,1.623-1.102c0.177,0,0.354,0.024,0.529,0.07l22.493,6.027 C42.415,12.482,43.032,13.384,42.814,14.197z" />
													<path
														d="M29.236,19.135H5.951C2.67,19.135,0,21.689,0,24.827v14.489c0,3.139,2.67,5.691,5.951,5.691h23.287 c3.281,0,5.951-2.554,5.951-5.691V24.827C35.188,21.689,32.52,19.135,29.236,19.135z M31.048,39.316 c0,0.843-0.829,1.553-1.812,1.553H5.951c-0.981,0-1.811-0.71-1.811-1.553V24.827c0-0.841,0.83-1.552,1.811-1.552h23.287 c0.982,0,1.811,0.711,1.811,1.552L31.048,39.316L31.048,39.316z" />
													<path
														d="M11.772,36.212h-4.14c-0.572,0-1.035,0.463-1.035,1.034v1.035c0,0.572,0.463,1.035,1.035,1.035h4.14 c0.572,0,1.035-0.463,1.035-1.035v-1.035C12.808,36.675,12.344,36.212,11.772,36.212z" />
													<path
														d="M19.664,36.212h-4.14c-0.571,0-1.035,0.463-1.035,1.034v1.035c0,0.572,0.463,1.035,1.035,1.035h4.14 c0.571,0,1.035-0.463,1.035-1.035v-1.035C20.699,36.675,20.235,36.212,19.664,36.212z" />
													<path
														d="M27.555,36.212h-4.14c-0.571,0-1.035,0.463-1.035,1.034v1.035c0,0.572,0.463,1.035,1.035,1.035h4.14 c0.572,0,1.035-0.463,1.035-1.035v-1.035C28.59,36.675,28.127,36.212,27.555,36.212z" />
													<path
														d="M18.025,32.059h7.848c0.57,0,1.035-0.434,1.035-0.965v-3.865c0-0.53-0.465-0.963-1.035-0.963h-7.848 c-0.569,0-1.035,0.433-1.035,0.963v3.865C16.991,31.625,17.456,32.059,18.025,32.059z M19.061,28.781 c0-0.532,0.465-0.965,1.035-0.965h3.708c0.569,0,1.035,0.433,1.035,0.965v0.76c0,0.531-0.466,0.964-1.035,0.964h-3.708 c-0.57,0-1.035-0.433-1.035-0.964V28.781z" />
												</g>
											</g>
										</g>
									</g>
								</svg>
								<span v-show="isOpen">Cards</span>
							</RouterLink>
						</li>
						<li class="rounded-sm hover:bg-gray-100 cursor-pointer">
							<RouterLink :to="{ name: 'home' }" class="flex items-center p-2 space-x-3 rounded-md cursor-pointer">
								<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"
									class="w-5 h-5 fill-current dark:text-gray-600">
									<path
										d="M245.151,168a88,88,0,1,0,88,88A88.1,88.1,0,0,0,245.151,168Zm0,144a56,56,0,1,1,56-56A56.063,56.063,0,0,1,245.151,312Z" />
									<path
										d="M464.7,322.319l-31.77-26.153a193.081,193.081,0,0,0,0-80.332l31.77-26.153a19.941,19.941,0,0,0,4.606-25.439l-32.612-56.483a19.936,19.936,0,0,0-24.337-8.73l-38.561,14.447a192.038,192.038,0,0,0-69.54-40.192L297.49,32.713A19.936,19.936,0,0,0,277.762,16H212.54a19.937,19.937,0,0,0-19.728,16.712L186.05,73.284a192.03,192.03,0,0,0-69.54,40.192L77.945,99.027a19.937,19.937,0,0,0-24.334,8.731L21,164.245a19.94,19.94,0,0,0,4.61,25.438l31.767,26.151a193.081,193.081,0,0,0,0,80.332l-31.77,26.153A19.942,19.942,0,0,0,21,347.758l32.612,56.483a19.937,19.937,0,0,0,24.337,8.73l38.562-14.447a192.03,192.03,0,0,0,69.54,40.192l6.762,40.571A19.937,19.937,0,0,0,212.54,496h65.222a19.936,19.936,0,0,0,19.728-16.712l6.763-40.572a192.038,192.038,0,0,0,69.54-40.192l38.564,14.449a19.938,19.938,0,0,0,24.334-8.731L469.3,347.755A19.939,19.939,0,0,0,464.7,322.319Zm-50.636,57.12-48.109-18.024-7.285,7.334a159.955,159.955,0,0,1-72.625,41.973l-10,2.636L267.6,464h-44.89l-8.442-50.642-10-2.636a159.955,159.955,0,0,1-72.625-41.973l-7.285-7.334L76.241,379.439,53.8,340.562l39.629-32.624-2.7-9.973a160.9,160.9,0,0,1,0-83.93l2.7-9.972L53.8,171.439l22.446-38.878,48.109,18.024,7.285-7.334a159.955,159.955,0,0,1,72.625-41.973l10-2.636L222.706,48H267.6l8.442,50.642,10,2.636a159.955,159.955,0,0,1,72.625,41.973l7.285,7.334,48.109-18.024,22.447,38.877-39.629,32.625,2.7,9.972a160.9,160.9,0,0,1,0,83.93l-2.7,9.973,39.629,32.623Z" />
								</svg>
								<span v-show="isOpen">Settings</span>
							</RouterLink>
						</li>
						<li class="rounded-sm hover:bg-gray-100" @click="user.logout">
							<a class="flex items-center p-2 space-x-3 rounded-md">
								<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"
									class="w-5 h-5 fill-current dark:text-gray-600">
									<path
										d="M440,424V88H352V13.005L88,58.522V424H16v32h86.9L352,490.358V120h56V456h88V424ZM320,453.642,120,426.056V85.478L320,51Z" />
									<rect width="32" height="64" x="256" y="232" />
								</svg>
								<span v-show="isOpen">Logout</span>
							</a>
						</li>
					</ul>
				</div>
			</div>
			<div class="flex items-center p-1 mt-2 space-x-5">
				<img src="/img/User.png" alt="" :class="isOpen ? 'w-12 h-12' : 'w-6 h-6'"
					class="rounded-lg dark:bg-gray-500">
				<div v-show="isOpen">
					<h2 class="text-lg font-semibold">{{ user.getUserName }}</h2>
				</div>
			</div>
		</div>

		<div class="p-10 lg:h-screen w-full overflow-y-auto">
			<RouterView />
		</div>
	</div>
</template>