using AutoFixture;
using AutoFixture.AutoMoq;
using BedeGaming.SimpleSlotMachine.Application.Constants;
using BedeGaming.SimpleSlotMachine.Application.Interfaces.Validators;
using BedeGaming.SimpleSlotMachine.Application.Providers;
using BedeGaming.SimpleSlotMachine.Application.Validators;
using Consoles.Common.Interfaces;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;

namespace BedeGaming.SimpleSlotMachine.Tests.Unit.Providers
{
    public class InitialBalanceProviderTests
    {
        private DepositValidator _validator;

        public InitialBalanceProviderTests()
        {
            _validator = new DepositValidator();
        }

        [Fact]
        public void SetDeposit_ValidInput_ShouldSetDeposit()
        {
            // Arrange
            decimal deposit = 1000;
            var validatorMock = new Mock<IDepositValidator>();
            var consoleInputReaderMock = new Mock<IConsoleInputReader>();

            consoleInputReaderMock.Setup(c => c.ReadValidInput<decimal>(It.IsAny<string>()))
                .Returns(deposit); // Simulate user input

            validatorMock.Setup(v => v.Validate(It.IsAny<decimal>()))
                .Returns(_validator.TestValidate(deposit));

            var balanceProvider = new InitialBalanceProvider(validatorMock.Object, consoleInputReaderMock.Object);

            // Act
            balanceProvider.Deposit = deposit;

            // Assert
            balanceProvider.Deposit.Should().Be(deposit);
        }

        //!TODO: should be investigated!
        [Theory(Skip = "Not work, because the second invocation of ReadValidInput<> not happens!")]
        [InlineData(-50, 500)]
        public void SetDeposit_InvalidInputThenValidInput_ShouldSetDeposit(decimal invalidInput, decimal validInput)
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var validatorMock = fixture.Freeze<Mock<IDepositValidator>>();
            var consoleInputReaderMock = fixture.Freeze<Mock<IConsoleInputReader>>();
            var balanceProvider = fixture.Create<InitialBalanceProvider>();

            var resultFailure = _validator.TestValidate(invalidInput);
            var result = _validator.TestValidate(validInput);

            //?Info: this part not work as expected...
            consoleInputReaderMock.SetupSequence(c => c.ReadValidInput<decimal>(Messages.Balance.InitialDepositPrompt))
                .Returns(invalidInput)
                .Returns(validInput); // Simulate invalid input

            validatorMock.SetupSequence(v => v.Validate(It.IsAny<decimal>()))
                .Returns(resultFailure)
                .Returns(result);

            // Act
            balanceProvider.Deposit = invalidInput;

            // Assert
            balanceProvider.Deposit.Should().Be(validInput);

            // Verify that the ReadValidInput method was called in the expected sequence
            consoleInputReaderMock.Verify(c => c.ReadValidInput<decimal>(Messages.Balance.InitialDepositPrompt), Times.Exactly(2));

            // Verify that the loop occurred with a failed validation and then successful validation
            validatorMock.Verify(v => v.Validate(invalidInput), Times.Once);
            validatorMock.Verify(v => v.Validate(validInput), Times.Once);
        }
    }
}