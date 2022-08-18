using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class PagingData
    {
        public int TotalRecords { get; set; }
        public int TotalRecordsd { get; set; }
        public int RecordsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalRecords / RecordsPerPage);
        public int TotalPagesd => (int)Math.Ceiling((decimal)TotalRecordsd / RecordsPerPage);
        public string UrlParams { get; set; }
        public int LinksPerPage { get; set; }
    

    public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
    }

}
