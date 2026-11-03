package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class NotificationPreferenceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, NotificationPreferenceEntity>(NotificationPreferenceTable)

    var userId by NotificationPreferenceTable.userId
    var user by UserEntity referencedOn NotificationPreferenceTable.userId
    var projectId by NotificationPreferenceTable.projectId
    var project by ProjectEntity referencedOn NotificationPreferenceTable.projectId
    var channel by NotificationPreferenceTable.channel
    var type by NotificationPreferenceTable.type
    var enabled by NotificationPreferenceTable.enabled
    var createdAt by NotificationPreferenceTable.createdAt
    var updatedAt by NotificationPreferenceTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(NotificationPreferenceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
