using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kino.Classes
{
    public class KinoteatrFilter
    {
        public string Name { get; set; }           
        public int? MinCountZal { get; set; }   
        public int? MaxCountZal { get; set; }        
        public int? MinCount { get; set; }           
        public int? MaxCount { get; set; }             
    }
}