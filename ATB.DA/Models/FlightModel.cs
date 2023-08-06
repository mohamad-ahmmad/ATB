using ATB.DA.Enums;
using ATB.DA.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Text;

namespace ATB.DA.Models
{
    public class FlightModel
    {

        public ulong FlightId { get; set; }

        [Required(ErrorMessage = "Departure country is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Departure country must contain only text.")]
        public string DepCountry { get; set; }
        
        [Required(ErrorMessage = "Arrival country is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Arrival country must contain only text.")]
        public string ArrivalCountry { get; set; }

        [Required(ErrorMessage = "Departure date is required.")]
        [ValidDate(ErrorMessage = "The departure date is invalid.")]
        public DateTime DepDate { get; set; }

        [Required(ErrorMessage = "Departure airport is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Departure airport only text allowed.")]
        
        public string DepAirport { get; set; }

        [Required(ErrorMessage = "Arrival airport is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Arrival airport only text allowed.")]
      
        public string ArrivalAirport { get; set; }

        public List<FlightClassModel> FlightClasses { get; set; }

        public string DateFormat { get ; set; }

        private const string _defaultFormat = "yyyy-MM-dd HH:mm:ss";

        public FlightModel
            (
            ulong flightId,
            string depCountry,
            string arrivalCountry,
            DateTime depDate,
            string depAirport,
            string arrivalAirport,
            List<FlightClassModel> flightClasses,
            string? dateFormat
            )
        => (FlightId, DepCountry, ArrivalCountry, DepDate, DepAirport, ArrivalAirport, FlightClasses, DateFormat)
            = (flightId, depCountry, arrivalCountry, depDate, depAirport, arrivalAirport, flightClasses, dateFormat?? _defaultFormat);
        /// <summary>
        /// CSV Representation of FlightModel Object.
        /// </summary>
        /// <returns></returns>
        public string ToCSV()
        {
            string flightClassesCSV = string.Empty;
            flightClassesCSV += FlightClasses.Count + "\r\n";

            foreach ( var flightClass in FlightClasses )
                   flightClassesCSV+= flightClass.ToCSV()+"\r\n";

           string dateFormat = DateFormat == _defaultFormat ? "default": DateFormat;

           return $"{FlightId},{DepCountry},{ArrivalCountry},{DepDate.ToString(DateFormat)},{DepAirport},{ArrivalAirport},{dateFormat},{flightClassesCSV}";
        }

       
        /// <summary>
        /// Gives a unique Id for the flight
        /// </summary>
        /// <returns></returns>
     

        /// <summary>
        /// Split the string into two strings flightDetails and flightClasses.
        /// </summary>
        /// <param name="csv"></param>
        /// <param name="flightDetails"></param>
        /// <param name="flightClasses"></param>
        private static void SplitFlightAndFlightClassesDetails(string csv, out string flightDetails, out string flightClasses)
        {
            int indexOfDelimiter = csv.IndexOf("\r\n");
            flightDetails = csv.Substring(0, indexOfDelimiter);
            flightClasses = csv.Substring(indexOfDelimiter + 2);
        }
        /// <summary>
        /// Construct the FlightClasses for FlightModel.
        /// </summary>
        /// <param name="flightClasses"></param>
        /// <returns>List<FligtClassModel></returns>
        private static List<FlightClassModel> FromCSVToFlightClassesModels(string flightClasses)
        {
            List<FlightClassModel> flightsClasses = new();

            string[] flightClassesLines = flightClasses.Trim('\n').Split("\r\n");

            for (int i = 0; i < flightClassesLines.Length; i++)
            {
                string[] classFields = flightClassesLines[i].Split(",");
                flightsClasses.Add(new FlightClassModel
                        (
                            (FlightClassEnum)int.Parse(classFields[0]),
                            int.Parse(classFields[1]),
                            int.Parse(classFields[2])
                        )
                    );
            }
            return flightsClasses;
        }

