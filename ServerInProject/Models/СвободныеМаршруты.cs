using System;
using System.Collections.Generic;

namespace ServerInProject.Models;

public partial class СвободныеМаршруты
{
    public int IdМаршрута { get; set; }

    public int IdПеревозчика { get; set; }

    public int IdГородаОтправления { get; set; }

    public int IdГородаПрибытия { get; set; }

    public DateTime ДатаОтправления { get; set; }

    public decimal Цена { get; set; }

    public virtual Города IdГородаОтправленияNavigation { get; set; } = null!;

    public virtual Города IdГородаПрибытияNavigation { get; set; } = null!;

    public virtual Перевозчик IdПеревозчикаNavigation { get; set; } = null!;

    public virtual ICollection<Бронирование> Бронированиеs { get; set; } = new List<Бронирование>();
}
