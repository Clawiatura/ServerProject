using System;
using System.Collections.Generic;

namespace ServerInProject.Models;

public partial class Payment
{
    public int ОплатаId { get; set; }

    public int БилетId { get; set; }

    public int IdКлиента { get; set; }

    public DateTime ДатаОплаты { get; set; }

    public decimal Сумма { get; set; }

    public string Статус { get; set; } = null!;

    public virtual Клиент IdКлиентаNavigation { get; set; } = null!;

    public virtual Ticket Билет { get; set; } = null!;
}