        /// <summary>
        /// Convert CSV string format to FlightModel object.
        /// </summary>
        /// <param name="csv"></param>
        /// <returns>FlightModel</returns>
        public static FlightModel FromCSV(string csv)
        {
            string flightDetails, flightClasses;
            SplitFlightAndFlightClassesDetails(csv, out flightDetails, out flightClasses);

            string[] fields = flightDetails.Split(",");
            ulong flightId = ulong.Parse(fields[0]);
            string depCountry = fields[1];
            string arrivalCountry = fields[2];
            string dateFormat = fields[6] == "default" ? _defaultFormat : fields[6];
            DateTime depDate = DateTime.ParseExact(fields[3], dateFormat, null);
            string depAirport = fields[4];
            string arrivalAirport = fields[5];

            List<FlightClassModel> flightsClasses = FromCSVToFlightClassesModels(flightClasses);
            
            return new FlightModel
                (
                    flightId,
                    depCountry,
                    arrivalCountry,
                    depDate,
                    depAirport,
                    arrivalAirport,
                    flightsClasses,
                    dateFormat
                );
        }

        /// <summary>
        /// Value equality between two FlightModel objects.
        /// </summary>
        /// <param name="flight"></param>
        /// <returns>bool.</returns>
        public bool EqualsTo(FlightModel flight)
        {
            if (flight.FlightClasses.Count != this.FlightClasses.Count)
                return false;

                bool isFlightClassesTheSame = true;
            for (int i = 0; i < flight.FlightClasses.Count; i++)
                isFlightClassesTheSame =
                this.FlightClasses[i] == flight.FlightClasses[i];


            if (!isFlightClassesTheSame)
                return false;

            return
                this.FlightId == flight.FlightId &&
                this.DepCountry == flight.DepCountry &&
                this.ArrivalCountry == flight.ArrivalCountry &&
                this.DepDate == flight.DepDate &&
                this.DepAirport == flight.DepAirport &&
                this.ArrivalAirport == flight.ArrivalAirport;

        }

        public static List<string> GetFlightModelFieldDetails()
        {
            List<string> fieldDetails = new List<string>();
            Type flightModelType = typeof(FlightModel);
            PropertyInfo[] properties = flightModelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                string fieldName = property.Name;

                // Get the data type of the property
                string dataType = property.PropertyType.Name;

                // Get the required and regular expression attributes
                var requiredAttribute = property.GetCustomAttribute<RequiredAttribute>();
                string required = requiredAttribute != null ? "Required" : "Not Required";

                var regexAttribute = property.GetCustomAttribute<RegularExpressionAttribute>();
                string regex = regexAttribute != null ? $"Allowed Format: {regexAttribute.Pattern}" : "Free Text";

                // Get custom validation attributes (e.g., ValidDateAttribute)
                var customAttributes = property.GetCustomAttributes<ValidationAttribute>();
                foreach (var attribute in customAttributes)
                {
                    if (attribute is ValidDateAttribute validDateAttribute)
                    {
                        regex += "\nAllowed Range: Today -> Future";
                    }
                    // Add more custom attribute checks here if needed
                }

                string fieldDetail = $"- {fieldName}:\n  - {dataType}\n  - {required}\n  - {regex}";
                fieldDetails.Add(fieldDetail);
            }

            return fieldDetails;
        }

        public override string ToString()
        {
            string flightClasses 
                = this.FlightClasses.Aggregate(new StringBuilder("{\r\n"), (builder, flightClass) => builder.Append(flightClass.ToString())).Append("}\r\n").ToString();

            return $"Flight id : {FlightId}\r\n" +
                    $"Departure country : {DepCountry}\r\n" +
                    $"Arrival country : {ArrivalCountry}\r\n" +
                    $"Departure date : {DepDate}\r\n" +
                    $"Departure airport : {DepAirport}\r\n" +
                    $"Arrival Airport : {ArrivalAirport}\r\n" +
                    $"Classes : {flightClasses}"; 
        }


    }
}
