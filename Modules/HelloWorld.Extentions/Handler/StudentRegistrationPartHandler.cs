using HelloWorld.Extentions.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.Extentions.Handler
{
    public class StudentRegistrationPartHandler : ContentHandler
    {
       public StudentRegistrationPartHandler(IRepository<StudentRegistrationPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}