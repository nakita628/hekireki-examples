package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class UserEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, UserEntity>(UserTable)

    var username by UserTable.username
    var password by UserTable.password
    var role by UserTable.role
    var logoUrl by UserTable.logoUrl
    var displayName by UserTable.displayName
    var twoFactorRequired by UserTable.twoFactorRequired
    var createdAt by UserTable.createdAt
    var updatedAt by UserTable.updatedAt
    var deletedAt by UserTable.deletedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
