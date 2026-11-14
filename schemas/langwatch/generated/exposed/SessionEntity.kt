package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SessionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SessionEntity>(SessionTable)

    var sessionToken by SessionTable.sessionToken
    var userId by SessionTable.userId
    var expires by SessionTable.expires
    var ipAddress by SessionTable.ipAddress
    var userAgent by SessionTable.userAgent
    var identifierId by SessionTable.identifierId
    var amr by SessionTable.amr
    var startedBySignUp by SessionTable.startedBySignUp
    var impersonating by SessionTable.impersonating
    var actorUserId by SessionTable.actorUserId
    var subjectUserId by SessionTable.subjectUserId
    var impersonationReason by SessionTable.impersonationReason
    var impersonationExpiresAt by SessionTable.impersonationExpiresAt
    var lastSeenAt by SessionTable.lastSeenAt
    var createdAt by SessionTable.createdAt
    var updatedAt by SessionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SessionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
