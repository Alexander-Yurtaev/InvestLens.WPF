using InvestLens.Common.Helpers;
using InvestLens.Model.MoexApi.Responses.ResponseItems;
using System.Reflection;
using System.Security.AccessControl;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvestLens.Model.Helpers;

public static class MoexResponseHelper
{
    public static IEnumerable<TModel> GetModels<TItem, TModel>(TItem responseItem) 
        where TItem : BaseMoexResponseItem
        where TModel : class
    {
        foreach (var row in responseItem.Data)
        {
            var model = Activator.CreateInstance<TModel>();
            if (model is null) throw new ArgumentException(nameof(TModel));

            var props = model.GetType()
                .GetProperties()
                .Where(p => p.GetCustomAttribute<JsonPropertyNameAttribute>() != null)
                .ToDictionary(k => k.GetCustomAttribute<JsonPropertyNameAttribute>()!.Name, v => v);

            for (var i = 0; i < responseItem.Columns.Length; i++)
            {
                if (!props.TryGetValue(responseItem.Columns[i], out var prop)) continue;

                if (row[i] is not null)
                {
                    var elevent = (JsonElement)row[i];
                    var value = PropetyTypeConvert(prop, elevent);
                    prop.SetValue(model, value);
                }
            }

            yield return model;
        }
    }

    private static dynamic PropetyTypeConvert(PropertyInfo prop, JsonElement element)
    {
        if (prop.PropertyType == typeof(string)) return element.ToString();
        if (prop.PropertyType == typeof(int)) return int.Parse(element.ToString());
        if (prop.PropertyType == typeof(decimal)) return decimal.Parse(element.ToString());
        if (prop.PropertyType == typeof(bool))
        {
            return int.Parse(element.ToString()) == 1;
        }

        throw new ArgumentException(nameof(prop));
    }
}