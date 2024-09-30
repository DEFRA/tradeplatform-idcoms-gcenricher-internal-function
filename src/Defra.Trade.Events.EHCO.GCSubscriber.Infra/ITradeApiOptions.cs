// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.EHCO.GCSubscriber.Infra;


public interface ITradeApiOptions : IConfigurationOptions
{
    /// <summary>
    /// Relative base url.
    /// </summary>
    public string BaseAddress { get; set; }
}