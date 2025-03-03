<script setup>
    import { RouterLink, RouterView } from 'vue-router'
    const authRoutes = [
        { name: 'register', text: 'Sing Up' },
        { name: 'login', text: 'Log In' },
    ]
</script>

<template>
  <div class="flex">
    <div class="md:w-screen">
        <div class="mx-auto lg:w-1/2 ">
            <div class="flex flex-col items-center lg:flex-row lg: justify-between my-16">
                <RouterLink
                    v-for="authRoute in authRoutes"
                    class="uppercase font-bold text-gray-800"
                    :to="{name: authRoute.name}"
                    >
                    {{ authRoute.text }}
                </RouterLink>
            </div>
            <RouterView class="h-5/6" />
        </div>
    </div>
  </div>

</template>


