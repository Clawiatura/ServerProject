

using System;
using System.Net;
using System.IO;
using System.Text;
using System.Text.Json;
using ServerInProject.Contollers;
using Newtonsoft.Json;
using ServerInProject.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;



public class Server
{
    public static async Task Main(string[] args)
    {
        HttpListener server = new HttpListener();
        server.Prefixes.Add("http://127.0.0.1:8888/api/");
        server.Start();
        Console.WriteLine("Сервер работает");



        while (true)
        {
            Console.WriteLine("Сервер ждет запрос");
            var context = await server.GetContextAsync();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            string method = request.HttpMethod;
            //string rawUrl = request.RawUrl; 
            //string[] urlSegments = rawUrl.Split('/', StringSplitOptions.RemoveEmptyEntries);
            //string table = urlSegments.Length > 1 ? urlSegments[1] : "";

            string? table = context.Request.Headers[0];
            response.ContentType = "application/json";

            try
            {
                string requestBody = await new StreamReader(request.InputStream, request.ContentEncoding).ReadToEndAsync();

                switch (method)
                {
                    case "GET":
                        if (table == "clients")
                        {
                            string jsonResponse = UserController.GetAllClients();
                            await WriteJsonResponse(response, jsonResponse);
                        }
                        else if (table == "ticket")
                        {


                            await TicketController.SearchTickets(requestBody, context);

                        }
                        break;

                    case "POST":
                        if (table == "clients")
                        {
                            await UserController.AddClient(requestBody, context);
                        }
                        break;



                        //case "PUT":
                        //    if (table == "clients" && urlSegments.Length > 2) 
                        //    {
                        //        int clientId = int.Parse(urlSegments[2]); 
                        //        UserController.UpdateClient(clientId, requestBody, context);
                        //    }


                        //        break;

                        //    case "DELETE":
                        //        if (table == "clients" && urlSegments.Length > 2) 
                        //        {
                        //            int clientId = int.Parse(urlSegments[2]);
                        //            UserController.deleteClient(clientId.ToString(), context); 
                        //        }

                        //        break;

                        //    default:
                        //        response.StatusCode = (int)HttpStatusCode.NotFound;
                        //        break;
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                string errorJson = JsonSerializer.Serialize(new { error = ex.Message });
                await WriteJsonResponse(response, errorJson);
            }
            finally
            {
                response.Close();
            }
        }

        server.Stop();
    }

    private static async Task WriteJsonResponse(HttpListenerResponse response, string json)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(json);
        response.ContentLength64 = buffer.Length;
        await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
    }
}

//case "PUT":
//    if (table == "clients" && urlSegments.Length > 2) 
//    {
//        int clientId = int.Parse(urlSegments[2]); 
//        UserController.UpdateClient(clientId, requestBody, context);
//    }


//        break;

//    case "DELETE":
//        if (table == "clients" && urlSegments.Length > 2) 
//        {
//            int clientId = int.Parse(urlSegments[2]);
//            UserController.deleteClient(clientId.ToString(), context); 
//        }

//        break;

//    default:
//        response.StatusCode = (int)HttpStatusCode.NotFound;
//        break;




