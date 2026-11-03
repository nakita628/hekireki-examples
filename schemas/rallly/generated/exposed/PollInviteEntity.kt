package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PollInviteEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PollInviteEntity>(PollInviteTable)

    var pollId by PollInviteTable.pollId
    var email by PollInviteTable.email
    var token by PollInviteTable.token
    var openedAt by PollInviteTable.openedAt
    var remindedAt by PollInviteTable.remindedAt
    var revokedAt by PollInviteTable.revokedAt
    var participantId by PollInviteTable.participantId
    var createdAt by PollInviteTable.createdAt
    var updatedAt by PollInviteTable.updatedAt
    var poll by PollEntity referencedOn PollInviteTable.pollId
    var participant by ParticipantEntity optionalReferencedOn PollInviteTable.participantId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PollInviteTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
