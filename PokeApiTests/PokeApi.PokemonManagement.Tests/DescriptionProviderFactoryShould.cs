using Moq;
using NUnit.Framework;
using PokeApi.Models;
using PokeApi.PokemonManagement;
using PokeApi.PokemonManagement.Descriptions;
using PokeApi.PokemonManagement.Descriptions.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeApiTests.PokeApi.PokemonManagement.Tests
{
    [TestFixture]
    public class DescriptionProviderFactoryShould
    {
        [Test]
        public void CallGetDescriptorMethodInDescriptionStrategy()
        {
            Mock<IDescriptionStrategy> descStrategy = new Mock<IDescriptionStrategy>();
            descStrategy.Setup(x => x.GetDescriptorKey(It.IsAny<Pokemon>()))
                .Returns(() => "yoda");

            descStrategy.Setup(x => x.InitialiseDescriptorMap())
                .Returns(() => new Dictionary<string, IDescriptionService>
                {
                    {"yoda", new Mock<IDescriptionService>().Object }
                });

            Pokemon pokemon = new Pokemon();

            DescriptionProviderFactory factory = new DescriptionProviderFactory(descStrategy.Object);
            factory.GetDescriptor(pokemon);
            descStrategy.Verify(x => x.GetDescriptorKey(pokemon), Times.Once);
        }
    }
}
