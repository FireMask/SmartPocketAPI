export const formatAPIDate = dateString => 
{
    const [day, month, year] = dateString.split('/');
    const formattedDate = `${year}-${month}-${day}`;
    return formattedDate;
}

export const formatShowDate = dateString => 
{
    const newDate = new Date(dateString);
    var options = { year: 'numeric', month: 'long', day: 'numeric' };
    return newDate.toLocaleDateString("en-US", options)
}

export const formatMoney = amount => {
    return amount.toLocaleString('en-US', {
        style: 'currency',
        currency: 'USD',
      });
}

export const formatterDate = {
    date: 'DD/MM/YYYY',
    month: 'MMM'
}