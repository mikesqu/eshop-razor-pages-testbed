

using MadStick.VM;
using MadStickWebAppTester.Services;

namespace MadStick.Services
{
    public class CacheService 
    {
        private readonly ILogger<CacheService> _logger;
        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        
        public CacheService(ILogger<CacheService> logger)
        {
            _logger = logger;
        }

        public void Update() {
            Status++;
        }
        

    }
}