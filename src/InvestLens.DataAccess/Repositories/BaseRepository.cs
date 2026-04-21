using InvestLens.Model.Services;

namespace InvestLens.DataAccess.Repositories;

public abstract class BaseRepository
{
    protected BaseRepository(
        InvestLensDataContext dataContext, 
        IAuthManager authManager)
    {
        DataContext = dataContext;
        AuthManager = authManager;
    }

    public InvestLensDataContext DataContext { get; }
    public IAuthManager AuthManager { get; }

    protected int GetOwnerId()
    {
        if (AuthManager.CurrentUser is null)
        {
            throw new InvalidOperationException("Вы не авторизованы!");
        }

        return AuthManager.CurrentUser.Id;
    }
}