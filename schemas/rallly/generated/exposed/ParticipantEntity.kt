package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ParticipantEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ParticipantEntity>(ParticipantTable)

    var name by ParticipantTable.name
    var email by ParticipantTable.email
    var note by ParticipantTable.note
    var token by ParticipantTable.token
    var userId by ParticipantTable.userId
    var pollId by ParticipantTable.pollId
    var locale by ParticipantTable.locale
    var timeZone by ParticipantTable.timeZone
    var createdAt by ParticipantTable.createdAt
    var updatedAt by ParticipantTable.updatedAt
    val votes by VoteEntity referrersOn VoteTable.participantId
    val invite by PollInviteEntity optionalBackReferencedOn PollInviteTable.participantId
    var poll by PollEntity referencedOn ParticipantTable.pollId
    var user by UserEntity optionalReferencedOn ParticipantTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ParticipantTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
