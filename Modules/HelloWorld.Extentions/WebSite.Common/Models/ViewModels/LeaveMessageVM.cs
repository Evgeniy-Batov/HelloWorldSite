using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebSite.Helpers;

namespace WebSite.Common.Models.ViewModels
{
    public class LeaveMessageVM
    {
        [Required]
        public long MessageId { get; set; }

        //[RegularExpression(@"^[a-zA-Z][a-zA-Z0-9-_\.]{1,20}$ ", ErrorMessage = "Имя может содержать только символы")]
        [Required(ErrorMessage="Укажите ваше имя")]
        [StringLength(32,ErrorMessage = "Недопустимая минимальная длинна имени",MinimumLength=2)]
        public String Name {get;set;}
        
        [Required(ErrorMessage = "Укажите ваш e-mail")]
        [EmailAddressAttribute(ErrorMessage="Укажите e-mail в правильном формате")]
        public String Email {get;set;}
        
        [RequiredAttribute(ErrorMessage = "Укажите ваш телефон")]
        [StringLength(32, ErrorMessage = "Недопустимая длинна номера", MinimumLength = 10)]
        public String Phone {get;set;}
        
        public String Topic {get;set;}
        
        public String Message { get; set; }
    }

    public class OfflineMessage : LeaveMessageVM
    {
        public DateTime PostedOn { get; set; }

        public String recaptcha_challenge_field { get; set; }

        public String recaptcha_response_field { get; set; }

        public String Ip { get; set; }
    }


    public enum LessonsTime
    {
        Morning,
        Evening,
        Whatever,
        Unavailable
    }

    public class ClassesSchedule:IEnumerable<KeyValuePair<DayOfWeek, LessonsTime>>
    {
        private Dictionary<DayOfWeek, LessonsTime> _daysMap;
        private CultureInfo _culture;

        public ClassesSchedule()
        {
            _culture = new CultureInfo("ru-RU");

            _daysMap = new Dictionary<DayOfWeek, LessonsTime>();
            _daysMap.Add(DayOfWeek.Monday, LessonsTime.Whatever);
            _daysMap.Add(DayOfWeek.Tuesday, LessonsTime.Whatever);
            _daysMap.Add(DayOfWeek.Wednesday, LessonsTime.Whatever);
            _daysMap.Add(DayOfWeek.Thursday, LessonsTime.Whatever);
            _daysMap.Add(DayOfWeek.Friday, LessonsTime.Whatever);
            _daysMap.Add(DayOfWeek.Saturday, LessonsTime.Whatever);
            _daysMap.Add(DayOfWeek.Sunday, LessonsTime.Whatever);
        }

        public void SetAvailableTime(DayOfWeek day, LessonsTime time)
        {
            if (_daysMap.ContainsKey(day))
                _daysMap[day] = time;
        }

        public int GetDesiredTimeAsInt(DayOfWeek day)
        {
            return (int)GetDesiredTime(day);
        }

        public LessonsTime GetDesiredTime(DayOfWeek day)
        {
            LessonsTime foundTime = LessonsTime.Whatever;
            _daysMap.TryGetValue(day, out foundTime);
            return foundTime;
        }

        public String GetDayInfoText(DayOfWeek day)
        {
            String russianDay = _culture.DateTimeFormat.GetDayName(day);

            return Char.ToUpper(russianDay[0]) + russianDay.Substring(1);
        }

        [Display(Name="Любое время")]
        public bool AnyTime
        {
            get
            {
                foreach (LessonsTime time in _daysMap.Values)
                    if (time != LessonsTime.Whatever)
                        return false;

                return true;
            }
            set
            { 
            }
        }

        public IEnumerator<KeyValuePair<DayOfWeek, LessonsTime>> GetEnumerator()
        {
            return this._daysMap.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._daysMap.GetEnumerator();
        }


        public IEnumerable<SelectListItem> AvailableTimeOptions(DayOfWeek day)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            int selection = 2;

            if (_daysMap.ContainsKey(day))
            {
                if (_daysMap[day] == LessonsTime.Morning)
                    selection = 0;
                else if (_daysMap[day] == LessonsTime.Evening)
                    selection = 1;
            }

