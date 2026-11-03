package papermark.models

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
    var teamId by NotificationPreferenceTable.teamId
    var type by NotificationPreferenceTable.type
    var frequency by NotificationPreferenceTable.frequency
    var scope by NotificationPreferenceTable.scope
    var user by UserEntity referencedOn NotificationPreferenceTable.userId
    var team by TeamEntity referencedOn NotificationPreferenceTable.teamId
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
