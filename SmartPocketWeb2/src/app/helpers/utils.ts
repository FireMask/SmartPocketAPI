export const dateToString = (date: Date | null): string => {
    
    if (date === null || date === undefined || isNaN(new Date(date).getTime())) {
        return '';
    }
    const dateConverted = new Date(date as Date);

    const year = dateConverted.getFullYear();
    const month = (dateConverted.getMonth() + 1).toString().padStart(2, '0');
    const day = dateConverted.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;   
}