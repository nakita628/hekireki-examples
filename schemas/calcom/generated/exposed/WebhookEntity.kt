package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class WebhookEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WebhookEntity>(WebhookTable)

    var userId by WebhookTable.userId
    var teamId by WebhookTable.teamId
    var eventTypeId by WebhookTable.eventTypeId
    var platformOAuthClientId by WebhookTable.platformOAuthClientId
    var subscriberUrl by WebhookTable.subscriberUrl
    var payloadTemplate by WebhookTable.payloadTemplate
    var createdAt by WebhookTable.createdAt
    var active by WebhookTable.active
    var eventTriggers by WebhookTable.eventTriggers
    var user by UserEntity optionalReferencedOn WebhookTable.userId
    var team by TeamEntity optionalReferencedOn WebhookTable.teamId
    var eventType by EventTypeEntity optionalReferencedOn WebhookTable.eventTypeId
    var platformOAuthClient by PlatformOAuthClientEntity optionalReferencedOn WebhookTable.platformOAuthClientId
    var app by AppEntity optionalReferencedOn WebhookTable.appId
    var appId by WebhookTable.appId
    var secret by WebhookTable.secret
    var platform by WebhookTable.platform
    val scheduledTriggers by WebhookScheduledTriggersEntity optionalReferrersOn WebhookScheduledTriggersTable.webhookId
    var time by WebhookTable.time
    var timeUnit by WebhookTable.timeUnit
    var version by WebhookTable.version
}
