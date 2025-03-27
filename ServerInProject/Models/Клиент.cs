using System;
using System.Collections.Generic;

namespace ServerInProject.Models;

public partial class Клиент
{
    public int IdКлиента { get; set; }

    public string Имя { get; set; } = null!;

    public string Фамилия { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<Бронирование> Бронированиеs { get; set; } = new List<Бронирование>();
}
