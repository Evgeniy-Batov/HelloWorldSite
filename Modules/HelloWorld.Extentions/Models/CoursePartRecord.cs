using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorld.Extentions.Models
{
    public class CoursePartRecord: ContentPartRecord
    {
        public virtual String Name  { get; set; }
        public virtual String Title { get; set; }
        public virtual String ShortDescription { get; set; }

        private const String SerializeSeparator = ",,,,";

        public static CoursePartRecord DeserializeCoursePartRecord(String str)
        {
            if (String.IsNullOrWhiteSpace(str))
                return new CoursePartRecord();

            String[] parts = str.Split(new string[] { SerializeSeparator },StringSplitOptions.None);

            return new CoursePartRecord()
            {
                Id = String.IsNullOrEmpty(parts[0]) ? 0 : Int32.Parse(parts[0]),
                Name = parts[1],
                Title = parts[2],
                ShortDescription = parts[3]
            };
        }

        public static String SerializeCoursePartRecord(CoursePartRecord record)
        {
            if (record == null)
                return String.Empty;

            return String.Join(SerializeSeparator,record.Id, record.Name, record.Title,record.ShortDescription);
        }

    }

    public class CoursePart: ContentPart<CoursePartRecord>
    {
        public String Name
        {
            get { return Retrieve(r => r.Name); }
            set { Store(r => r.Name, value); }
        }

        public String Title
        {
            get { return Retrieve(r => r.Title); }
            set { Store(r => r.Title, value); }
        }
        
        public String ShortDescription
        {
            get { return Retrieve(r => r.ShortDescription); }
            set { Store(r => r.ShortDescription, value); }
        }
    }

}