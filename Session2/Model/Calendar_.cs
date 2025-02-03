using System;
using System.Collections.Generic;

namespace Session2.Model;

public partial class Calendar_ : IComparable
{
    public int IdCalendar { get; set; }

    public string TypeOfEvent { get; set; } = null!;

    public int? IdEmployee { get; set; }

    public DateOnly DateStart { get; set; }

    public DateOnly DateFinish { get; set; }

    public int? IdEvent { get; set; }

    public string? TypeOfAbsense { get; set; }

    public int? IdAlternate { get; set; }

    public virtual Employee? IdAlternateNavigation { get; set; }

    public virtual Employee? IdEmployeeNavigation { get; set; }

    public virtual Event? IdEventNavigation { get; set; }

   
    public int CompareTo(object? obj)
    {
        if (obj is Calendar_)
        {
            Calendar_ obj1 = obj as Calendar_;
            return this.DateStart.CompareTo(obj1.DateStart);
        }
        return 0;
    }
}
