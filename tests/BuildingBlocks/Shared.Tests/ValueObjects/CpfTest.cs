using System;
using System.Collections.Generic;
using FluentAssertions;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;
using Xunit;

namespace NerdStoreEnterprise.Tests.BuildingBlocks.Shared.UnitTests.ValueObjects
{
    public class CpfTest
    {
        public static IEnumerable<object[]> ValidCpfNumber
        {
            get
            {
                yield return new object[]
                {
                    "62342780001"
                };
                yield return new object[]
                {
                    "10266364004"
                };
                yield return new object[]
                {
                    "90784483000"
                };
                yield return new object[]
                {
                    "97169839059"
                };
                yield return new object[]
                {
                    "80475181000"
                };
                yield return new object[]
                {
                    "27857286028"
                };
            }
        }

        public static IEnumerable<object[]> InvalidCpfNumbers
        {
            get
            {
                yield return new object[]
                {
                    "50"
                };
                yield return new object[]
                {
                    "A"
                };
                yield return new object[]
                {
                    "5464a56ds"
                };
                yield return new object[]
                {
                    "@$WQFDAD"
                };
                yield return new object[]
                {
                    "1a6s5d4a1sd61a56sd"
                };
                yield return new object[]
                {
                    "526as41d6a4sd8q4236"
                };
            }
        }

        [Theory]
        [MemberData(nameof(ValidCpfNumber))]
        public void ShouldNotThrowDomainExceptionIfCpfIsValid(string cpfNumber)
        {
            //Arrange
            Action cpf = () => new Cpf(cpfNumber);

            //Act and Assert
            cpf.Should().NotThrow<DomainException>();
        }

        [Theory]
        [MemberData(nameof(InvalidCpfNumbers))]
        public void ShouldThorwDomainExceptionIfCpfIsInvalid(string cpfNumber)
        {
            //Arrange
            Action cpf = () => new Cpf(cpfNumber);

            //Act and Assert
            cpf.Should().Throw<DomainException>()
                .WithMessage("Invalid CPF.");
        }
    }
}
