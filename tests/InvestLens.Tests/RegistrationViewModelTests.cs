using InvestLens.DataAccess.Services;
using InvestLens.Model.Crud.User;
using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Windows;
using Moq;

namespace InvestLens.Tests;

public class RegistrationViewModelTests
{
    private readonly RegistrationWindowViewModel _viewModel;
    private readonly Mock<ISecurityService> _securityServiceMock;

    public RegistrationViewModelTests()
    {
        var model = new Mock<RegistrationModel>();
        _securityServiceMock = new Mock<ISecurityService>();
        _securityServiceMock.Setup(s => s.CheckLoginUniqueAsync(It.IsAny<string>())).ReturnsAsync(true);

        var windowManagerMock = new Mock<IWindowManager>();
        _viewModel = new RegistrationWindowViewModel(model.Object, _securityServiceMock.Object, windowManagerMock.Object);
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
    public void LoginPropertyIsRequired()
    {
        Assert.False(_viewModel.HasErrors);
        _viewModel.Login = "";
        // При первом запуске валидация не выполняется
        Assert.False(_viewModel.HasErrors);

        _viewModel.Login = "TestUser";
        Assert.False(_viewModel.HasErrors);

        _viewModel.Login = "";
        // До этого уже были изменения - ошибка есть
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

    [Fact]
    public void ShouldCallCheckLoginUniqueAsync()
    {
        // Arrange

        // Act
        _viewModel.Login = "TestUser";

        // Assert
        _securityServiceMock.Verify(s => s.CheckLoginUniqueAsync("TestUser"), Times.Once);
    }
}