            result.Add(new SelectListItem() { Selected = selection == 0, Text = "Утро", Value = ((int)LessonsTime.Morning).ToString() });
            result.Add(new SelectListItem() { Selected = selection == 1, Text = "Вечер", Value = ((int)LessonsTime.Evening).ToString() });
            result.Add(new SelectListItem() { Selected = selection == 2, Text = "Неважно", Value = ((int)LessonsTime.Whatever).ToString() });
            
            
            return result;
        }
    }

    //public class SignupApplication
    //{
    //    public String FirstName { get; set; }
    //    public String LastName  { get; set; }
    //    public String Email     { get; set; }

    //}

    public class SignupApplicationData
    {

        List<SelectListItem> preloadedGroups = new List<SelectListItem>();

        public SignupApplicationData(IEnumerable<CourseVM> coursesList,SignupApplication application)
        {
            _courses          = coursesList;
            _application      = application;
        }

        public ClassesSchedule Schedule
        {
            get { return _application.Schedule; }
        }

        public IEnumerable<SelectListItem> Refferals
        {
            get
            {
                List<SelectListItem> result = new List<SelectListItem>();
                result.Add(new SelectListItem() { Selected = true, Text = "Выберите источник", Value = String.Empty });
                result.Add(new SelectListItem() { Selected = false, Text = "Поиск в интернете", Value = "Поиск в интернете" });
                result.Add(new SelectListItem() { Selected = false, Text = "Получил листовку", Value = "Получил листовку" });
                result.Add(new SelectListItem() { Selected = false, Text = "Увидел плакат", Value = "Увидел плакат" });
                result.Add(new SelectListItem() { Selected = false, Text = "Объявление в газете", Value = "Объявление в газете" });
                result.Add(new SelectListItem() { Selected = false, Text = "Рекомендации друзей", Value = "Рекомендации друзей" });
                result.Add(new SelectListItem() { Selected = false, Text = "Ссылки на ХФ", Value = "Ссылки с ХФ" });
                result.Add(new SelectListItem() { Selected = false, Text = "Ссылки из соц. сетей", Value = "Ссылки с соц. сетей" });
                return result;
            }
        }

        public void SetPrelopadedGroups(ICollection<CourseFlowVM> flows)
        {
            foreach (CourseFlowVM f in flows)
            {
                this.preloadedGroups.Add(new SelectListItem() { Text = f.CustomName, Value = f.FlowId.ToString() });
            }
        }

        public IEnumerable<SelectListItem> Groups
        {
            get
            {
                if (this.preloadedGroups.Count > 0)
                    return this.preloadedGroups;


                List<SelectListItem> results = new List<SelectListItem>();
                results.Add(new SelectListItem() { Selected = true, Text = "Выберите группу", Value = String.Empty });
                return results;
            }
        }

        public SignupApplication Application
        {
            get
            {
                return _application;
            }
        }


   

        public IEnumerable<SelectListItem> Courses
        {
            get 
            {
                List<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem() { Selected = true, Text = "Выберите курс", Value = String.Empty});
                foreach (CourseVM course in _courses)
                    items.Add(new SelectListItem() { Selected = false, Text = course.CourseName, Value = course.CourseId.ToString() });

                return items.AsReadOnly();
            }
        }


        private IEnumerable<CourseVM> _courses;
        private SignupApplication     _application;
    }

    public enum ApplicationStatus
    {
        Sent,
        Approved,
        Canceled,
        Student,
        PastStudent
    }

    public class SignupApplication : OfflineMessage
    {
        public SignupApplication()
        {
            this.Schedule = new ClassesSchedule();
        }


        public ClassesSchedule Schedule {get;set;}
        public int             CourseId { get; set; }
        [Required(ErrorMessage = "Укажите вашу фамилию")]
        [StringLength(32,ErrorMessage = "Недопустимая минимальная длинна фамилии",MinimumLength=2)]
        //[RegularExpression(@"^[a-zA-Z][a-zA-Z0-9-_\.]{1,20}$", ErrorMessage = "Фамилия может содержать только символы")]
        public String LastName          { get; set; }

        [Required(ErrorMessage = "Выберите интересующий курс")]
        public int SelectedCourseId { get; set; }
        [Required(ErrorMessage = "Укажите время занятий")]
        public int SelectedGroupId { get; set; }
        [Required(ErrorMessage = "Укажите источник информации")]
        public String SelectedReferral { get; set; }

        public ApplicationStatus Status { get; set; }

        public Guid AccessToken { get; set; }

        public int FlowId { get; set; }

        public String AccessTokenEncoded
        {
            get
            {
                return GuidHelper.GuidToBase64(this.AccessToken);
            }
        }

        public String DisplayText
        {
            get { return String.Format("{0} {1}({2})", this.LastName, this.Name, this.Email); }
        }

        [JsonIgnore]
        public String JsonString
        {
            get { return Newtonsoft.Json.JsonConvert.SerializeObject(this); }
        }
    }


    public class AjaxOperationResult
    {
        public static AjaxOperationResult Ok
        {
            get
            {
                return new AjaxOperationResult() { ErrorMessage = String.Empty, Passed = true };
            }
        }

        private AjaxOperationResult()
        {
        }

        public AjaxOperationResult(System.Web.Mvc.ModelStateDictionary state)
        {
            List<String> errors = new List<string>();
            foreach (var s in state)
            {
                foreach (var error in s.Value.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }

            if (errors.Count > 0)
            {
                this.Passed       = false;
                this.ErrorMessage = "Ошибка ввода. Проверте корректность указанных данных";
            }
            else
            {
                this.Passed       = true;
                this.ErrorMessage = String.Empty;
            }
        }

        public AjaxOperationResult(Exception e)
        {
            this.Passed       = false;
            this.ErrorMessage = e.Message;
        }

        public bool   Passed       { get; set; }
        public String ErrorMessage {get; set;}
    }
}
