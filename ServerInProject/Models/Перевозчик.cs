using System;
using System.Collections.Generic;

namespace ServerInProject.Models;

public partial class Перевозчик
{
    public int IdПеревозчика { get; set; }

    public string НазваниеПеревозчика { get; set; } = null!;

    public virtual ICollection<СвободныеМаршруты> СвободныеМаршрутыs { get; set; } = new List<СвободныеМаршруты>();
}
