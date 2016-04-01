﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Descriptors;
using Orchard.FileSystems.VirtualPath;

namespace Orchard.Layouts.Services {
    public class ContentFieldDisplay : ContentDisplayBase, IContentFieldDisplay {
        private readonly IEnumerable<IContentFieldDriver> _contentFieldDrivers;

        public ContentFieldDisplay(
            IShapeFactory shapeFactory,
            Lazy<IShapeTableLocator> shapeTableLocator, 
            RequestContext requestContext,
            IVirtualPathProvider virtualPathProvider,
            IWorkContextAccessor workContextAccessor, 
            IEnumerable<IContentFieldDriver> contentFieldDrivers) 
            : base(shapeFactory, shapeTableLocator, requestContext, virtualPathProvider, workContextAccessor) {

            _contentFieldDrivers = contentFieldDrivers;
        }

        public override string DefaultStereotype {
            get { return "ContentField"; }
        }

        public dynamic BuildDisplay(IContent content, ContentField field, string displayType, string groupId) {
            ContentManagement.Handlers.BuildDisplayContext context = BuildDisplayContext(content, displayType, groupId);
            context.Field = field;

            var drivers = GetFieldDrivers(field.FieldDefinition.Name);

            drivers.Invoke(driver => {
                var result = driver.BuildDisplayShape(context);
                if (result != null)
                    result.Apply(context);
            }, Logger);

            return context.Shape;
        }

        public dynamic BuildEditor(IContent content, ContentField field, string groupId) {
            var context = BuildEditorContext(content, groupId);
            var drivers = GetFieldDrivers(field.FieldDefinition.Name);

            drivers.Invoke(driver => {
                var result = driver.BuildEditorShape(context);
                if (result != null)
                    result.Apply(context);
            }, Logger);

            return context.Shape;
        }

        public dynamic UpdateEditor(IContent content, ContentField field, IUpdateModel updater, string groupInfoId) {
            var context = UpdateEditorContext(content, updater, groupInfoId);
            var drivers = GetFieldDrivers(field.FieldDefinition.Name);

            drivers.Invoke(driver => {
                var result = driver.UpdateEditorShape(context);
                if (result != null)
                    result.Apply(context);
            }, Logger);
            
            return context.Shape;
        }

        private IEnumerable<IContentFieldDriver> GetFieldDrivers(string fieldName) {
            return _contentFieldDrivers.Where(x => x.GetType().BaseType.GenericTypeArguments[0].Name == fieldName);
        }
    }
}