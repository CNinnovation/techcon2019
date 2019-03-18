using System;
using System.Text.Json;

namespace JSONSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            // WriteJSON();
            ParseJson();
        }

        private static void WriteJSON()
        {
            Utf8JsonWriter writer = new Utf8JsonWriter();
            writer.WriteStartObject();
            writer.WriteString("date", new DateTime(2019, 3, 12));
            writer.WriteString("title", "TechConference 2019");
            writer.WriteEndObject();
            writer.Flush();

        }


        public static void Utf8JsonReaderLoop(ReadOnlySpan<byte> dataUtf8)
        {
            var json = new Utf8JsonReader(dataUtf8, isFinalBlock: true, state: default);

            while (json.Read())
            {
                JsonTokenType tokenType = json.TokenType;
                ReadOnlySpan<byte> valueSpan = json.ValueSpan;
                switch (tokenType)
                {
                    case JsonTokenType.StartObject:
                    case JsonTokenType.EndObject:
                        break;
                    case JsonTokenType.StartArray:
                    case JsonTokenType.EndArray:
                        break;
                    case JsonTokenType.PropertyName:
                        break;
                    case JsonTokenType.String:
                        string valueString = json.GetString();
                        break;
                    case JsonTokenType.Number:
                        if (!json.TryGetInt32(out int valueInteger))
                        {
                            throw new FormatException();
                        }
                        break;
                    case JsonTokenType.True:
                    case JsonTokenType.False:
                        bool valueBool = json.GetBoolean();
                        break;
                    case JsonTokenType.Null:
                        break;
                    default:
                        throw new ArgumentException();
                }
            }

            dataUtf8 = dataUtf8.Slice((int)json.BytesConsumed);
            JsonReaderState state = json.CurrentState;
        }

        static double ParseJson()
        {
            const string json = " [ { \"name\": \"John\" }, [ \"425-000-1212\", 15 ], { \"grades\": [ 90, 80, 100, 75 ] } ]";

            double average = -1;

            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                JsonElement root = doc.RootElement;
                JsonElement info = root[1];

                string phoneNumber = info[0].GetString();
                int age = info[1].GetInt32();

                JsonElement grades = root[2].GetProperty("grades");

                double sum = 0;
                foreach (JsonElement grade in grades.EnumerateArray())
                {
                    sum += grade.GetInt32();
                }

                int numberOfCourses = grades.GetArrayLength();
                average = sum / numberOfCourses;
            }

            return average;
        }
    }
}
