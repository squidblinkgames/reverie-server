using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


public static class JsonExtensions
{
	public static readonly JsonSerializerSettings JsonFormat =
		new JsonSerializerSettings

		{
			NullValueHandling = NullValueHandling.Ignore,
			ContractResolver = new CamelCasePropertyNamesContractResolver()
		};


	public static string ToJson<T>(this T obj)
	{
		return JsonConvert.SerializeObject(obj, JsonFormat);
	}


	public static string ToPrettyJson<T>(this T obj)
	{
		return JsonConvert.SerializeObject(obj, Formatting.Indented, JsonFormat);
	}
}