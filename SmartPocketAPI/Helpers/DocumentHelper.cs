using SmartPocketAPI.Models;
using ExcelDataReader;

namespace SmartPocketAPI.Helpers;

public class DocumentHelper
{
    public static List<MovementDocument> LoadFromXLSFile(Stream stream)
    {
        List<MovementDocument> movementList = new List<MovementDocument>();
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        using (var reader = ExcelReaderFactory.CreateReader(stream))
        {
            do
            {
                reader.Read(); // Skip header row
                while (reader.Read())
                {
                    var card = reader.GetValue(0)?.ToString() ?? string.Empty;
                    var date = DateOnly.FromDateTime(DateTime.Parse(reader.GetValue(1)?.ToString() ?? DateTime.MinValue.ToString()));
                    var concept = reader.GetValue(2)?.ToString() ?? string.Empty;
                    var amount = decimal.TryParse(reader.GetValue(3)?.ToString(), out var amt) ? amt : 0m;

                    movementList.Add(new MovementDocument(card, date, concept, amount));
                }
            } while (reader.NextResult());

        }
        return movementList;
    }
}