// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.Models;

public static class GcMessageConstants
{
    public const string BrokerLabel = "trade.remos.enrichment";

    public const string MessageContentType = "application/json";

    public const string SchemaVersion = "1";

    public const string StatusErrorMessage = "{PropertyName} must be Complete";

    public const string SchemaVersionMessage = "{PropertyName} must be 1";

    public const string TypeErrorMessage = "{PropertyName} must be Internal";

    public const string LabelErrorMessage = "{PropertyName} must be trade.remos.enrichment";

    public const string ContentErrorMessage = "{PropertyName} must be application/json";

    public const string PublisherIdErrorMessage = "{PropertyName} must be TradeApi";
}