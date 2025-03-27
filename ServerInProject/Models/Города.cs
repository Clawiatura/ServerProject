using System;
using System.Collections.Generic;

namespace ServerInProject.Models;

public partial class Города
{
    public int IdГорода { get; set; }

    public string НазваниеГорода { get; set; } = null!;

    public virtual ICollection<СвободныеМаршруты> СвободныеМаршрутыIdГородаОтправленияNavigations { get; set; } = new List<СвободныеМаршруты>();

    public virtual ICollection<СвободныеМаршруты> СвободныеМаршрутыIdГородаПрибытияNavigations { get; set; } = new List<СвободныеМаршруты>();
}
