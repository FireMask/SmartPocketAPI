export const formatAPIDate = dateString => 
{
    const [day, month, year] = dateString.split('/');
    const formattedDate = `${year}-${month}-${day}`;
    return formattedDate;
}

export const formatShowDate = dateString => 
{
    const newDate = new Date(dateString + 'T00:00:00');
    var options = { year: 'numeric', month: 'short', day: 'numeric' };
    return newDate.toLocaleDateString("en-US", options)
}

export const formatShowMothYear = date => {
    if(!date)
        return "";
    var options = { year: 'numeric', month: 'long'};
    return date.toLocaleDateString("en-US", options)
}

export const formatMoney = amount => {
    if (amount == null) {
    return '$0.00'; 
    }
    const formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD'
      });
    
    return formatter.format(amount)
}

export const formatterDate = {
    date: 'DD/MM/YYYY',
    month: 'MMM'
}