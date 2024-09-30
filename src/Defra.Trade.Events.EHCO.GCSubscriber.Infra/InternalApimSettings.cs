// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.EHCO.GCSubscriber.Infra;


public class InternalApimSettings : ITradeApiOptions
{
    public static string SectionName { get; set; } = "Apim:Internal";

    public string Authority { get; set; }

    public string BaseUrl { get; set; }

    public string SubscriptionKey { get; set; }

    public string SubscriptionKeyHeaderName { get; set; }

    public string DaeraInternalCertificateStoreApiHealthEndpoint { get; set; } = "/api/health";
    public string DaeraInternalCertificateStoreApi { get; set; } = "/certificates-store/v1";

    public string CmsAdapterApi { get; set; } = "/trade-crm-adapter/v1";

    public string BaseAddress
    {
        get => BaseUrl;

        set => BaseUrl = value;
    }
}