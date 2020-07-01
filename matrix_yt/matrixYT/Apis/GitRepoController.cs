using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using  matrixYT.Models;
using matrixYT.Infrastructure;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using matrixYT.ClientApp.src.app.core;
using Microsoft.AspNetCore.Http;

namespace matrixYT.Apis
{
    [Route("api/repos")]
    public class matrixYTController:Controller

    {
       // GET api/repos
        [HttpGet("{id}", Name = "GetReposRoute")]
        [NoCache]
        [ProducesResponseType(typeof(Repo), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Repos(string id)
        { 
                       
               try
               {
                    var repositories = await DeserializeOptimizedFromStreamCallAsync(CancellationToken.None,id);               

                     return Ok(repositories);

               }   
               catch(Exception exp){

                   return BadRequest(new ApiResponse { Status = false });

               }
                   
                
                
       } 

       // POST api/repos/
        //[Route("api/repos")]
        [HttpPost]
        [NoCache]
        //[Produces("application/json")]
        [ProducesResponseType(typeof(ApiResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> BookmarkRepo([FromBody]Bookmark Bookmark)
        {            
            try
            {
                var status = await BookmarkSession(Bookmark); 

                if (!status)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return Ok(new ApiResponse { Status = true });
            }

            catch (Exception exp)
            {
                //_Logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }                             
                
       }   

       private async Task<bool>  BookmarkSession(Bookmark bookmark_session)
       {
            Dictionary<string,Dictionary<string,string>> bookmarkDic = new Dictionary<string, Dictionary<string, string>>();  

            bookmarkDic.Add(bookmark_session.Id,new Dictionary<string, string>());   

            bookmarkDic[bookmark_session.Id].Add(bookmark_session.Name,bookmark_session.Avatar);
       

            var status =   HttpContext.Session.Set(bookmark_session.Id,bookmarkDic);  

            if (!status)
            {
                return false;
            }

            return true;
       }  

        private static async Task<Repo> DeserializeOptimizedFromStreamCallAsync(CancellationToken cancellationToken,string id)
        {

            string apiUrl = String.Format("https://api.github.com/search/repositories?q={0}",id); 

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; " +  "Windows NT 5.2; .NET CLR 1.0.3705;)");

                    client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
                    {
                        NoCache = true
                    };           
                                  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

                    HttpResponseMessage  response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                
                    var stream = await response.Content.ReadAsStreamAsync();                    

                    if (response.IsSuccessStatusCode){

                        return   DeserializeJsonFromStream<Repo>(stream);                      
                    }

                    var content = await StreamToStringAsync(stream);

                    throw new ApiException
                    {

                    StatusCode = (int)response.StatusCode,

                    Content = content   

                    };
                }
        }  

        public class ApiException : Exception
        {
            public int StatusCode { get; set; }

            public string Content { get; set; }
        } 

        private static T  DeserializeJsonFromStream<T>(Stream stream)
        {
                if (stream == null || stream.CanRead == false)
                    return default(T);

                using (var sr = new StreamReader(stream))
                using (var jtr = new JsonTextReader(sr))
                {
                    var js = new Newtonsoft.Json.JsonSerializer();
                    var searchResult = js.Deserialize<T>(jtr);
                    return searchResult;
                }
        }

        private static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
                using (var sr = new StreamReader(stream))
                    content = await sr.ReadToEndAsync();

            return content;
        }
        
        
    }

}