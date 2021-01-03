using System;

namespace IData.Interfaces
{
    public interface ITimestamp
    {
        DateTime? ChangedAt { get; set; }
        DateTime? CreatedAt { get; set; }
    }
}