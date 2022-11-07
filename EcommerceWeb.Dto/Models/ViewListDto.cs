using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWeb.Dto.Models
{
    public class ViewListDto<T> where T : class
    {
        public List<T> ModelDatas { get; set; } = new List<T>();
        public int MaxPage { get; set; }
        public int PageIndex { get; set; }
    }
}
