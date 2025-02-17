<script setup>
    import { RouterLink, RouterView } from 'vue-router'
    const authRoutes = [
        { name: 'register', text: 'Sing Up' },
        { name: 'login', text: 'Log In' },
    ]
</script>

<template>
  <div class="flex h-screen">
    <div class="md:w-screen h-screen">
        <div class="mx-auto lg:w-4/5 h-screen">
            <div class="flex flex-col items-center lg:flex-row lg: justify-between h-1/6">
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


