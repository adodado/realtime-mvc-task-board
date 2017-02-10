using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace BootstrapSupport
{
    public static class DefaultScaffoldingExtensions
    {
        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        /// <param name="controllerType">Type of the controller.</param>
        /// <returns>System.String.</returns>
        public static string GetControllerName(this Type controllerType)
        {
            return controllerType.Name.Replace("Controller", String.Empty);
        }

        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        /// <param name="actionExpression">The action expression.</param>
        /// <returns>System.String.</returns>
        public static string GetActionName(this LambdaExpression actionExpression)
        {
            return ((MethodCallExpression) actionExpression.Body).Method.Name;
        }

        /// <summary>
        /// Visibles the properties.
        /// </summary>
        /// <param name="Model">The model.</param>
        /// <returns>PropertyInfo[][].</returns>
        public static PropertyInfo[] VisibleProperties(this IEnumerable Model)
        {
            Type elementType = Model.GetType().GetElementType();
            if (elementType == null)
            {
                elementType = Model.GetType().GetGenericArguments()[0];
            }
            return
                elementType.GetProperties().Where(info => info.Name != elementType.IdentifierPropertyName()).ToArray();
        }

        /// <summary>
        /// Visibles the properties.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>PropertyInfo[][].</returns>
        public static PropertyInfo[] VisibleProperties(this Object model)
        {
            return model.GetType().GetProperties().Where(info => info.Name != model.IdentifierPropertyName()).ToArray();
        }

        /// <summary>
        /// Gets the identifier value.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>RouteValueDictionary.</returns>
        public static RouteValueDictionary GetIdValue(this object model)
        {
            var v = new RouteValueDictionary();
            v.Add(model.IdentifierPropertyName(), model.GetId());
            return v;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>System.Object.</returns>
        public static object GetId(this object model)
        {
            return model.GetType().GetProperty(model.IdentifierPropertyName()).GetValue(model, new object[0]);
        }


        /// <summary>
        /// Identifiers the name of the property.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>System.String.</returns>
        public static string IdentifierPropertyName(this Object model)
        {
            return IdentifierPropertyName(model.GetType());
        }

        /// <summary>
        /// Identifiers the name of the property.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>System.String.</returns>
        public static string IdentifierPropertyName(this Type type)
        {
            if (type.GetProperties().Any(info => info.PropertyType.AttributeExists<KeyAttribute>()))
            {
                return
                    type.GetProperties().First(
                        info => info.PropertyType.AttributeExists<KeyAttribute>())
                        .Name;
            }
            else if (type.GetProperties().Any(p => p.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase)))
            {
                return
                    type.GetProperties().First(
                        p => p.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase)).Name;
            }
            return "";
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>System.String.</returns>
        public static string GetLabel(this PropertyInfo propertyInfo)
        {
            ModelMetadata meta = ModelMetadataProviders.Current.GetMetadataForProperty(null, propertyInfo.DeclaringType,
                                                                                       propertyInfo.Name);
            return meta.GetDisplayName();
        }

        /// <summary>
        /// To the separated words.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string ToSeparatedWords(this string value)
        {
            return Regex.Replace(value, "([A-Z][a-z])", " $1").Trim();
        }
    }

    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Attributes the exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AttributeExists<T>(this PropertyInfo propertyInfo) where T : class
        {
            var attribute = propertyInfo.GetCustomAttributes(typeof (T), false)
                                        .FirstOrDefault() as T;
            if (attribute == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Attributes the exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AttributeExists<T>(this Type type) where T : class
        {
            var attribute = type.GetCustomAttributes(typeof (T), false).FirstOrDefault() as T;
            if (attribute == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <returns>``0.</returns>
        public static T GetAttribute<T>(this Type type) where T : class
        {
            return type.GetCustomAttributes(typeof (T), false).FirstOrDefault() as T;
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>``0.</returns>
        public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : class
        {
            return propertyInfo.GetCustomAttributes(typeof (T), false).FirstOrDefault() as T;
        }

        /// <summary>
        /// Labels from type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>System.String.</returns>
        public static string LabelFromType(Type @type)
        {
            var att = GetAttribute<DisplayNameAttribute>(@type);
            return att != null
                       ? att.DisplayName
                       : @type.Name.ToSeparatedWords();
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="Model">The model.</param>
        /// <returns>System.String.</returns>
        public static string GetLabel(this Object Model)
        {
            return LabelFromType(Model.GetType());
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="Model">The model.</param>
        /// <returns>System.String.</returns>
        public static string GetLabel(this IEnumerable Model)
        {
            Type elementType = Model.GetType().GetElementType();
            if (elementType == null)
            {
                elementType = Model.GetType().GetGenericArguments()[0];
            }
            return LabelFromType(elementType);
        }
    }

    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Tries the partial.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="model">The model.</param>
        /// <returns>MvcHtmlString.</returns>
        public static MvcHtmlString TryPartial(this HtmlHelper helper, string viewName, object model)
        {
            try
            {
                return helper.Partial(viewName, model);
            }
            catch (Exception)
            {
            }
            return MvcHtmlString.Empty;
        }
    }
}