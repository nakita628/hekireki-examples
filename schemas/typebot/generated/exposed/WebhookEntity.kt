package typebot.models

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

    var createdAt by WebhookTable.createdAt
    var updatedAt by WebhookTable.updatedAt
    var url by WebhookTable.url
    var method by WebhookTable.method
    var queryParams by WebhookTable.queryParams
    var headers by WebhookTable.headers
    var body by WebhookTable.body
    var typebotId by WebhookTable.typebotId
    var typebot by TypebotEntity referencedOn WebhookTable.typebotId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WebhookTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
