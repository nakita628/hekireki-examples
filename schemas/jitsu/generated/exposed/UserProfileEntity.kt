package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserProfileEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserProfileEntity>(UserProfileTable)

    var name by UserProfileTable.name
    var email by UserProfileTable.email
    var admin by UserProfileTable.admin
    var createdAt by UserProfileTable.createdAt
    var updatedAt by UserProfileTable.updatedAt
    var loginProvider by UserProfileTable.loginProvider
    var externalUsername by UserProfileTable.externalUsername
    var externalId by UserProfileTable.externalId
    val tokens by UserApiTokenEntity referrersOn UserApiTokenTable.userId
    val workspaceAccess by WorkspaceAccessEntity referrersOn WorkspaceAccessTable.userId
    val userPreferences by UserPreferencesEntity referrersOn UserPreferencesTable.userId
    val workspaceUserProperties by WorkspaceUserPropertiesEntity referrersOn WorkspaceUserPropertiesTable.userId
    val password by UserPasswordEntity optionalBackReferencedOn UserPasswordTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserProfileTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
