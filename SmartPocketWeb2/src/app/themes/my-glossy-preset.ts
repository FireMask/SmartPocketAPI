import { definePreset } from '@primeuix/themes';
import Lara from '@primeuix/themes/lara';

const MyGlossyPreset = definePreset(Lara, {
    semantic: {
        primary: {
            50: '{indigo.50}',
            100: '{indigo.100}',
            200: '{indigo.200}',
            300: '{indigo.300}',
            400: '{indigo.400}',
            500: '{indigo.500}',
            600: '{indigo.600}',
            700: '{indigo.700}',
            800: '{indigo.800}',
            900: '{indigo.900}',
            950: '{indigo.950}'
        },
        colorScheme: {
            light: {
                primary: {
                    color: '{zinc.950}',
                    inverseColor: '#ffffff',
                    hoverColor: '{zinc.900}',
                    activeColor: '{zinc.800}',
                },
                highlight: {
                    background: '{zinc.950}',
                    focusBackground: '{zinc.700}',
                    color: '#ffffff',
                    focusColor: '#ffffff'
                },
                formField: {
                    hoverBorderColor: '{primary.color}'
                },
                surface: {
                    0: '#ffffff',
                    50: '{zinc.50}',
                    100: '{zinc.100}',
                    200: '{zinc.200}',
                    300: '{zinc.300}',
                    400: '{zinc.400}',
                    500: '{zinc.500}',
                    600: '{zinc.600}',
                    700: '{zinc.700}',
                    800: '{zinc.800}',
                    900: '{zinc.900}',
                    950: '{zinc.950}'
                },
                
            },
            dark: {
                primary: {
                    color: '{zinc.50}',
                    inverseColor: '{zinc.950}',
                    hoverColor: '{zinc.100}',
                    activeColor: '{zinc.200}'
                },
                highlight: {
                    background: 'rgba(250, 250, 250, .16)',
                    focusBackground: 'rgba(250, 250, 250, .24)',
                    color: 'rgba(255,255,255,.87)',
                    focusColor: 'rgba(255,255,255,.87)'
                },
                formField: {
                    hoverBorderColor: '{primary.color}'
                },
                surface: {
                    0: '#ffffff',
                    50: '{slate.50}',
                    100: '{slate.100}',
                    200: '{slate.200}',
                    300: '{slate.300}',
                    400: '{slate.400}',
                    500: '{slate.500}',
                    600: '{slate.600}',
                    700: '{slate.700}',
                    800: '{slate.800}',
                    900: '{slate.900}',
                    950: '{slate.950}'
                },
            }
        }
    },
    components: {
        datatable: {
            colorScheme:{
                light:{
                    headerCell:{
                        ...Lara.semantic,
                        background: 'linear-gradient(to bottom, #ffffff, #f0f4f9)',
                        color: '#1e293b',
                        borderColor: '#d1d5db',
                    },
                    row:{
                        ...Lara.semantic,
                        background: 'linear-gradient(to left top, {gray.100}, {gray.200})',
                        color: '{gray.900}',
                        hoverBackground: 'linear-gradient(to left top, {blue.100}, {blue.50})',
                    },
                    bodyCell:{
                        ...Lara.semantic,
                        selectedBorderColor: '{primary.color}',
                    },
                    paginatorBottom:{
                        ...Lara.semantic,
                        borderColor: '{red.700}', 
                    }
                },
                dark:{
                    headerCell:{
                        ...Lara.semantic,
                        background: '{sky.950}',
                        color: '{gray.100}',
                        borderColor: '#d1d5db',
                    },
                    row:{
                        ...Lara.semantic,
                        background: 'linear-gradient(to left top, {gray.600}, {gray.700})',
                        color: '{gray.200}',
                        hoverBackground: 'linear-gradient(to left top, {blue.900}, {blue.950})',
                        hoverColor: '{gray.100}',
                        stripedBackground: 'linear-gradient(to left top, {gray.700}, {gray.800})',
                    },
                    bodyCell:{
                        ...Lara.semantic,
                        selectedBorderColor: '{primary.color}',
                    },
                    paginatorBottom:{
                        ...Lara.semantic,
                        borderColor: '{red.700}', 
                        borderWidth: '10px'
                    },
                    paginatorTop:{
                        ...Lara.semantic,
                        borderColor: '{red.700}', 
                        borderWidth: '10px'
                    }
                }
            },
        }
    }
});
export default MyGlossyPreset;