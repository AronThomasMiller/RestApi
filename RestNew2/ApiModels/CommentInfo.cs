using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestNew2.ApiModels
{
    public class CommentInfo
    {
        public string Id { get; set; }
        public string CommentedById { get; set; }
        public string PostId { get; set; }
        public string Text { get; set; }
    }
}
