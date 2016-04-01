using CustomFields.DateTimeField.ViewModels;
using JetBrains.Annotations;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using CustomFields.DateTimeField.Settings;
using Orchard.Localization;
using System;


namespace CustomFields.DateTimeField.Drivers
{
    [UsedImplicitly]
    public class DateTimeFieldDriver : ContentFieldDriver<Fields.DateTimeField>
    {
        public IOrchardServices Services { get; set; }

        // EditorTemplates/Fields/Custom.DateTime.cshtml
        private const string TemplateName = "Fields/Custom.DateTime";

        public DateTimeFieldDriver(IOrchardServices services)
        {
            Services = services;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        private static string GetPrefix(ContentField field, ContentPart part)
        {
            // handles spaces in field names
            return (part.PartDefinition.Name + "." + field.Name)
                   .Replace(" ", "_");
        }

        protected override DriverResult Display(
            ContentPart part, Fields.DateTimeField field,
            string displayType, dynamic shapeHelper)
        {

            var settings = field.PartFieldDefinition.Settings
                                .GetModel<DateTimeFieldSettings>();
            var value = field.DateTime;

            return ContentShape("Fields_Custom_DateTime", // key in Shape Table
                    field.Name, // used to differentiate shapes in placement.info overrides, e.g. Fields_Common_Text-DIFFERENTIATOR
                                // this is the actual Shape which will be resolved
                                // (Fields/Custom.DateTime.cshtml)
                    s =>
                    s.Name(field.Name)
                     .Date(value.HasValue ?
                         value.Value.ToLocalTime().ToShortDateString() :
                         String.Empty)
                     .Time(value.HasValue ?
                         value.Value.ToLocalTime().ToShortTimeString() :
                         String.Empty)
                     .ShowDate(
                         settings.Display == DateTimeFieldDisplays.DateAndTime ||
                         settings.Display == DateTimeFieldDisplays.DateOnly)
                     .ShowTime(
                         settings.Display == DateTimeFieldDisplays.DateAndTime ||
                         settings.Display == DateTimeFieldDisplays.TimeOnly)
                );
        }

        protected override DriverResult Editor(ContentPart part,
                                               Fields.DateTimeField field,
                                               dynamic shapeHelper)
        {

            var settings = field.PartFieldDefinition.Settings
                                .GetModel<DateTimeFieldSettings>();
            var value = field.DateTime;

            if (value.HasValue)
            {
                value = value.Value.ToLocalTime();
            }

            var viewModel = new DateTimeFieldViewModel
            {
                Name = field.Name,
                Date = value.HasValue ?
                       value.Value.ToLocalTime().ToShortDateString() : "",
                Time = value.HasValue ?
                       value.Value.ToLocalTime().ToShortTimeString() : "",
                ShowDate =
                    settings.Display == DateTimeFieldDisplays.DateAndTime ||
                    settings.Display == DateTimeFieldDisplays.DateOnly,
                ShowTime =
                    settings.Display == DateTimeFieldDisplays.DateAndTime ||
                    settings.Display == DateTimeFieldDisplays.TimeOnly

            };

            return ContentShape("Fields_Custom_DateTime_Edit",
                () => shapeHelper.EditorTemplate(
                          TemplateName: TemplateName,
                          Model: viewModel,
                          Prefix: GetPrefix(field, part)));
        }

        protected override DriverResult Editor(ContentPart part,
                                               Fields.DateTimeField field,
                                               IUpdateModel updater,
                                               dynamic shapeHelper)
        {

            var viewModel = new DateTimeFieldViewModel();

            if (updater.TryUpdateModel(viewModel,
                                       GetPrefix(field, part), null, null))
            {
                DateTime value;

                var settings = field.PartFieldDefinition.Settings
                                    .GetModel<DateTimeFieldSettings>();
                if (settings.Display == DateTimeFieldDisplays.DateOnly)
                {
                    viewModel.Time = DateTime.Now.ToShortTimeString();
                }

                if (settings.Display == DateTimeFieldDisplays.TimeOnly)
                {
                    viewModel.Date = DateTime.Now.ToShortDateString();
                }

                if (DateTime.TryParse(
                        viewModel.Date + " " + viewModel.Time, out value))
                {
                    field.DateTime = value.ToUniversalTime();
                }
                else
                {
                    updater.AddModelError(GetPrefix(field, part),
                                          T("{0} is an invalid date and time",
                                          field.Name));
                    field.DateTime = null;
                }
            }

            return Editor(part, field, shapeHelper);
        }

        protected override void Importing(ContentPart part, Fields.DateTimeField field,
            ImportContentContext context)
        {

            var importedText = context.Attribute(GetPrefix(field, part), "DateTime");
            if (importedText != null)
            {
                field.Storage.Set(null, importedText);
            }
        }

        protected override void Exporting(ContentPart part, Fields.DateTimeField field,
            ExportContentContext context)
        {
            context.Element(GetPrefix(field, part))
                .SetAttributeValue("DateTime", field.Storage.Get<string>(null));
        }
    }
}