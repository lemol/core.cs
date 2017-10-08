using System.Collections.Generic;

namespace Core.Application.Services
{
    public class PaggedList<TDto>
    {
        public IEnumerable<TDto> Result { get; private set; }
        public int Total { get; private set; }

        public PaggedList(IEnumerable<TDto> result, int total)
        {
            Result = result;
            Total = total;
        }
    }
}
