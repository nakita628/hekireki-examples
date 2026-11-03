package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SuppressedEmailEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SuppressedEmailEntity>(SuppressedEmailTable)

    var createdAt by SuppressedEmailTable.createdAt
    var updatedAt by SuppressedEmailTable.updatedAt
    var email by SuppressedEmailTable.email
    var transientGeneralBounceCount by SuppressedEmailTable.transientGeneralBounceCount
    var suppressedAt by SuppressedEmailTable.suppressedAt
    var lastWebhookId by SuppressedEmailTable.lastWebhookId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SuppressedEmailTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
