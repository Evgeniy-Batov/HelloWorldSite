using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.Extentions.Models
{
    public class StudentRegistrationPartRecord: ContentPartRecord
    {
        public virtual String HeaderText      { get; set; }
        public virtual String FirstNameLabel  { get; set; }
        public virtual String MiddleNameLabel { get; set; }
        public virtual String LastNameLabel   { get; set; }
        public virtual String EmailLabel      { get; set; }
        public virtual String PhoneLabel      { get; set; }
        public virtual String CommentLabel    { get; set; }
		public virtual String RegisterLable   { get; set; }
    }

    public class StudentRegistrationPart : ContentPart<StudentRegistrationPartRecord>
    {
        public String HeaderText
        {
            get { return Retrieve(r => r.HeaderText); }
            set { Store(r => r.HeaderText, value); }
        }

        public String FirstNameLabel
        {
            get { return Retrieve(r => r.FirstNameLabel); }
            set { Store(r => r.FirstNameLabel, value); }
        }

        public String MiddleNameLabel
        {
            get { return Retrieve(r => r.MiddleNameLabel); }
            set { Store(r => r.MiddleNameLabel, value); }
        }

        public String LastNameLabel
        {
            get { return Retrieve(r => r.LastNameLabel); }
            set { Store(r => r.LastNameLabel, value); }
        }

        public String EmailLabel
        {
            get { return Retrieve(r => r.EmailLabel); }
            set { Store(r => r.EmailLabel, value); }
        }

        public String PhoneLabel
        {
            get { return Retrieve(r => r.PhoneLabel); }
            set { Store(r => r.PhoneLabel, value); }
        }

        public String CommentLabel
        {
            get { return Retrieve(r => r.CommentLabel); }
            set { Store(r => r.CommentLabel, value); }
        }

		public String RegisterLable
		{
			get { return Retrieve(r => r.RegisterLable); }
			set { Store(r => r.RegisterLable,value); }
		}
    }
}