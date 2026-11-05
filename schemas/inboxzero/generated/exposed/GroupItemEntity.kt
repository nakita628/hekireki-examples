package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GroupItemEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GroupItemEntity>(GroupItemTable)

    var createdAt by GroupItemTable.createdAt
    var updatedAt by GroupItemTable.updatedAt
    var groupId by GroupItemTable.groupId
    var group by GroupEntity optionalReferencedOn GroupItemTable.groupId
    var type by GroupItemTable.type
    var value by GroupItemTable.value
    var exclude by GroupItemTable.exclude
    var reason by GroupItemTable.reason
    var threadId by GroupItemTable.threadId
    var messageId by GroupItemTable.messageId
    var source by GroupItemTable.source1

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GroupItemTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
