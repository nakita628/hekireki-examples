package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PollEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PollEntity>(PollTable)

    var createdAt by PollTable.createdAt
    var updatedAt by PollTable.updatedAt
    var deadline by PollTable.deadline
    var title by PollTable.title
    var description by PollTable.description
    var location by PollTable.location
    var conferencing by PollTable.conferencing
    var userId by PollTable.userId
    var timeZone by PollTable.timeZone
    var status by PollTable.status
    var closedReason by PollTable.closedReason
    var kind by PollTable.kind
    var deleted by PollTable.deleted
    var deletedAt by PollTable.deletedAt
    var eventId by PollTable.eventId
    var scheduledEventId by PollTable.scheduledEventId
    var hideParticipants by PollTable.hideParticipants
    var hideScores by PollTable.hideScores
    var disableComments by PollTable.disableComments
    var requireParticipantEmail by PollTable.requireParticipantEmail
    var allowTentativeVotes by PollTable.allowTentativeVotes
    var muted by PollTable.muted
    var user by UserEntity optionalReferencedOn PollTable.userId
    var scheduledEvent by ScheduledEventEntity optionalReferencedOn PollTable.scheduledEventId
    val options by OptionEntity referrersOn OptionTable.pollId
    val participants by ParticipantEntity referrersOn ParticipantTable.pollId
    val comments by CommentEntity referrersOn CommentTable.pollId
    val votes by VoteEntity referrersOn VoteTable.pollId
    val invites by PollInviteEntity referrersOn PollInviteTable.pollId
    val activities by PollActivityEntity referrersOn PollActivityTable.pollId
    var space by SpaceEntity optionalReferencedOn PollTable.spaceId
    var spaceId by PollTable.spaceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PollTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
