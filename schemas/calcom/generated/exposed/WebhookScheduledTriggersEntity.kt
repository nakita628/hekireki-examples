package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class WebhookScheduledTriggersEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, WebhookScheduledTriggersEntity>(WebhookScheduledTriggersTable)

    var jobName by WebhookScheduledTriggersTable.jobName
    var subscriberUrl by WebhookScheduledTriggersTable.subscriberUrl
    var payload by WebhookScheduledTriggersTable.payload
    var startAfter by WebhookScheduledTriggersTable.startAfter
    var retryCount by WebhookScheduledTriggersTable.retryCount
    var createdAt by WebhookScheduledTriggersTable.createdAt
    var appId by WebhookScheduledTriggersTable.appId
    var webhookId by WebhookScheduledTriggersTable.webhookId
    var webhook by WebhookEntity optionalReferencedOn WebhookScheduledTriggersTable.webhookId
    var bookingId by WebhookScheduledTriggersTable.bookingId
    var booking by BookingEntity optionalReferencedOn WebhookScheduledTriggersTable.bookingId
}
