using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public record GetPagedProductsQuery(int Page = 1, int PageSize = 10);
}
