package formbricks.models

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

    var name by WebhookTable.name
    var createdAt by WebhookTable.createdAt
    var updatedAt by WebhookTable.updatedAt
    var url by WebhookTable.url
    var source by WebhookTable.source1
    var workspace by WorkspaceEntity referencedOn WebhookTable.workspaceId
    var workspaceId by WebhookTable.workspaceId
    var triggers by WebhookTable.triggers
    var surveyIds by WebhookTable.surveyIds
    var secret by WebhookTable.secret

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WebhookTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
