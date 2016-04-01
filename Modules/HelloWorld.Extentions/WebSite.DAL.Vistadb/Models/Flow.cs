using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models
{
    public class FlowDbModel
    {
        public int FlowId { get; set; }
        public int CourseId { get; set; }
        public DateTime? EstimatedStartDate { get; set; }
        public String CustomName { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public String StartDateStr { get; set; }
        public int Status { get; set; }

        public virtual CourseDbM Course { get; set; }
        public ICollection<SignupApplicationDbModel> Applications { get; set; }

        public int MondayStart    { get; set; }
        public int MondayEnd      { get; set; }
        public int ThuesdayStart  { get; set; }
        public int ThuesdayEnd    { get; set; }
        public int WednesdayStart { get; set; }
        public int WednesdayEnd   { get; set; }
        public int ThursdayStart  { get; set; }
        public int ThursdayEnd    { get; set; }
        public int FridayStart    { get; set; }
        public int FridayEnd      { get; set; }
        public int SaturdayStart  { get; set; }
        public int SaturdayEnd    { get; set; }
        public int SundayStart    { get; set; }
        public int SundayEnd      { get; set; }
    }
}
