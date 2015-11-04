using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharMap_Plus.Model
{
    public class FontDetail
    {
        public FontDetail()
        {
            FontChars  = new List<FontChar>();
        }

        public string Name { get; set; }

        private string _family;
        public string Family
        {
            get { return _family ?? Name; }
            set { _family = value; }
        }

        public string Type { get; set; }

        private int? _characterCount;
        public int CharacterCount { get
            {
                return _characterCount ?? (CharacterCodes ?? new Dictionary<string, int>()).Count;
            }
            set
            {
                _characterCount = value;
            }
        }

        [JsonProperty(ItemConverterType = typeof(HexMapConverter))]
        public Dictionary<string, int> CharacterCodes { get; set; }

        [JsonIgnore]
        public List<FontChar> FontChars { get; set; }
    }

    public class HexMapConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Dictionary<string, int>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            int value = 0;

            string hexValue = reader.Value as string;
            int.TryParse(hexValue, NumberStyles.AllowHexSpecifier, CultureInfo.CurrentCulture, out value);
         
            return value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var num = (int)value;
            writer.WriteValue(num.ToString("x"));
        }
    }

}
