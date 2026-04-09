using InvestLens.Model;
using InvestLens.ViewModel;
using InvestLens.ViewModel.Services;
using Moq;

namespace InvestLens.Tests;

public class RegistrationViewModelTests
{
    private readonly RegistrationWindowViewModel _viewModel;

    public RegistrationViewModelTests()
    {
        var securityServiceMock = new Mock<ISecurityService>();
        var windowManagerMock = new Mock<IWindowManager>();
        _viewModel = new RegistrationWindowViewModel(new RegistrationModel(), securityServiceMock.Object, windowManagerMock.Object);
    }

    [Fact]
    public void NamePropertyIsRequired()
    {
        Assert.False(_viewModel.HasErrors);
        _viewModel.Name = "";
        // При первом запуске валидация не выполняется
        Assert.False(_viewModel.HasErrors);

        _viewModel.Name = "Bob";
        Assert.False(_viewModel.HasErrors);

        _viewModel.Name = "";
        // До этого уже были изменения - ошибка есть
        Assert.True(_viewModel.HasErrors);
    }

    [Fact]
    public void SurnamePropertyIsRequired()
    {
        Assert.False(_viewModel.HasErrors);
        _viewModel.Surname = "";
        // При первом запуске валидация не выполняется
        Assert.False(_viewModel.HasErrors);

        _viewModel.Surname = "Smith";
        Assert.False(_viewModel.HasErrors);

        _viewModel.Surname = "";
        // До этого уже были изменения - ошибка есть
        Assert.True(_viewModel.HasErrors);
    }

    [Fact]
    public void EmailPropertyIsRequired()
    {
        Assert.False(_viewModel.HasErrors);
        _viewModel.Email = "";
        // При первом запуске валидация не выполняется
        Assert.False(_viewModel.HasErrors);

        _viewModel.Email = "bob@smith.com";
        Assert.False(_viewModel.HasErrors);

        _viewModel.Email = "";
        // До этого уже были изменения - ошибка есть
        Assert.True(_viewModel.HasErrors);
    }

    [Fact]
    public void EmailPropertyShouldHaveCorrectFormat()
    {
        Assert.False(_viewModel.HasErrors);
        
        _viewModel.Email = "bob@smith.com";
        Assert.False(_viewModel.HasErrors);
        
        _viewModel.Email = "bob-smith.com";
        Assert.True(_viewModel.HasErrors);
    }

    [Fact]
    public void ShouldRaiseErrorsChangedWhenNameIsChanged()
    {
        bool fired = false;
        _viewModel.ErrorsChanged += (_, args) =>
        {
            if (args.PropertyName != nameof(_viewModel.Name)) return;
            fired = true;
        };

        // При первом запуске валидация не выполняется
        _viewModel.Name = "";
        Assert.False(fired);

        // Ранее ошибок не было
        _viewModel.Name = "Bob";
        Assert.False(fired);
        fired = false;

        // До этого уже были изменения
        _viewModel.Name = "";
        Assert.True(fired);
    }

    [Fact]
    public void ShouldRaiseErrorsChangedWhenSurnameIsChanged()
    {
        bool fired = false;
        _viewModel.ErrorsChanged += (_, args) =>
        {
            if (args.PropertyName != nameof(_viewModel.Surname)) return;
            fired = true;
        };

        // При первом запуске валидация не выполняется
        _viewModel.Surname = "";
        Assert.False(fired);

        // Ранее ошибок не было
        _viewModel.Surname = "Smith";
        Assert.False(fired);
        fired = false;

        // До этого уже были изменения
        _viewModel.Surname = "";
        Assert.True(fired);
    }
}