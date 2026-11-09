package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserPreferencesEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserPreferencesEntity>(UserPreferencesTable)

    var createdAt by UserPreferencesTable.createdAt
    var updatedAt by UserPreferencesTable.updatedAt
    var userId by UserPreferencesTable.userId
    var user by UserProfileEntity referencedOn UserPreferencesTable.userId
    var workspaceId by UserPreferencesTable.workspaceId
    var workspace by WorkspaceEntity optionalReferencedOn UserPreferencesTable.workspaceId
    var scope by UserPreferencesTable.scope
    var preferences by UserPreferencesTable.preferences

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserPreferencesTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
