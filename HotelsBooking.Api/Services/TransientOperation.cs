using HotelsBooking.Api.Services.Abstractions;
using System;

namespace HotelsBooking.Api.Services
{
    public class TransientOperation : ITransientOperation
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
