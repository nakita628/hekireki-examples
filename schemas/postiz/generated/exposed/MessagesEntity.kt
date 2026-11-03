package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MessagesEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MessagesEntity>(MessagesTable)

    var from by MessagesTable.from
    var content by MessagesTable.content
    var groupId by MessagesTable.groupId
    var special by MessagesTable.special
    var createdAt by MessagesTable.createdAt
    var updatedAt by MessagesTable.updatedAt
    var deletedAt by MessagesTable.deletedAt
    var group by MessagesGroupEntity referencedOn MessagesTable.groupId
    val posts by PostEntity optionalReferrersOn PostTable.lastMessageId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MessagesTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
