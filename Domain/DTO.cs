using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class DTO
    {
        public Operations Operation { get; set; }
        public Object TransferObject { get; set; }
        public Object Result { get; set; }
    }
}
