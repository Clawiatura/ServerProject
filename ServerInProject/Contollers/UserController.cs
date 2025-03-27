using ServerInProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ServerInProject.Contollers
{
     

    class UserController
    {
        public async static Task AddClient(string json, HttpListenerContext context)
        {
            using (ProrjdContext db = new ProrjdContext())
            {
                Клиент? person = JsonSerializer.Deserialize<Клиент>(json);
                db.Клиентs.Add(new Клиент()
                {


                    Имя = person.Имя,
                    Email = person.Email,
                    PasswordHash = person.PasswordHash,
                    Фамилия = person.Фамилия
                });

                await db.SaveChangesAsync();
                var response = context.Response;
                string responseText = "OK";
                byte[] buffer = Encoding.UTF8.GetBytes(responseText);
                response.ContentLength64 = buffer.Length;
                response.ContentType = "text/html";
                response.ContentEncoding = Encoding.UTF8;
                using Stream output = response.OutputStream;
                await output.WriteAsync(buffer);
                await output.FlushAsync();
                Console.WriteLine("Запрос обработан");
            }
        }
        public async static void deleteClient(string query, HttpListenerContext context)
        {
            using (ProrjdContext db = new ProrjdContext())
            {
                Клиент desClient = JsonSerializer.Deserialize<Клиент>(query)!;
                Клиент client = db.Клиентs.FirstOrDefault(p => p.IdКлиента == desClient.IdКлиента)!;
                var response = context.Response;
                string responseText = "";

                if (client != null)
                {
                    db.Remove(client);
                    await db.SaveChangesAsync();
                    responseText = "Yes";
                }
                else responseText = "No";
                byte[] buffer = Encoding.UTF8.GetBytes(responseText);
                response.ContentLength64 = buffer.Length;
                response.ContentType = "text/html";
                response.ContentEncoding = Encoding.UTF8;
                using Stream output = response.OutputStream;
                await output.WriteAsync(buffer);
                await output.FlushAsync();
                Console.WriteLine("Запрос обработан");

            }
        }


        internal static string GetAllClients()
        {
            throw new NotImplementedException();
        }

        internal static void UpdateClient(int clientId, string requestBody, HttpListenerContext context)
        {
            throw new NotImplementedException();
        }
    }
}
