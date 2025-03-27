using ServerInProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Search.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ServerInProject.Contollers
{
    class TicketController
    {
        public async static void AddTicket(string json, HttpListenerContext context)
        {
            using (ProrjdContext db = new ProrjdContext())
            {
                СвободныеМаршруты ticket = JsonSerializer.Deserialize<СвободныеМаршруты>(json);




                
                await db.SaveChangesAsync();
                Respond(context, "OK");


            }
        }




        public async static void GetTicketById(int id, HttpListenerContext context)
        {

            using (ProrjdContext db = new ProrjdContext())
            {
                var ticket = await db.Tickets.FindAsync(id);

                if (ticket == null)
                {
                    Respond(context, "Not Found", HttpStatusCode.NotFound);
                }
                else

                {
                    string json = JsonSerializer.Serialize(ticket);
                    Respond(context, json, HttpStatusCode.OK, "application/json");
                }


            }
        }





        public async static void DeleteTicket(int id, HttpListenerContext context)
        {
            using (ProrjdContext db = new ProrjdContext())
            {
                var ticket = await db.Tickets.FindAsync(id);

                if (ticket == null)
                {
                    Respond(context, "Not Found", HttpStatusCode.NotFound);
                }
                else
                {
                    db.Tickets.Remove(ticket);
                    await db.SaveChangesAsync();
                    Respond(context, "OK");

                }



            }
        }
        public async static Task SearchTickets(string json, HttpListenerContext context)
        {
            using (ProrjdContext db = new ProrjdContext())
            {
                try
                {

                    {
                        СвободныеМаршруты? person = JsonSerializer.Deserialize<СвободныеМаршруты>(json);


                        await db.SaveChangesAsync();
                        var response = context.Response;
                        string responseText = json;
                        byte[] buffer = Encoding.UTF8.GetBytes(responseText);
                        response.ContentLength64 = buffer.Length;
                        response.ContentType = "text/html";
                        response.ContentEncoding = Encoding.UTF8;
                        using Stream output = response.OutputStream;
                        await output.WriteAsync(buffer);
                        await output.FlushAsync();
                        Console.WriteLine("+");
                    }
                }
                catch
                {
                    Console.WriteLine("1");
                }

            }
        }



        private static async Task SendErrorResponse(HttpListenerResponse response, string message)
        {

            string errorJson = JsonSerializer.Serialize(new { error = message });
            byte[] buffer = Encoding.UTF8.GetBytes(errorJson);
            response.ContentLength64 = buffer.Length;
            response.ContentType = "application/json";


            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);

        }



        // DTO для параметров поиска
        public class SearchRequestData
        {
            public int IdГородаОтправления { get; set; }
            public int IdГородаПрибытия { get; set; }
            public DateTime ДатаОтправления { get; set; }
            // ... другие свойства для поиска
        }




        private static async void Respond(HttpListenerContext context, string responseText, HttpStatusCode statusCode = HttpStatusCode.OK, string contentType = "text/html")
        {
            try
            {

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseText);
                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = contentType;
                context.Response.ContentLength64 = buffer.Length;

                using (var output = context.Response.OutputStream)
                {
                    await output.WriteAsync(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"123: {ex.Message}");
            }



        }
    }





}


