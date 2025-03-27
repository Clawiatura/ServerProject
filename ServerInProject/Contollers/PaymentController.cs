using ServerInProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServerInProject.Contollers
{
    public class PaymentController
    {
        
        public static async Task AddPayment(string json, HttpListenerContext context)
        {
            using (ProrjdContext db = new ProrjdContext())
            {
                try
                {
                    Payment payment = JsonSerializer.Deserialize<Payment>(json);

                    
                    if (payment == null)
                    {
                        Respond(context, "Invalid JSON", HttpStatusCode.BadRequest);
                        return;
                    }

                    bool paymentSuccessful = await ProcessPayment(payment);

                    if (paymentSuccessful)
                    {
                        db.Payments.Add(payment);
                        await db.SaveChangesAsync();
                        Respond(context, "OK");
                    }
                    else
                    {
                        Respond(context, "Payment Failed", HttpStatusCode.BadRequest);
                    }
                }
                catch (JsonException ex) 
                {
                    Respond(context, "Invalid JSON: " + ex.Message, HttpStatusCode.BadRequest);
                }
                catch (Exception ex) 
                {
                    Respond(context, "Error: " + ex.Message, HttpStatusCode.InternalServerError);
                }

            }
        }

        public static async Task GetPaymentById(int id, HttpListenerContext context)
        {
            using (ProrjdContext db = new ProrjdContext())
            {
                var payment = await db.Payments.FindAsync(id);

                if (payment == null)
                {
                    Respond(context, "Not Found", HttpStatusCode.NotFound);
                }
                else
                {
                    string json = JsonSerializer.Serialize(payment);
                    Respond(context, json, HttpStatusCode.OK, "application/json");
                    //
                }
            }
        }




        private static async Task Respond(HttpListenerContext context, string responseText, HttpStatusCode statusCode = HttpStatusCode.OK, string contentType = "text/html")
        {
            var response = context.Response;
            byte[] buffer = Encoding.UTF8.GetBytes(responseText);
            response.ContentLength64 = buffer.Length;
            response.ContentType = contentType;
            response.StatusCode = (int)statusCode;
            response.ContentEncoding = Encoding.UTF8;

            try
            {
                await using (Stream output = response.OutputStream)
                {
                    await output.WriteAsync(buffer, 0, buffer.Length); // Запись всего буфера
                }

                Console.WriteLine($"Запрос обработан. Status: {statusCode}");
            }
            catch (Exception ex) // обработка исключений при записи ответа
            {
                Console.Error.WriteLine($"Ошибка при отправке ответа: {ex.Message}");

            }
        }


        private static async Task<bool> ProcessPayment(Payment payment)
        {
            // Здесь должна быть логика взаимодействия с платежным шлюзом
            // ...
            await Task.Delay(1000); // Имитация задержки обработки платежа
            return true;
        }
    }
}
