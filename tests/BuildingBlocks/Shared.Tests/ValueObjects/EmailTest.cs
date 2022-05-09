using System;
using System.Collections.Generic;
using FluentAssertions;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;
using Xunit;

namespace NerdStoreEnterprise.Tests.BuildingBlocks.Shared.UnitTests.ValueObjects
{
    public class EmailTest
    {
        #region Test data collections

        public static IEnumerable<object[]> ValidEmailAddresses
        {
            get
            {
                yield return new object[]
                {
                    "teste@teste.com"
                };
                yield return new object[]
                {
                    "flavio.candela@gmail.com"
                };
                yield return new object[]
                {
                    "jose.filipe@yahoo.com"
                };
                yield return new object[]
                {
                    "xt1@bolmail.com"
                };
                yield return new object[]
                {
                    "maria.luiza@uol.com"
                };
                yield return new object[]
                {
                    "tsa@tsa.com.br"
                };
            }
        }
        public static IEnumerable<object[]> InvalidEmailAddresses
        {
            get
            {
                yield return new object[]
                {
                    "50@@gmail.com"
                };
                yield return new object[]
                {
                    "A"
                };
                yield return new object[]
                {
                    "546456ds"
                };
                yield return new object[]
                {
                    "www@$WQFDAD"
                };
                yield return new object[]
                {
                    "1a6s5d4@a1sd61a56sd"
                };
                yield return new object[]
                {
                    "526as41d6a4sd8q4236.com"
                };
            }
        }

        #endregion

        [Theory]
        [MemberData(nameof(ValidEmailAddresses))]
        public void ShouldNotThrowDomainExceptionIfEmailIsValid(string emailAddress)
        {
            //Arrange
            Action email = () => new Email(emailAddress);

            //Act and Assert
            email.Should().NotThrow<DomainException>();
        }

        [Theory]
        [MemberData(nameof(InvalidEmailAddresses))]
        public void ShouldThorwDomainExceptionIfEmailIsInvalid(string emailAddress)
        {
            //Arrange
            Action email = () => new Email(emailAddress);

            //Act and Assert
            email.Should().Throw<DomainException>()
                .WithMessage("The email provided is invalid.");
        }
    }
}