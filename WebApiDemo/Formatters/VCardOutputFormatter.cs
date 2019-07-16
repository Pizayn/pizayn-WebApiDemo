using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using WebApiDemo.Model;

namespace WebApiDemo.Formatters
{
    public class VCardOutputFormatter:TextOutputFormatter
    {
        public VCardOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);

                
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var stringBuilder = new StringBuilder();
            if (context.Object is List<ProductModel>)
            {
                foreach (ProductModel model in context.Object as List<ProductModel>)
                {
                    FormatVcard(stringBuilder, model);
                }
            }
            else
            {
                var model = context.Object as ProductModel;
                FormatVcard(stringBuilder, model);
            }

            return response.WriteAsync(stringBuilder.ToString());
        }

        private static void FormatVcard(StringBuilder stringBuilder, ProductModel model)
        {
           
        
            stringBuilder.AppendLine("BEGIN:VCARD");
            stringBuilder.AppendLine("VERSION:2.1");
            stringBuilder.AppendLine($"PN:{model.ProductName}");
            stringBuilder.AppendLine($"CN:{model.CategoryName}");
            stringBuilder.AppendLine($"UID:{model.ProductId}\r\n");
            stringBuilder.AppendLine("END:VCARD");
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(ProductModel).IsAssignableFrom(type) || typeof(List<ProductModel>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }
    }
}
