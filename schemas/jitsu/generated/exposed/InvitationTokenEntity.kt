package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class InvitationTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InvitationTokenEntity>(InvitationTokenTable)

    var workspaceId by InvitationTokenTable.workspaceId
    var email by InvitationTokenTable.email
    var createdAt by InvitationTokenTable.createdAt
    var updatedAt by InvitationTokenTable.updatedAt
    var token by InvitationTokenTable.token
    var usedBy by InvitationTokenTable.usedBy
    var role by InvitationTokenTable.role

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(InvitationTokenTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
