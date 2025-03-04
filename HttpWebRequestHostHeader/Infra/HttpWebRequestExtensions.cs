using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebRequestHostHeader.Infra
{
    public static class HttpWebRequestExtensions
    {
        static string[] RestrictedHeaders = new string[] {
            "Accept",
            "Connection",
            "Content-Length",
            "Content-Type",
            "Date",
            "Expect",
            "Host",
            "If-Modified-Since",
            "Keep-Alive",
            "Proxy-Connection",
            "Range",
            "Referer",
            "Transfer-Encoding",
            "User-Agent"
    };
        //Создаём словарь со строковыми ключами и PropertyInfo в качестве значения, с функцией сравнения равенства ключей без учета регистра для текущего языка.
        //https://learn.microsoft.com/ru-ru/dotnet/api/system.collections.generic.dictionary-2.-ctor?view=net-8.0#system-collections-generic-dictionary-2-ctor(system-collections-generic-iequalitycomparer((-0))) 
        static Dictionary<string, PropertyInfo> HeaderProperties = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// В конструкторе перебираем захардкоженный строковый массив, каджый элемент которого представляет http-заголовок, преобразуем его к PascalCase C# и
        /// подставляем в метод typeof(HttpWebRequest).GetProperty(propertyName) с целью извлечения соответствующего PropertyInfo. Заполняем словарь HeaderProperties.
        /// </summary>
        static HttpWebRequestExtensions()
        {
            Type type = typeof(HttpWebRequest);
            foreach (string header in RestrictedHeaders)
            {
                string propertyName = header.Replace("-", "");
                PropertyInfo headerProperty = type.GetProperty(propertyName);
                HeaderProperties[header] = headerProperty;
            }
        }
        /// <summary>
        /// Метод, который устанавливает в переданном а аргументе запросе клиента HttpWebRequest request правильные значения заголовков, используя
        /// встроенное в HttpWebRequest форматирование заголовков не строковых типов.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetRawHeader(this HttpWebRequest request, string name, string value)
        {
            if (HeaderProperties.ContainsKey(name))
            {
                PropertyInfo property = HeaderProperties[name];
                if (property.PropertyType == typeof(DateTime))
                    property.SetValue(request, DateTime.Parse(value), null);
                else if (property.PropertyType == typeof(bool))
                    property.SetValue(request, Boolean.Parse(value), null);
                else if (property.PropertyType == typeof(long))
                    property.SetValue(request, Int64.Parse(value), null);
                else
                    property.SetValue(request, value, null);
            }
            else
            {
                request.Headers[name] = value;
            }
        }
    }
}
