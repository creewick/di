using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public interface IFrequencyProvider
    {
        Dictionary<string, int> GetFrequency(IWordParser wordParser, IWordFilter[] wordFilters,
            IWordTransformation[] wordTransformations);
    }
}
