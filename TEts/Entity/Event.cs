//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TEts.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public int DirectionId { get; set; }
        public System.DateTime EventDate { get; set; }
        public System.TimeSpan EventTime { get; set; }
        public int EventDuration { get; set; }
        public string EventPhoto { get; set; }
    
        public virtual Direction Direction { get; set; }
    }
}
