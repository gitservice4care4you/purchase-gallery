var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.PurchaseGallery_Api>("purchasegallery-api");

builder.AddProject<Projects.PurchaseGallery_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService);

//builder.AddProject<Projects.PurchaseGallery_Api>("purchasegallery-api");

builder.Build().Run();
