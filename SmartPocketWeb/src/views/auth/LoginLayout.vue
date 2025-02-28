<script setup>
    import { ref, inject } from 'vue';
    import { reset } from '@formkit/vue';
    import { useRouter } from 'vue-router';
    import AuthAPI from '../../api/AuthAPI'

    const toast = inject('toast')
    const router = useRouter()
    const email = ref('')
    const password = ref('')

    const handleSubmit = async (formData) => {
        try {
            const {data: {success, message, data: {token}}} = await AuthAPI.login(formData);

            if(success)
            {
                reset('loginForm')
                localStorage.setItem('AUTH_TOKEN', token)
                router.push({name:'dashboard'});
            }
            else 
            {
                toast.open({ message: message, type: 'error' });
            }
        } 
        catch (error)
        {
            console.log(error);
            toast.open({ message: error.response.data.message, type: 'error' })
        }
    }
</script>

<template>
    <div class="flex items-center justify-center h-auto px-6 bg-gray-200">
    <div class="w-full max-w-sm p-6 bg-white rounded-md shadow-md">
      <div class="flex items-center justify-center">
        <span class="text-2xl font-semibold text-gray-700">Smart Pocket</span>
        </div>
        <p class=" text-gray-800 text-center my-5">Enter in your Smart Pocket account.</p>

        <FormKit
            id="loginForm"
            type="form"
            :actions="false"
            incomplete-message="Error sending the data, please, see the notifications."
            @submit="handleSubmit"
        >
        <label class="block">
            <FormKit
                type="email"
                label="Email"
                name="email"
                v-model="email"
                placeholder="Your email"
                validation="required|email"
                :validation-messages="{
                    required: 'The email is required.',
                    email: 'Invalid email.'
                }"
            />
        </label>

        <label class="block mt-3">
            <FormKit
                type="password"
                label="Password"
                name="password"
                v-model="password"
                placeholder="Your password"
                validation="required"
                :validation-messages="{
                    required: 'The password is required.'
                }"
            />
        </label>

        <div class="flex items-center justify-between mt-4">
          <div>
            <label class="inline-flex items-center">
              <input type="checkbox" class="text-indigo-600 border-gray-200 rounded-md focus:border-indigo-600 focus:ring focus:ring-opacity-40 focus:ring-indigo-500">
              <span class="mx-2 text-sm text-gray-600">Remember me</span>
            </label>
          </div>

          <div>
            <a
              class="block text-sm text-indigo-700 fontme hover:underline"
              href="#"
            >Forgot your password?</a>
          </div>
        </div>

        <div class="mt-6">
          <button
            type="submit"
            class="w-full px-4 py-2 text-sm text-center text-white bg-indigo-600 rounded-md focus:outline-none hover:bg-indigo-500"
          >
            Login
          </button>
        </div>
        </FormKit>
    </div>
  </div>
</template>