<script setup>
    import { inject } from 'vue'
    import { reset } from '@formkit/vue';
    import MovementsAPI from '../api/MovementsAPI';

    

    const open = defineModel('open');
    const toast = inject('toast')

    const handleSubmit = async (formData) => {
        try {
            const {data: {success, message}} = await MovementsAPI.createCategory(formData);
            
            if(success)
            {
                closeModal();
                toast.open({ message: message, type: 'success' });
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

    const closeModal = () => {
        reset('categoryForm');
        open.value = false;
    }
</script>

<template>
    <div :class="`modal ${!open && 'opacity-0 pointer-events-none'} z-50 fixed w-full h-full top-0 left-0 flex items-center justify-center`">
        <div class="absolute w-full h-full bg-gray-900 opacity-50 modal-overlay" @click=closeModal />

        <div class="z-50 w-11/12 mx-auto overflow-y-auto bg-white rounded shadow-lg modal-container md:max-w-md">

            <div class="px-6 py-4 text-left modal-content">
                <div class="flex items-center justify-between pb-3">
                    <p class="text-2xl font-bold">
                        New Category
                    </p>
                    <div class="z-50 cursor-pointer modal-close" @click=closeModal>
                        <svg class="text-black fill-current" xmlns="http://www.w3.org/2000/svg" width="18" height="18"
                            viewBox="0 0 18 18">
                            <path
                                d="M14.53 4.53l-1.06-1.06L9 7.94 4.53 3.47 3.47 4.53 7.94 9l-4.47 4.47 1.06 1.06L9 10.06l4.47 4.47 1.06-1.06L10.06 9z" />
                        </svg>
                    </div>
                </div>

                <!-- Body -->
                <FormKit 
                    id="categoryForm" 
                    type="form" 
                    :actions="false"
                    incomplete-message="Error sending the data, please, see the notifications." 
                    @submit="handleSubmit">
                    
                    <label class="block">
                        <FormKit type="text" label="Name" name="name" placeholder="Name for your category."
                            validation="required" 
                            :validation-messages="{
                                required: 'The name is required.'
                            }" />
                    </label>

                    <label class="block mt-3">
                        <FormKit type="text" label="Description" name="description" placeholder="It could help you in the movement registration." />
                    </label>
                    
                    <!-- Footer -->
                    <div class="flex justify-end pt-2">
                        <div
                            class="p-3 px-6 py-3 mr-2 text-indigo-500 bg-transparent rounded-lg hover:bg-gray-100 hover:text-indigo-400 focus:outline-none"
                            @click=closeModal>
                            Close
                    </div>
                        <button type="submit"
                            class="px-6 py-3 font-medium tracking-wide text-white bg-indigo-600 rounded-md hover:bg-indigo-500 focus:outline-none">
                            Save
                        </button>
                    </div>
                </FormKit>
            </div>
        </div>
    </div>
</template>

<style>
.modal {
    transition: opacity 0.25s ease;
}
</style>