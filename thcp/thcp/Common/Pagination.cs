using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thcp.Common
{
    //clase generica
    public class Pagination<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int RecordPerPage { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPage { get; set; }
        //el inge puso Seacrh
        public string Search { get; set; }
        public IEnumerable<T> Result { get; set; }
    }
}
