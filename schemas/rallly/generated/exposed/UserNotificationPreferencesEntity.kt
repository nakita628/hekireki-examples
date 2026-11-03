package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserNotificationPreferencesEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserNotificationPreferencesEntity>(UserNotificationPreferencesTable)

    var userId by UserNotificationPreferencesTable.userId
    var prefs by UserNotificationPreferencesTable.prefs
    var createdAt by UserNotificationPreferencesTable.createdAt
    var updatedAt by UserNotificationPreferencesTable.updatedAt
    var user by UserEntity referencedOn UserNotificationPreferencesTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserNotificationPreferencesTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
