<script setup>
</script>

<template>    
    <div class="flex justify-between items-center">
        <div >
            <h1 class="text-2xl lg:text-6xl font-black text-gray-800">Smart Pocket</h1>
            <h2 class="text-sm lg:text-xl text-gray-800 mt-1">Spend Smart, Save Big</h2>
        </div>
        <div class="flex flex-col items-center">
            <nav class="flex gap-2 items-center justify-end">
                <RouterLink :to="{ name: 'new-movement' }"
                    class="p-3 text-gray-200 uppercase text-xs font-black rounded-lg bg-teal-500">
                    New movement
                </RouterLink>
            </nav>
        </div>
    </div>
    <div class="grid grid-cols-1 gap-6 mb-6 mt-12 lg:grid-cols-3">
        <div class="w-full px-4 py-5 bg-gray-800 rounded-lg shadow">
            <div class="text-sm font-medium text-gray-500 truncate">
                Income of the month
            </div>
            <div class="mt-1 text-3xl font-semibold text-gray-900">
                12,00
            </div>
        </div>
        <div class="w-full px-4 py-5 bg-gray-800 rounded-lg shadow">
            <div class="text-sm font-medium text-gray-500 truncate">
                Spending of the month
            </div>
            <div class="mt-1 text-3xl font-semibold text-gray-900">
                $ 450k
            </div>
        </div>
        <div class="w-full px-4 py-5 bg-gray-800 rounded-lg shadow">
            <div class="text-sm font-medium text-gray-500 truncate">
                Number of movements
            </div>
            <div class="mt-1 text-3xl font-semibold text-gray-900">
                48
            </div>
        </div>
    </div>
</template>