using FluentAssertions;
using NetArchTest.Rules;
using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Architecture.Tests;

public class ArchitectureTest
{
    private const string ApiNamespace = "QuanLyNhanSu.API";
    private const string ApplicationNamespace = "QuanLyNhanSu.Application";
    private const string ContractNamespace = "QuanLyNhanSu.Contract";
    private const string DomainNamespace = "QuanLyNhanSu.Domain";
    private const string InfrastructureNamespace = "QuanLyNhanSu.Infrastructure";
    private const string PersistenceNamespace = "QuanLyNhanSu.Persistence";
    private const string PresentationNamespace = "QuanLyNhanSu.Presentation";

    #region =================== Architecture ===================

    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;
        var otherProject = new[]
        {
            InfrastructureNamespace,
            PersistenceNamespace,
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProject)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Domain.AssemblyReference.Assembly;
        var otherProject = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PersistenceNamespace,
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProject)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastucture_Should_Not_HaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Infrastructure.AssemblyReference.Assembly;
        var otherProject = new[]
        {
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProject)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Persistence_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Persistence.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Presentation_Should_Not_HaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Presentation.AssemblyReference.Assembly;
        var otherProject = new[]
        {
            PersistenceNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProject)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    #endregion end Architecture

    #region =================== Command ===================

    [Fact]
    public void Command_Should_HaveNameEndWithCommand()
    {
        // Arrange
        var assembly = Contract.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommand))
            .Should().HaveNameEndingWith("Command")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandT_Should_HaveNameEndWithCommand()
    {
        // Arrange
        var assembly = Contract.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommand<>))
            .Should().HaveNameEndingWith("Command")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandler_Should_HaveBeSealed()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Should().BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandler_Should_HaveNameEndWithCommandHandler()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Should().HaveNameEndingWith("CommandHandler")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandlerT_Should_HaveBeSealed()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should().BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandlerT_Should_HaveNameEndWithCommand()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should().HaveNameEndingWith("CommandHandler")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    #endregion end Command

    #region =================== Query ===================

    [Fact]
    public void Query_Should_HaveNameEndWithQuery()
    {
        // Arrange
        var assembly = Contract.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IQuery<>))
            .Should().HaveNameEndingWith("Query")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Query_Should_HaveNameEndWithQueryHandler()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should().HaveNameEndingWith("QueryHandler")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandler_Should_HaveBeSealed()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should().BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    #endregion end Query
}
