using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public interface IActivityManager
{
    Task<List<ActivityItem>> GetActivityItems();
}