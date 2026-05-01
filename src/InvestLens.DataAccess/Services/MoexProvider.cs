using AutoMapper;
using InvestLens.Model;
using InvestLens.Model.Enums;
using InvestLens.Model.Helpers;
using InvestLens.Model.MoexApi.Responses;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace InvestLens.DataAccess.Services;

public class MoexProvider : IMoexProvider
{
    private readonly IMapper _mapper;
    private HttpClient _httpClient;

    public MoexProvider(IMapper mapper)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://iss.moex.com/iss/securities.json")
        };
        _mapper = mapper;
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }

    public async Task<List<SecurityModel>> GetSecurityList(IEnumerable<string> secIdNewList)
    {
        //var secTypeGoups = new Dictionary<string, List<string>>();
        var securityModelList = new List<SecurityModel>();

        foreach (var secId in secIdNewList)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<SecuritiesResponse>($"https://iss.moex.com/iss/securities.json?q={secId}");
                if (response == null)
                {
                    continue;
                }

                var securities = MoexResponseHelper.GetModels<Securities, Security>(response.Securities);
                var security = securities.FirstOrDefault(s => s.SecId == secId);

                //if (security == null) continue;
                //if (!secTypeGoups.TryGetValue(security.SecGroup, out var list))
                //{
                //    list = new List<string>();
                //    secTypeGoups[security.SecGroup] = list;
                //}

                //if (!list.Contains(security.SecType)) list.Add(security.SecType);

                var model = security is not null
                        ? _mapper.Map<SecurityModel>(security)
                        : new SecurityModel(secId, SecurityType.None);

                securityModelList.Add(model);
            }            
            catch (Exception)
            {

            }
        }

        return securityModelList;
    }
}