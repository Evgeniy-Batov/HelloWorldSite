using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.Extentions.Models
{
    public class CourseInformationPartRecord : ContentPartRecord
    {
        public virtual int Length { get; set; }
        public virtual String LengthPeriod { get; set; }
        public virtual String LengthText { get; set; }

        public virtual int Intencity { get; set; }
        public virtual String IntencityPeriod { get; set; }
        public virtual String IntencityText { get; set; }

        public virtual int Duration { get; set; }
        public virtual String DurationPeriod { get; set; }
        public virtual String DurationText { get; set; }

        public virtual int MinPrice { get; set; }
        public virtual String MinPricePeriod { get; set; }
        public virtual String MinPriceText { get; set; }

        public virtual int MaxPrice { get; set; }
        public virtual String MaxPricePeriod { get; set; }
        public virtual String MaxPriceText { get; set; }
    }

    public class CourseInformationPart : ContentPart<CourseInformationPartRecord>
    {
        public string MaxPriceText
        {
            get { return Retrieve(r => r.MaxPriceText); }
            set { Store(r => r.MaxPriceText, value); }
        }

        public string MaxPricePeriod
        {
            get { return Retrieve(r => r.MaxPricePeriod); }
            set { Store(r => r.MaxPricePeriod, value); }
        }

        public int MaxPrice
        {
            get { return Retrieve(r => r.MaxPrice); }
            set { Store(r => r.MaxPrice, value); }
        }

        public string MinPriceText
        {
            get { return Retrieve(r => r.MinPriceText); }
            set { Store(r => r.MinPriceText, value); }
        }

        public string MinPricePeriod
        {
            get { return Retrieve(r => r.MinPricePeriod); }
            set { Store(r => r.MinPricePeriod, value); }
        }

        public int MinPrice
        {
            get { return Retrieve(r => r.MinPrice); }
            set { Store(r => r.MinPrice, value); }
        }

        public String DurationText
        {
            get { return Retrieve(r => r.DurationText); }
            set { Store(r => r.DurationText, value); }
        }

        public String DurationPeriod
        {
            get { return Retrieve(r => r.DurationPeriod); }
            set { Store(r => r.DurationPeriod, value); }
        }

        public int Duration
        {
            get { return Retrieve(r => r.Duration); }
            set { Store(r => r.Duration, value); }
        }


        public String IntencityText
        {
            get { return Retrieve(r => r.IntencityText); }
            set { Store(r => r.IntencityText, value); }
        }

        public String IntencityPeriod
        {
            get { return Retrieve(r => r.IntencityPeriod); }
            set { Store(r => r.IntencityPeriod, value); }
        }

        public int Intencity
        {
            get { return Retrieve(r => r.Intencity); }
            set { Store(r => r.Intencity, value); }
        }


        public int Length
        {
            get { return Retrieve(r => r.Length); }
            set { Store(r => r.Length, value); }
        }

        public String LengthPeriod
        {
            get { return Retrieve(r => r.LengthPeriod); }
            set { Store(r => r.LengthPeriod, value); }
        }

        public String LengthText
        {
            get { return Retrieve(r => r.LengthText); }
            set { Store(r => r.LengthText, value); }
        }


    }
}