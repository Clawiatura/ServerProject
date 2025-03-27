using System;
using System.Collections.Generic;

namespace ServerInProject.Models;

public partial class Ticket
{
    public int БилетId { get; set; }

    public int НомерМеста { get; set; }

    public string ГородОтправления { get; set; } = null!;

    public string ГородПрибытия { get; set; } = null!;

    public int? IdКлиента { get; set; }
    public DateTime Дата { get; set; }

    public virtual Клиент? IdКлиентаNavigation { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
