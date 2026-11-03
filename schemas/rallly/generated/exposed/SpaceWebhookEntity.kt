package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SpaceWebhookEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SpaceWebhookEntity>(SpaceWebhookTable)

    var spaceId by SpaceWebhookTable.spaceId
    var url by SpaceWebhookTable.url
    var secret by SpaceWebhookTable.secret
    var events by SpaceWebhookTable.events
    var enabled by SpaceWebhookTable.enabled
    var version by SpaceWebhookTable.version
    var cursor by SpaceWebhookTable.cursor
    var lastDeliveredAt by SpaceWebhookTable.lastDeliveredAt
    var lastFailedAt by SpaceWebhookTable.lastFailedAt
    var consecutiveFailures by SpaceWebhookTable.consecutiveFailures
    var createdAt by SpaceWebhookTable.createdAt
    var updatedAt by SpaceWebhookTable.updatedAt
    var space by SpaceEntity referencedOn SpaceWebhookTable.spaceId
    val deliveries by WebhookDeliveryEntity referrersOn WebhookDeliveryTable.webhookId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SpaceWebhookTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
