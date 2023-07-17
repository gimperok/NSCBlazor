using System;
using System.Net;
using System.Net.Http.Json;
using System.Security.AccessControl;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace NSCBlazor.Client.Helpers
{
    public static class WebServiceHelper
    {
        public static readonly HttpClient GlobalHttpClient = new HttpClient();


        public async static Task<T> GetContentFromResponse<T>(HttpResponseMessage response)
        {
            if (response is null) return default(T);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var responseObject = await response.Content.ReadFromJsonAsync(typeof(T));
                    return (T)responseObject;
                }
                catch (Exception)
                {
                    return default(T);
                }            
            }
            else
                return default(T);
        }


        #region HTTP
        /// <summary>
        /// Создание HTTP-запроса на основании параметров
        /// </summary>
        /// <param name="httpMethodType">Тип HTTP-метода (Get, Post, Put)</param>
        /// <param name="requestUri">Строка с адресом запроса</param>
        /// <param name="content">Тело запроса (Body), если необходимо</param>
        /// <returns>Объект для отправки HTTP-запроса</returns>
        public static HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethodType, string requestUri, HttpContent content = null)
        {
            var request = new HttpRequestMessage(httpMethodType, requestUri);
            if (content != null)
                request.Content = content;
            return request;
        }

        /// <summary>
        /// Асинхронная отправка HTTP-запроса с получением результата указанного типа
        /// </summary>
        /// <typeparam name="T">Тип результата</typeparam>
        /// <param name="httpClient">Объект HTTP-клиента</param>
        /// <param name="httpMethodType">Тип HTTP-метода (Get, Post, Put)</param>
        /// <param name="requestUri">Строка с адресом запроса</param>
        /// <param name="content">Тело запроса (Body), если необходимо</param>
        /// <param name="jsonSerializerOptions">Настройки для сериализации результата</param>
        /// <returns>Объект задачи, возвращающей объект с результатом</returns>
        public static async Task<T> SendHttpRequestAsync<T>(HttpClient httpClient, HttpMethod httpMethodType, string requestUri, HttpContent content = null, JsonSerializerOptions jsonSerializerOptions = null)
        {
            HttpRequestMessage request = CreateHttpRequestMessage(httpMethodType, requestUri, content);
            return await SendHttpRequestAsync<T>(httpClient, request, jsonSerializerOptions);
        }


        /// <summary>
        /// Асинхронная отправка HTTP-запроса с получением результата указанного типа
        /// </summary>
        /// <typeparam name="T">Тип результата</typeparam>
        /// <param name="httpClient">Объект HTTP-клиента</param>
        /// <param name="request">Сообщение HTTP-запроса для отправки</param>
        /// <param name="jsonSerializerOptions">Настройки для сериализации результата</param>
        /// <returns>Объект задачи, возвращающей объект с результатом</returns>
        /// <exception cref="ArgumentNullException">Если объект HTTP-клиента null или объект HTTP-запроса null</exception>
        public static async Task<T> SendHttpRequestAsync<T>(HttpClient httpClient, HttpRequestMessage request, JsonSerializerOptions jsonSerializerOptions = null)
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (jsonSerializerOptions == null)
                jsonSerializerOptions = CreateDefaultSerializerOptions();

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var responseObject = await response.Content.ReadFromJsonAsync(typeof(T), jsonSerializerOptions);
                return (T)responseObject;
            }
            else
            {
                var responseString = await response.Content?.ReadAsStringAsync();

                return default(T);
            }
        }

        #endregion


        #region JSON
        /// <summary>
        /// Создание стандартных настроек сериализации/десериализации
        /// </summary>
        /// <returns></returns>
        public static JsonSerializerOptions CreateDefaultSerializerOptions()
        {
            var jsonSerializerWeb = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            jsonSerializerWeb.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            jsonSerializerWeb.WriteIndented = true;

            return jsonSerializerWeb;
        }

        public static StringContent CreateJsonContent(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
                jsonString = "{}";
            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }
        #endregion
    }
}