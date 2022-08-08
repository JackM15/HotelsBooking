using HotelsBooking.Api.Services.Abstractions;
using System;

namespace HotelsBooking.Api.Services
{
    public class ScopedOperation : IScopedOperation
    {
        public Guid Guid { get ; set ; } = Guid.NewGuid();
    }
}
