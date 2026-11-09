package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserPasswordEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserPasswordEntity>(UserPasswordTable)

    var createdAt by UserPasswordTable.createdAt
    var updatedAt by UserPasswordTable.updatedAt
    var userId by UserPasswordTable.userId
    var user by UserProfileEntity referencedOn UserPasswordTable.userId
    var hash by UserPasswordTable.hash
    var changeAtNextLogin by UserPasswordTable.changeAtNextLogin

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserPasswordTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
