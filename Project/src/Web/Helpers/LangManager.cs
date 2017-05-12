using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Resources;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;


namespace Web.Helpers
{
    public class LangManagers : IStringLocalizer
    {
        private readonly IStringLocalizer _resourceManagerStringLocalizer;

        public LangManagers(IStringLocalizer resourceManagerStringLocalizer)
        {
            _resourceManagerStringLocalizer = resourceManagerStringLocalizer;
        }

        LocalizedString IStringLocalizer.this[string name] =>new LocalizedString(name,"ffff");

        LocalizedString IStringLocalizer.this[string name, params object[] arguments] => throw new NotImplementedException();

        IEnumerable<LocalizedString> IStringLocalizer.GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        IStringLocalizer IStringLocalizer.WithCulture(CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LangManagerss : IViewLocalizer
    {
        public LocalizedHtmlString this[string name] => new LocalizedHtmlString(name, "dddd");

        public LocalizedHtmlString this[string name, params object[] arguments] => throw new NotImplementedException();

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        public LocalizedString GetString(string name)
        {
           return new LocalizedString(name,DateTime.Now.ToString());
        }

        public LocalizedString GetString(string name, params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public IHtmlLocalizer WithCulture(CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



    public static class LangManager
    {
     


        public static string GetString(object name)
        {
            var lang = "";

            var a = name?.ToString().Trim();

            if (string.IsNullOrEmpty(a))
            {
                return string.Empty;
            }

            // 资源文件里面寻找
            var re = new ResourceManager(typeof(Resources.Lang));
            lang = re.GetString(a);

            // 数据库中寻找
            if (string.IsNullOrEmpty(lang))
            {
               

            }



            // 网上寻找
            if (string.IsNullOrEmpty(lang))
            {

                // 本地数据找不到的 检测语言去网上翻译
                // http://fanyi.youdao.com/openapi?path=data-mode
                // API key：597913687
                // keyfrom：wjw1 - mpms
                // http://fanyi.youdao.com/openapi.do?keyfrom=wjw1-mpms&key=597913687&type=data&doctype=<doctype>&version=1.1&q=要翻译的文本
                //            版本：1.1，请求方式：get，编码方式：utf - 8
                //主要功能：中英互译，同时获得有道翻译结果和有道词典结果（可能没有）
                //参数说明：
                //　type - 返回结果的类型，固定为data

                //doctype - 返回结果的数据格式，xml或json或jsonp

                //version - 版本，当前最新版本为1.1

                //q - 要翻译的文本，必须是UTF - 8编码，字符长度不能超过200个字符，需要进行urlencode编码

                //only - 可选参数，dict表示只获取词典数据，translate表示只获取翻译数据，默认为都获取
                //注： 词典结果只支持中英互译，翻译结果支持英日韩法俄西到中文的翻译以及中文到英语的翻译
                //errorCode：

                //0 - 正常

                //20 - 要翻译的文本过长

                //30 - 无法进行有效的翻译

                //40 - 不支持的语言类型

                //50 - 无效的key

                //60 - 无词典结果，仅在获取词典结果生效

                //            http://fanyi.youdao.com/openapi.do?keyfrom=wjw1-mpms&key=597913687&type=data&doctype=json&version=1.1&q=good
                //            {
                //                "errorCode":0
                //    "query":"good",
                //    "translation":["好"], // 有道翻译
                //    "basic":{ // 有道词典-基本词典
                //        "phonetic":"gʊd"
                //        "uk-phonetic":"gʊd" //英式发音
                //        "us-phonetic":"ɡʊd" //美式发音
                //        "explains":[
                //            "好处",
                //            "好的"
                //            "好"
                //        ]
                //    },
                //    "web":[ // 有道词典-网络释义
                //        {
                //            "key":"good",
                //            "value":["良好","善","美好"]
                //},
                //        {...}
                //    ]
                //}

                // 英文-》中文
                var postUrl = "http://fanyi.youdao.com/openapi.do?keyfrom=wjw1-mpms&key=597913687&type=data&doctype=json&version=1.1&q="+a.SplitUpperCaseToString();
                try
                {
                    var httpClient = new HttpClient();
                    var responseJson = httpClient.GetAsync(postUrl).Result.Content.ReadAsStringAsync().Result;
                  
                    var obj = Newtonsoft.Json.Linq.JObject.Parse(responseJson);
                    
                    // translation
                    if (obj != null) lang = obj["translation"][0].ToString();


                    // 存回导数据库里面

               
                }
                catch (Exception)
                {
                  
                }

            }
            


            return string.IsNullOrEmpty(lang) ? a.SplitUpperCaseToString() : lang;
        }

        public static string SplitUpperCaseToString(this string source)
        {
            return source == null
                ? null
                : string.Join(" ", source.SplitUpperCase());
        }

        public static string[] SplitUpperCase(this string source)
        {
            if (source == null)
                return new string[] { }; //Return empty array.

            if (source.Length == 0)
                return new[] { "" };

            var words = new ArrayList();

            var wordStartIndex = 0;

            var letters = source.ToCharArray();
            var previousChar = char.MinValue;
            // Skip the first letter. we don't care what case it is.
            for (var i = 1; i < letters.Length; i++)
            {
                if (char.IsUpper(letters[i]) && !char.IsWhiteSpace(previousChar))
                {
                    //Grab everything before the current index.
                    words.Add(new String(letters, wordStartIndex, i - wordStartIndex));

                    wordStartIndex = i;
                }
                previousChar = letters[i];
            }
            //We need to have the last word.
            words.Add(new String(letters, wordStartIndex, letters.Length - wordStartIndex));

            //Copy to a string array.
            var wordArray = new string[words.Count];
            words.CopyTo(wordArray, 0);
            return wordArray;
        }
    }
}
