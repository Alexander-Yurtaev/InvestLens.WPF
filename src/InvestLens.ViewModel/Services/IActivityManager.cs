using InvestLens.Shared.Model;

namespace InvestLens.ViewModel.Services;

public interface IActivityManager
{
    Task<List<ActivityItem>> GetActivityItems();
}