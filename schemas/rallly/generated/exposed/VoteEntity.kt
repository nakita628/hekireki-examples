package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class VoteEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, VoteEntity>(VoteTable)

    var participantId by VoteTable.participantId
    var optionId by VoteTable.optionId
    var pollId by VoteTable.pollId
    var type by VoteTable.type
    var createdAt by VoteTable.createdAt
    var updatedAt by VoteTable.updatedAt
    var participant by ParticipantEntity referencedOn VoteTable.participantId
    var option by OptionEntity referencedOn VoteTable.optionId
    var poll by PollEntity referencedOn VoteTable.pollId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(VoteTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
