using Newtonsoft.Json.Converters;

namespace O10.Nomy.Utils
{
    public class DateDateTimeConverter : IsoDateTimeConverter
    {
        public DateDateTimeConverter()
        {
            DateTimeFormat = "MM/dd/yyyy";
        }
    }
}
