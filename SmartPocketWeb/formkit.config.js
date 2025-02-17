import { generateClasses } from '@formkit/themes'

const config = {
    config: {
        classes: generateClasses ({
            global: {
                wrapper: 'space-y-1 mb-2',
                message: 'bg-red-500 text-white text-center text-xs p-1 mb-2',
                label: 'block mb-1 font-bold text-lg text-gray-800',
                legend: 'block font-bold text-lg text-gray-800',
                input: 'w-full p-3 border border-gray-300 rounded-lg text-gray-700 placeholder-gray-400',
            },
            submit: {
                input: '$reset bg-teal-400 hover:bg-teal-500 rounded-lg text-gray-800 font-bold w-full p-3 mt-10'
            },
            radio: {
                wrapper: 'flex items-center space-x-4 mb-0 space-y-0',
                label: 'block mb-1 text-lg font-normal',
            },
            radioOption: {
                wrapper: 'flex items-center space-x-2' 
            },
            checkbox: {
                wrapper: 'flex items-center space-x-2 mb-0 space-y-0 flex-direction: row',
                label: 'block text-lg font-normal pb-2 mb-0',
            },
            date: {
                wrapper: 'collapse h-0',
                input: 'collapse h-0',
            },
        })
    }   
}

export default config;