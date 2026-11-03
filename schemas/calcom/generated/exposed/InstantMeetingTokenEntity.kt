package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class InstantMeetingTokenEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, InstantMeetingTokenEntity>(InstantMeetingTokenTable)

    var token by InstantMeetingTokenTable.token
    var expires by InstantMeetingTokenTable.expires
    var teamId by InstantMeetingTokenTable.teamId
    var team by TeamEntity referencedOn InstantMeetingTokenTable.teamId
    var bookingId by InstantMeetingTokenTable.bookingId
    var booking by BookingEntity optionalReferencedOn InstantMeetingTokenTable.bookingId
    var createdAt by InstantMeetingTokenTable.createdAt
    var updatedAt by InstantMeetingTokenTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(InstantMeetingTokenTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
