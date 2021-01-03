using System.Collections.Generic;
using System.Linq;

namespace IData.Services
{
    public static class IElementServiceExtension
    {
        public static T GetService<T>(this IEnumerable<IElementService> services)
        {
            return services.OfType<T>().First();
        }
    }
}