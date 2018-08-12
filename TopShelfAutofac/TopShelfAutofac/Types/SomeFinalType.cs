using Microsoft.Extensions.Configuration;
using System;
using TopShelfAutofac.Interfaces;

namespace TopShelfAutofac.Types
{
    public class SomeFinalType : IRandomGenerator
    {
        private readonly IConfiguration _config;
        private const string _cfgSection = "NumberGeneration";

        public SomeFinalType(IConfiguration config)
        {
            _config = config;
        }

        public int GenerateRandomNumber()
        {
            int seed = int.Parse(_config.GetSection(_cfgSection)["Seed"]);
            return new Random().Next();
        }
    }
}
