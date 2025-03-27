using System;
using System.Collections.Generic;

namespace ServerInProject.Models;

public partial class Бронирование
{
    public int IdБронирования { get; set; }

    public int IdКлиента { get; set; }

    public int IdМаршрута { get; set; }

    public DateTime ДатаБронирования { get; set; }

    public virtual Клиент IdКлиентаNavigation { get; set; } = null!;

    public virtual СвободныеМаршруты IdМаршрутаNavigation { get; set; } = null!;
}
