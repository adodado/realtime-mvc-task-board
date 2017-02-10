using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace BootstrapSupport
{
    public class ControlGroup : IDisposable
    {
        /// <summary>
        /// The HTML Helper
        /// </summary>
        private readonly HtmlHelper _html;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlGroup"/> class.
        /// </summary>
        /// <param name="html">The HTML.</param>
        public ControlGroup(HtmlHelper html)
        {
            _html = html;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _html.ViewContext.Writer.Write(_html.EndControlGroup());
        }
    }

    public static class ControlGroupExtensions
    {
        /// <summary>
        /// Begins the control group for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="modelProperty">The model property.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html,
                                                          Expression<Func<T, object>> modelProperty)
        {
            return BeginControlGroupFor(html, modelProperty, null);
        }

        /// <summary>
        /// Begins the control group for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="modelProperty">The model property.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html,
                                                          Expression<Func<T, object>> modelProperty,
                                                          object htmlAttributes)
        {
            return BeginControlGroupFor(html, modelProperty,
                                        HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Begins the control group for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="modelProperty">The model property.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html,
                                                          Expression<Func<T, object>> modelProperty,
                                                          IDictionary<string, object> htmlAttributes)
        {
            string propertyName = ExpressionHelper.GetExpressionText(modelProperty);
            return BeginControlGroupFor(html, propertyName, null);
        }

        /// <summary>
        /// Begins the control group for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html, string propertyName)
        {
            return BeginControlGroupFor(html, propertyName, null);
        }

        /// <summary>
        /// Begins the control group for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html, string propertyName,
                                                          object htmlAttributes)
        {
            return BeginControlGroupFor(html, propertyName, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Begins the control group for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html, string propertyName,
                                                          IDictionary<string, object> htmlAttributes)
        {
            var controlGroupWrapper = new TagBuilder("div");
            controlGroupWrapper.MergeAttributes(htmlAttributes);
            controlGroupWrapper.AddCssClass("control-group");
            string partialFieldName = propertyName;
            string fullHtmlFieldName =
                html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(partialFieldName);
            if (!html.ViewData.ModelState.IsValidField(fullHtmlFieldName))
            {
                controlGroupWrapper.AddCssClass("error");
            }
            string openingTag = controlGroupWrapper.ToString(TagRenderMode.StartTag);
            return MvcHtmlString.Create(openingTag);
        }

        /// <summary>
        /// Ends the control group.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString EndControlGroup(this HtmlHelper html)
        {
            return MvcHtmlString.Create("</div>");
        }

        /// <summary>
        /// Controls the group for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="modelProperty">The model property.</param>
        /// <returns>ControlGroup.</returns>
        public static ControlGroup ControlGroupFor<T>(this HtmlHelper<T> html, Expression<Func<T, object>> modelProperty)
        {
            return ControlGroupFor(html, modelProperty, null);
        }

        /// <summary>
        /// Controls the group for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="modelProperty">The model property.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>ControlGroup.</returns>
        public static ControlGroup ControlGroupFor<T>(this HtmlHelper<T> html, Expression<Func<T, object>> modelProperty,
                                                      object htmlAttributes)
        {
            string propertyName = ExpressionHelper.GetExpressionText(modelProperty);
            return ControlGroupFor(html, propertyName, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Controls the group for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>ControlGroup.</returns>
        public static ControlGroup ControlGroupFor<T>(this HtmlHelper<T> html, string propertyName)
        {
            return ControlGroupFor(html, propertyName, null);
        }

        /// <summary>
        /// Controls the group for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>ControlGroup.</returns>
        public static ControlGroup ControlGroupFor<T>(this HtmlHelper<T> html, string propertyName,
                                                      object htmlAttributes)
        {
            return ControlGroupFor(html, propertyName, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Controls the group for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>ControlGroup.</returns>
        public static ControlGroup ControlGroupFor<T>(this HtmlHelper<T> html, string propertyName,
                                                      IDictionary<string, object> htmlAttributes)
        {
            html.ViewContext.Writer.Write(BeginControlGroupFor(html, propertyName, htmlAttributes));
            return new ControlGroup(html);
        }
    }

    public static class Alerts
    {
        /// <summary>
        /// The success
        /// </summary>
        public const string SUCCESS = "success";
        /// <summary>
        /// The attention
        /// </summary>
        public const string ATTENTION = "attention";
        /// <summary>
        /// The error
        /// </summary>
        public const string ERROR = "error";
        /// <summary>
        /// The information
        /// </summary>
        public const string INFORMATION = "info";

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <value>All.</value>
        public static string[] ALL
        {
            get { return new[] {SUCCESS, ATTENTION, INFORMATION, ERROR}; }
        }
    }
}