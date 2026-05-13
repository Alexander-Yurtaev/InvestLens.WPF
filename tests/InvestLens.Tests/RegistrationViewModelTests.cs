using InvestLens.DataAccess.Services;
using InvestLens.Model.Crud.User;
using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Windows;
using Moq;
using FluentAssertions;

namespace InvestLens.Tests;

public class RegistrationViewModelTests
{
    private readonly RegistrationWindowViewModel _viewModel;
    private readonly Mock<IAuthService> _authServiceMock;

    public RegistrationViewModelTests()
    {
        var model = new Mock<RegistrationModel>();
        _authServiceMock = new Mock<IAuthService>();
        _authServiceMock.Setup(s => s.CheckLoginUniqueAsync(It.IsAny<string>())).ReturnsAsync(true);

        var windowManagerMock = new Mock<IWindowManager>();
        _viewModel = new RegistrationWindowViewModel(model.Object, _authServiceMock.Object, windowManagerMock.Object);
    }

    [Fact]
    public void NamePropertyIsRequired()
    {
        _viewModel.HasErrors.Should().BeFalse();
        _viewModel.Name = "";
        // При первом запуске валидация не выполняется
        _viewModel.HasErrors.Should().BeFalse();

        _viewModel.Name = "Bob";
        _viewModel.HasErrors.Should().BeFalse();

        _viewModel.Name = "";
        // До этого уже были изменения - ошибка есть
        _viewModel.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void SurnamePropertyIsRequired()
    {
        _viewModel.HasErrors.Should().BeFalse();
        _viewModel.Surname = "";
        // При первом запуске валидация не выполняется
        _viewModel.HasErrors.Should().BeFalse();

        _viewModel.Surname = "Smith";
        _viewModel.HasErrors.Should().BeFalse();

        _viewModel.Surname = "";
        // До этого уже были изменения - ошибка есть
        _viewModel.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void LoginPropertyIsRequired()
    {
        _viewModel.HasErrors.Should().BeFalse();
        _viewModel.Login = "";
        // При первом запуске валидация не выполняется
        _viewModel.HasErrors.Should().BeFalse();

        _viewModel.Login = "TestUser";
        _viewModel.HasErrors.Should().BeFalse();

        _viewModel.Login = "";
        // До этого уже были изменения - ошибка есть
        _viewModel.HasErrors.Should().BeTrue();
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
        fired.Should().BeFalse();

        // Ранее ошибок не было
        _viewModel.Name = "Bob";
        fired.Should().BeFalse();
        fired = false;

        // До этого уже были изменения
        _viewModel.Name = "";
        fired.Should().BeTrue();
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
        fired.Should().BeFalse();

        // Ранее ошибок не было
        _viewModel.Surname = "Smith";
        fired.Should().BeFalse();
        fired = false;

        // До этого уже были изменения
        _viewModel.Surname = "";
        fired.Should().BeTrue();
    }

    [Fact]
    public void ShouldCallCheckLoginUniqueAsync()
    {
        // Arrange

        // Act
        _viewModel.Login = "TestUser";

        // Assert
        _authServiceMock.Verify(s => s.CheckLoginUniqueAsync("TestUser"), Times.Once);
    }
}