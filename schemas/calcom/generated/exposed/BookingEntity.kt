package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BookingEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, BookingEntity>(BookingTable)

    var uid by BookingTable.uid
    var idempotencyKey by BookingTable.idempotencyKey
    var user by UserEntity optionalReferencedOn BookingTable.userId
    var userId by BookingTable.userId
    var userPrimaryEmail by BookingTable.userPrimaryEmail
    val references by BookingReferenceEntity optionalReferrersOn BookingReferenceTable.bookingId
    var eventType by EventTypeEntity optionalReferencedOn BookingTable.eventTypeId
    var eventTypeId by BookingTable.eventTypeId
    var title by BookingTable.title
    var description by BookingTable.description
    var customInputs by BookingTable.customInputs
    var responses by BookingTable.responses
    var startTime by BookingTable.startTime
    var endTime by BookingTable.endTime
    val attendees by AttendeeEntity optionalReferrersOn AttendeeTable.bookingId
    var location by BookingTable.location
    var createdAt by BookingTable.createdAt
    var updatedAt by BookingTable.updatedAt
    var status by BookingTable.status
    var paid by BookingTable.paid
    val payment by PaymentEntity referrersOn PaymentTable.bookingId
    var destinationCalendar by DestinationCalendarEntity optionalReferencedOn BookingTable.destinationCalendarId
    var destinationCalendarId by BookingTable.destinationCalendarId
    var cancellationReason by BookingTable.cancellationReason
    var rejectionReason by BookingTable.rejectionReason
    var reassignReason by BookingTable.reassignReason
    var reassignBy by UserEntity optionalReferencedOn BookingTable.reassignById
    var reassignById by BookingTable.reassignById
    var dynamicEventSlugRef by BookingTable.dynamicEventSlugRef
    var dynamicGroupSlugRef by BookingTable.dynamicGroupSlugRef
    var rescheduled by BookingTable.rescheduled
    var fromReschedule by BookingTable.fromReschedule
    var recurringEventId by BookingTable.recurringEventId
    var smsReminderNumber by BookingTable.smsReminderNumber
    var scheduledJobs by BookingTable.scheduledJobs
    val seatsReferences by BookingSeatEntity referrersOn BookingSeatTable.bookingId
    var metadata by BookingTable.metadata
    var isRecorded by BookingTable.isRecorded
    var iCalUID by BookingTable.iCalUID
    var iCalSequence by BookingTable.iCalSequence
    val instantMeetingToken by InstantMeetingTokenEntity optionalBackReferencedOn InstantMeetingTokenTable.bookingId
    var rating by BookingTable.rating
    var ratingFeedback by BookingTable.ratingFeedback
    var noShowHost by BookingTable.noShowHost
    val scheduledTriggers by WebhookScheduledTriggersEntity optionalReferrersOn WebhookScheduledTriggersTable.bookingId
    var oneTimePassword by BookingTable.oneTimePassword
    var cancelledBy by BookingTable.cancelledBy
    var rescheduledBy by BookingTable.rescheduledBy
    val assignmentReason by AssignmentReasonEntity referrersOn AssignmentReasonTable.bookingId
    val internalNote by BookingInternalNoteEntity referrersOn BookingInternalNoteTable.bookingId
    var creationSource by BookingTable.creationSource
    val tracking by TrackingEntity optionalBackReferencedOn TrackingTable.bookingId
    val expenseLogs by CreditExpenseLogEntity optionalReferrersOn CreditExpenseLogTable.bookingUid
    val report by BookingReportEntity optionalBackReferencedOn BookingReportTable.bookingUid
    val wrongAssignmentReports by WrongAssignmentReportEntity referrersOn WrongAssignmentReportTable.bookingUid

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BookingTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
