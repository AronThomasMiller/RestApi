using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestNew2.ApiModels
{
    public class CommentBase
    {
        public string PostId { get; set; }
        public string Text { get; set; }
    }
}
