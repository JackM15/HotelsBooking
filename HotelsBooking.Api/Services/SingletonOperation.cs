using HotelsBooking.Api.Services.Abstractions;
using System;

namespace HotelsBooking.Api.Services
{
    public class SingletonOperation : ISingletonOperation
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
