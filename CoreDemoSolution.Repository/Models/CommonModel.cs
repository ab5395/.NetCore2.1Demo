

namespace CoreDemoSolution.Repository.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class CommonModel
    {
        public static string NullToString(this string val)
        {
            return (val + "").Trim();
        }
    }
}
