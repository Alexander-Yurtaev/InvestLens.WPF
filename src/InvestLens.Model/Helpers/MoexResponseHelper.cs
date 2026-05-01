using InvestLens.Model.MoexApi.Responses.ResponseItems;
using System.Reflection;
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

                var value = row[i]?.ToString();
                prop.SetValue(model, value);
            }

            yield return model;
        }
    }
}