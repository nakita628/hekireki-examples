package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MessageEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, MessageEntity>(MessageTable)

    var content by MessageTable.content
    var role by MessageTable.role
    var conversationId by MessageTable.conversationId
    var createdAt by MessageTable.createdAt
    var updatedAt by MessageTable.updatedAt
    var metadata by MessageTable.metadata
    var conversation by ConversationEntity referencedOn MessageTable.conversationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MessageTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
