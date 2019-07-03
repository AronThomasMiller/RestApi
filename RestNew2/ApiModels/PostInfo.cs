using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestNew2.ApiModels
{
    public class PostInfo
    {
        public string Id { get; set; }
        public double Rate { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public override bool Equals(object obj)
        {
            return Id == ((PostInfo)obj).Id;
        }
    }
}
