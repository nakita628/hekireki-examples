package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WebhookEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WebhookEntity>(WebhookTable)

    var webhookUrl by WebhookTable.webhookUrl
    var eventTriggers by WebhookTable.eventTriggers
    var secret by WebhookTable.secret
    var enabled by WebhookTable.enabled
    var createdAt by WebhookTable.createdAt
    var updatedAt by WebhookTable.updatedAt
    var userId by WebhookTable.userId
    var user by UserEntity referencedOn WebhookTable.userId
    var teamId by WebhookTable.teamId
    var team by TeamEntity referencedOn WebhookTable.teamId
    val webhookCalls by WebhookCallEntity referrersOn WebhookCallTable.webhookId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WebhookTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
