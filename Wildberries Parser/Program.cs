using System;

namespace Wildberries_Parser
{
    /// <summary>
    /// Console aplication.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Console aplication.
        /// </summary>
        public static void Main()
        {
            Startup startup = new Startup();
            startup.CreateParsingService().Parse().Wait();
        }
    }
}
