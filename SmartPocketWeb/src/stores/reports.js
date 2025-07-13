import { ref, onMounted} from 'vue';
import { defineStore } from 'pinia';
import { AlignBy } from '@/enums';
import ReportsAPI from '@/api/ReportsAPI';

export const useReportsStore = defineStore( 'reports', () => {

    const paymentMethodsProjection = ref([])
    const earliestDateProjection = ref(null)
    const filters = ref(
        {
            monthsBefore: -2,
            monthsAhead: 4,
        })

    onMounted(async () => {
        try {
            await getProjections(6);
        } catch (error) {
            console.log(error);
        }
    })

    const getProjections = async () => {
        const {data} = await ReportsAPI.paymentMethodsProjection(filters.value);
        if(data){
            paymentMethodsProjection.value = data?.data?.sort((a, b) => {
                const dateA = new Date(a.periods[0]?.dueDate + 'T00:00:00').getUTCDate();
                const dateB = new Date(b.periods[0]?.dueDate + 'T00:00:00').getUTCDate();
                return dateA - dateB;
            });

            alignProjectionItems(AlignBy.START_DATE);
        }
        else{
            earliestDateProjection.value = null
        }
    }

    const resetProjectionsCheck = () => {
        paymentMethodsProjection.value = paymentMethodsProjection.value.map(
            paymentMethod => {
             return {...paymentMethod, periods: paymentMethod.periods.map(
                projection => {return {...projection, checked: false}} 
            )} 
        }); 
    }

    const monthDiff = (from, to) => {
        return (
            (to.getFullYear() - from.getFullYear()) * 12 +
            (to.getMonth() - from.getMonth())
          );
    }

    const alignProjectionItems = (alignBy) => {
        //Se asigna el valor de alignByDate dependiendo el valor seleccionado
        //Se limpia el arreglo de los empty si es que tenía de una alineación anterior
        var cleanedData = paymentMethodsProjection.value.map(item => {
            const firstPeriod = item.periods[item.emptyPeriods??0];
            switch (alignBy) {
                case AlignBy.START_DATE:
                    item.alignByDate = firstPeriod?.startDate;
                    break;
                case AlignBy.END_DATE:
                    item.alignByDate = firstPeriod?.endDate;
                    break;
                case AlignBy.DUE_DATE:
                    item.alignByDate = firstPeriod?.dueDate;
                    break;
                default:
                    break;
            }
            
            return {
            ...item,
            periods: item.periods.filter(period => !period.empty)
        }});

        //Se obtiene el valor earliestDateProjection
        earliestDateProjection.value =  cleanedData.reduce((minDate, item) => {
            const currentDate = new Date(item.alignByDate + 'T00:00:00');
            return currentDate < minDate ? currentDate : minDate;
        }, new Date('9999-12-31'));

        //Se agregan los emptyPeriods para alinear
        cleanedData = cleanedData.map(item => {
            const diff = monthDiff(earliestDateProjection.value,  new Date(item.alignByDate + 'T00:00:00'));
            item.emptyPeriods = diff;
          
            const emptyPeriods = Array.from({ length: diff }, () => ({
              empty: true,
            }));
          
            return {
              ...item,
              periods: [...emptyPeriods, ...item.periods],
            };
        });
        paymentMethodsProjection.value = cleanedData;
    }


    return {
        paymentMethodsProjection,
        filters,
        earliestDateProjection,
        getProjections,
        resetProjectionsCheck,
        alignProjectionItems
    }
})