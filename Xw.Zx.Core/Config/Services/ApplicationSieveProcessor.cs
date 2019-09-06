
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;


namespace Xw.Zx.Core.Config
{
    //protected class Option3s : IOptions<SieveOptions>
    //{
    //    //public SieveOptions Value = new SieveOptions()
    //    //{
    //    //    CaseSensitive = false,
    //    //    DefaultPageSize = 30,
    //    //    MaxPageSize = 50,
    //    //    ThrowExceptions = true
    //    //};

    //    public SieveOptions Value => throw new System.NotImplementedException();
    //};



    /// <summary>
    /// 配置mapper
    /// </summary>
    public class ApplicationSieveProcessor : SieveProcessor
    {
        public ApplicationSieveProcessor(IOptions<SieveOptions> options, ISieveCustomSortMethods customSortMethods, ISieveCustomFilterMethods customFilterMethods) : base(options, customSortMethods, customFilterMethods)
        {

        }

        public ApplicationSieveProcessor(IOptions<SieveOptions> options) :base(options)
        {

        }

    }

    public class ManagerSieveOptions : SieveOptions
    { 
    }

    public interface IManagerSieveProcessor : ISieveProcessor
    { 
    
    }
    public class ManagerSieveProcessor : SieveProcessor, IManagerSieveProcessor
    {
        public ManagerSieveProcessor(IOptions<ManagerSieveOptions> options, ISieveCustomSortMethods customSortMethods, ISieveCustomFilterMethods customFilterMethods) : base(options, customSortMethods, customFilterMethods)
        {

        }

    }
}
  