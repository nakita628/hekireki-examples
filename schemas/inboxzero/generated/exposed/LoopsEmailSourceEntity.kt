package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LoopsEmailSourceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LoopsEmailSourceEntity>(LoopsEmailSourceTable)

    var createdAt by LoopsEmailSourceTable.createdAt
    var updatedAt by LoopsEmailSourceTable.updatedAt
    var emailMessageId by LoopsEmailSourceTable.emailMessageId
    var loopId by LoopsEmailSourceTable.loopId
    var loopName by LoopsEmailSourceTable.loopName
    var campaignId by LoopsEmailSourceTable.campaignId
    var campaignName by LoopsEmailSourceTable.campaignName
    var sourceType by LoopsEmailSourceTable.sourceType
    var eventTime by LoopsEmailSourceTable.eventTime

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LoopsEmailSourceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
