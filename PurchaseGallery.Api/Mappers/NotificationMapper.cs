using System;
using PurchaseGallery.Api.Dtos.Notification;
using PurchaseGallery.Api.Models.Notifications;
using PurchaseGallery.Api.Models.PurchaseRequests;

namespace PurchaseGallery.Api.Mappers;

public static class NotificationMapper
{
    public static NotificationDto ToDto(Notifications notification)
    {
        return new NotificationDto
        {
            PurchaseRequestId = notification.PurchaseRequestId,
            Title = notification.Title,
            Message = notification.Message,
            CreatedDate = notification.CreatedDate,
            SentDate = notification.SentDate,
        };
    }
    public static Notifications ToModel(NotificationDto notificationDto, PurchaseRequest purchaseRequest)
    {
        return new Notifications
        {
            Id = Guid.NewGuid(),
            PurchaseRequestId = notificationDto.PurchaseRequestId,
            Title = notificationDto.Title,
            Message = notificationDto.Message,
            CreatedDate = notificationDto.CreatedDate,
            SentDate = notificationDto.SentDate,
            PurchaseRequest = purchaseRequest
        };
    }
}