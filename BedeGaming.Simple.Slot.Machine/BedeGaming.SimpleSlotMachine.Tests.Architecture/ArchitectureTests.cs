using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;

namespace BedeGaming.SimpleSlotMachine.Tests.Architecture
{
    public class ArchitectureTests
    {
        private const string ApplicationNamespace = "BedeGaming.SimpleSlotMachine.Application";
        private const string DomainNamespace = "BedeGaming.SimpleSlotMachine.Domain";
        private const string PresentationNamespace = "BedeGaming.SimpleSlotMachine.ConsoleGame";
        private const string SharedNamespace = "Consoles.Common";

        [Fact]
        public void Domain_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            Assembly assembly = Assembly.Load(DomainNamespace);

            var otherProjects = new[] {
                ApplicationNamespace,
                PresentationNamespace,
                SharedNamespace
            };

            // Act
            var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Application_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            Assembly assembly = Assembly.Load(ApplicationNamespace);

            var otherProjects = new[] {
                PresentationNamespace,
            };

            // Act
            var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Presentation_Should_HaveDependencyOnOtherProjects()
        {
            // Arrange
            Assembly assembly = Assembly.Load(PresentationNamespace);

            var otherProjects = new[] {
                ApplicationNamespace,
                PresentationNamespace,
                SharedNamespace,
            };

            // Act
            var testResult = Types
            .InAssembly(assembly)
            .Should()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Services_Should_Have_DependencyOnDomain()
        {
            // Arrange
            Assembly assembly = Assembly.Load(ApplicationNamespace);

            // Act
            var testResult = Types
            .InAssembly(assembly)
            .That()
            .AreClasses()
            .And()
            .HaveNameEndingWith("Service")
            .Should()
            .HaveDependencyOn(DomainNamespace)
            .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
    }
}