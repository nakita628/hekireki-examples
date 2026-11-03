package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class IncomingWebhookEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IncomingWebhookEntity>(IncomingWebhookTable)

    var externalId by IncomingWebhookTable.externalId
    var name by IncomingWebhookTable.name
    var secret by IncomingWebhookTable.secret
    var source by IncomingWebhookTable.source1
    var actions by IncomingWebhookTable.actions
    var consecutiveFailures by IncomingWebhookTable.consecutiveFailures
    var lastFailedAt by IncomingWebhookTable.lastFailedAt
    var disabledAt by IncomingWebhookTable.disabledAt
    var teamId by IncomingWebhookTable.teamId
    var team by TeamEntity referencedOn IncomingWebhookTable.teamId
    var createdAt by IncomingWebhookTable.createdAt
    var updatedAt by IncomingWebhookTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(IncomingWebhookTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
