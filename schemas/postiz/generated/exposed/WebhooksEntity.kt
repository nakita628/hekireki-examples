package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WebhooksEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WebhooksEntity>(WebhooksTable)

    var name by WebhooksTable.name
    var organizationId by WebhooksTable.organizationId
    var url by WebhooksTable.url
    var deletedAt by WebhooksTable.deletedAt
    var createdAt by WebhooksTable.createdAt
    var updatedAt by WebhooksTable.updatedAt
    val integrations by IntegrationsWebhooksEntity referrersOn IntegrationsWebhooksTable.webhookId
    var organization by OrganizationEntity referencedOn WebhooksTable.organizationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WebhooksTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
