﻿using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace System.IO.Compression
{
   public class HttpStreamZip
   {

      HttpClient httpClient { get; set; }
      public HttpStreamZip() : this(new HttpClient()) { }
      public HttpStreamZip(HttpClient httpClient)
      {
         this.httpClient = httpClient;
         this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
      }

      long ContentLength { get; set; }
      private async Task<bool> RefreshContentLength(string httpUrl)
      {
         try
         {
            var httpMessage = await this.httpClient.GetAsync(httpUrl, HttpCompletionOption.ResponseHeadersRead);
            if (!httpMessage.IsSuccessStatusCode) { return false; }
            this.ContentLength = httpMessage.Content.Headers
               .GetValues("Content-Length")
               .Select(x => long.Parse(x))
               .FirstOrDefault();
            return true;
         }
         catch (Exception) { throw; }
      }

   }
}