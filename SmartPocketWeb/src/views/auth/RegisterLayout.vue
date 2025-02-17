<script setup>
    import { inject } from 'vue';
    import { reset } from '@formkit/vue';
    import { useRoute, useRouter } from 'vue-router';
    import AuthAPI from '../../api/AuthAPI'

    const toast = inject('toast')
    const route = useRoute()
    const router = useRouter()

    const handleSubmit = async ({password_confirm, ...formData}) => {
        try {
            const {data : {success, message}} = await AuthAPI.register(formData);
            if(success)
            {
                toast.open({ message: 'Successful registration, please log in with your email and password.', type: 'success' });
                reset('registerForm')
                setTimeout(() => {
                    router.push({name:'login'});
                }, 500);
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
    <div class="w-full max-w-sm p-6 m-2 bg-white rounded-md shadow-md ">
      <div class="flex items-center justify-center">
        <span class="text-2xl font-semibold text-gray-700">Smart Pocket</span>
        </div>
        <p class=" text-gray-800 text-center my-5">Create your account in Smart Pocket.</p>

    <FormKit
        id="registerForm"
        type="form"
        :actions="false"
        @submit="handleSubmit"
    >
        <FormKit
            type="text"
            label="Name"
            name="name"
            placeholder="Your name"
            validation="required|length:3"
            :validation-messages="{
                required: 'The name is required.',
                length: 'The name is too short.'
            }"
        />

        <FormKit
        type="email"
        label="Email"
        name="email"
        placeholder="Your email"
        validation="required|email"
        :validation-messages="{
                required: 'The email is required.',
                email: 'Invalid email.'
            }"
        />

        <FormKit
            type="text"
            label="Phone (Optional)"
            name="phone"
            placeholder="Your phone number - 10 digits."
            validation="length:10,10|number"
            :validation-messages="{
                length: 'The phone number must have 10 digits.',
                number: 'The phone number must have only numbers.'
            }"
        />
        
        <FormKit
            type="password"
            label="Password"
            name="password"
            placeholder="User password - Min 8 characters."
            validation="required|length:8"
            :validation-messages="{
                required: 'The password is required.',
                length: 'Password must contain at least 8 characters.'
            }"
        />

        <FormKit
            type="password"
            label="Confirm password"
            name="password_confirm"
            placeholder="Repeat the password"
            validation="required|confirm"
            :validation-messages="{
                required: 'Confirming the password is mandatory.',
                confirm: 'Passwords are not the same.'
            }"
        />

        <div class="my-5">
          <button
            type="submit"
            class="w-full px-4 py-2 text-sm text-center text-white bg-indigo-600 rounded-md focus:outline-none hover:bg-indigo-500"
          >
          Create new Smart Pocket user
          </button>
        </div>
    </FormKit>
    </div>
    </div>
</template>