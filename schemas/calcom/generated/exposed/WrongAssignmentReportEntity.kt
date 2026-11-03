package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class WrongAssignmentReportEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, WrongAssignmentReportEntity>(WrongAssignmentReportTable)

    var bookingUid by WrongAssignmentReportTable.bookingUid
    var booking by BookingEntity referencedOn WrongAssignmentReportTable.bookingUid
    var reportedById by WrongAssignmentReportTable.reportedById
    var reportedBy by UserEntity optionalReferencedOn WrongAssignmentReportTable.reportedById
    var correctAssignee by WrongAssignmentReportTable.correctAssignee
    var additionalNotes by WrongAssignmentReportTable.additionalNotes
    var teamId by WrongAssignmentReportTable.teamId
    var team by TeamEntity optionalReferencedOn WrongAssignmentReportTable.teamId
    var status by WrongAssignmentReportTable.status
    var reviewedById by WrongAssignmentReportTable.reviewedById
    var reviewedBy by UserEntity optionalReferencedOn WrongAssignmentReportTable.reviewedById
    var reviewedAt by WrongAssignmentReportTable.reviewedAt
    var createdAt by WrongAssignmentReportTable.createdAt
    var updatedAt by WrongAssignmentReportTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WrongAssignmentReportTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
