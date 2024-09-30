// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.EHCO.GCSubscriber.Infra.UnitTests;

public class InternalApimSettingsTests
{
    [Fact]
    public void Options_ShouldBe_AsExpected()
    {
        // Act
        var sut = new InternalApimSettings
        {
            BaseUrl = "mocked-base-uri",
            SubscriptionKey = "mocked-key",
            SubscriptionKeyHeaderName = "mocked-header",
            DaeraInternalCertificateStoreApi = "mocked-api",
            Authority = "mocked-authority"
        };

        // Assert
        sut.BaseUrl.ShouldBe("mocked-base-uri");
        sut.SubscriptionKey.ShouldBe("mocked-key");
        sut.SubscriptionKeyHeaderName.ShouldBe("mocked-header");
        sut.DaeraInternalCertificateStoreApi.ShouldBe("mocked-api");
        sut.Authority.ShouldBe("mocked-authority");
    }
}