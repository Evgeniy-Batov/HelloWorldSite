using HelloWorld.Extentions.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.Extentions.Handler
{
    public class FeedbackPartHandler: ContentHandler
    {
        public FeedbackPartHandler(IRepository<FeedbackPartRecord> repository)
        {
            this.Filters.Add(StorageFilter.For(repository));
        }
    }
}