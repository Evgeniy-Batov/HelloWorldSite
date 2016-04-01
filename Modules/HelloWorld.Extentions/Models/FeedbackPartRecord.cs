using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.Extentions.Models
{
    public class FeedbackPartRecord: ContentPartRecord
    {
        public String           AuthorName { get; set;}
        public int                CourseId { get; set;}
    }

    public class FeedbackPart: ContentPart<FeedbackPartRecord>
    {
        public String AuthorName
        {   
            get { return Retrieve(r => r.AuthorName); }
            set { Store(r => r.AuthorName, value); }
        }

        public int CourseId
        {
            get 
            {
                return Retrieve(r => r.CourseId);
                //var rawCourseRecord = Retrieve<String>("CoursePartRecord");
                //return CoursePartRecord.DeserializeCoursePartRecord(rawCourseRecord);
            }
            set
            {
                Store(r => r.CourseId, value);
                //var serializedStateRecord = CoursePartRecord.SerializeCoursePartRecord(value);
                //Store("CoursePartRecord", serializedStateRecord);
            }
        }
    }
}