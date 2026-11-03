package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EventTypeEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, EventTypeEntity>(EventTypeTable)

    var title by EventTypeTable.title
    var slug by EventTypeTable.slug
    var description by EventTypeTable.description
    var interfaceLanguage by EventTypeTable.interfaceLanguage
    var position by EventTypeTable.position
    var locations by EventTypeTable.locations
    var length by EventTypeTable.length
    var offsetStart by EventTypeTable.offsetStart
    var hidden by EventTypeTable.hidden
    val hosts by HostEntity referrersOn HostTable.eventTypeId
    var users by UserEntity via UserEventtypeTable
    var owner by UserEntity optionalReferencedOn EventTypeTable.userId
    var userId by EventTypeTable.userId
    var profileId by EventTypeTable.profileId
    var profile by ProfileEntity optionalReferencedOn EventTypeTable.profileId
    var team by TeamEntity optionalReferencedOn EventTypeTable.teamId
    var teamId by EventTypeTable.teamId
    val hashedLink by HashedLinkEntity referrersOn HashedLinkTable.eventTypeId
    val bookings by BookingEntity optionalReferrersOn BookingTable.eventTypeId
    val availability by AvailabilityEntity optionalReferrersOn AvailabilityTable.eventTypeId
    val webhooks by WebhookEntity optionalReferrersOn WebhookTable.eventTypeId
    val destinationCalendar by DestinationCalendarEntity optionalBackReferencedOn DestinationCalendarTable.eventTypeId
    var useEventLevelSelectedCalendars by EventTypeTable.useEventLevelSelectedCalendars
    var eventName by EventTypeTable.eventName
    val customInputs by EventTypeCustomInputEntity referrersOn EventTypeCustomInputTable.eventTypeId
    var parentId by EventTypeTable.parentId
    var parent by EventTypeEntity optionalReferencedOn EventTypeTable.parentId
    val children by EventTypeEntity optionalReferrersOn EventTypeTable.parentId
    var bookingFields by EventTypeTable.bookingFields
    var timeZone by EventTypeTable.timeZone
    var periodType by EventTypeTable.periodType
    var periodStartDate by EventTypeTable.periodStartDate
    var periodEndDate by EventTypeTable.periodEndDate
    var periodDays by EventTypeTable.periodDays
    var periodCountCalendarDays by EventTypeTable.periodCountCalendarDays
    var lockTimeZoneToggleOnBookingPage by EventTypeTable.lockTimeZoneToggleOnBookingPage
    var lockedTimeZone by EventTypeTable.lockedTimeZone
    var requiresConfirmation by EventTypeTable.requiresConfirmation
    var requiresConfirmationWillBlockSlot by EventTypeTable.requiresConfirmationWillBlockSlot
    var requiresConfirmationForFreeEmail by EventTypeTable.requiresConfirmationForFreeEmail
    var requiresBookerEmailVerification by EventTypeTable.requiresBookerEmailVerification
    var canSendCalVideoTranscriptionEmails by EventTypeTable.canSendCalVideoTranscriptionEmails
    var autoTranslateDescriptionEnabled by EventTypeTable.autoTranslateDescriptionEnabled
    var autoTranslateInstantMeetingTitleEnabled by EventTypeTable.autoTranslateInstantMeetingTitleEnabled
    var recurringEvent by EventTypeTable.recurringEvent
    var disableGuests by EventTypeTable.disableGuests
    var hideCalendarNotes by EventTypeTable.hideCalendarNotes
    var hideCalendarEventDetails by EventTypeTable.hideCalendarEventDetails
    var minimumBookingNotice by EventTypeTable.minimumBookingNotice
    var beforeEventBuffer by EventTypeTable.beforeEventBuffer
    var afterEventBuffer by EventTypeTable.afterEventBuffer
    var seatsPerTimeSlot by EventTypeTable.seatsPerTimeSlot
    var onlyShowFirstAvailableSlot by EventTypeTable.onlyShowFirstAvailableSlot
    var showOptimizedSlots by EventTypeTable.showOptimizedSlots
    var disableCancelling by EventTypeTable.disableCancelling
    var disableRescheduling by EventTypeTable.disableRescheduling
    var minimumRescheduleNotice by EventTypeTable.minimumRescheduleNotice
    var seatsShowAttendees by EventTypeTable.seatsShowAttendees
    var seatsShowAvailabilityCount by EventTypeTable.seatsShowAvailabilityCount
    var schedulingType by EventTypeTable.schedulingType
    var schedule by ScheduleEntity optionalReferencedOn EventTypeTable.scheduleId
    var scheduleId by EventTypeTable.scheduleId
    var allowReschedulingCancelledBookings by EventTypeTable.allowReschedulingCancelledBookings
    var price by EventTypeTable.price
    var currency by EventTypeTable.currency
    var slotInterval by EventTypeTable.slotInterval
    var metadata by EventTypeTable.metadata
    var successRedirectUrl by EventTypeTable.successRedirectUrl
    var forwardParamsSuccessRedirect by EventTypeTable.forwardParamsSuccessRedirect
    var bookingLimits by EventTypeTable.bookingLimits
    var durationLimits by EventTypeTable.durationLimits
    var isInstantEvent by EventTypeTable.isInstantEvent
    var instantMeetingExpiryTimeOffsetInSeconds by EventTypeTable.instantMeetingExpiryTimeOffsetInSeconds
    var instantMeetingScheduleId by EventTypeTable.instantMeetingScheduleId
    var instantMeetingSchedule by ScheduleEntity optionalReferencedOn EventTypeTable.instantMeetingScheduleId
    var instantMeetingParameters by EventTypeTable.instantMeetingParameters
    var assignAllTeamMembers by EventTypeTable.assignAllTeamMembers
    var assignRRMembersUsingSegment by EventTypeTable.assignRRMembersUsingSegment
    var rrSegmentQueryValue by EventTypeTable.rrSegmentQueryValue
    var useEventTypeDestinationCalendarEmail by EventTypeTable.useEventTypeDestinationCalendarEmail
    var isRRWeightsEnabled by EventTypeTable.isRRWeightsEnabled
    val fieldTranslations by EventTypeTranslationEntity referrersOn EventTypeTranslationTable.eventTypeId
    var maxLeadThreshold by EventTypeTable.maxLeadThreshold
    var includeNoShowInRRCalculation by EventTypeTable.includeNoShowInRRCalculation
    val selectedCalendars by SelectedCalendarEntity optionalReferrersOn SelectedCalendarTable.eventTypeId
    var allowReschedulingPastBookings by EventTypeTable.allowReschedulingPastBookings
    var hideOrganizerEmail by EventTypeTable.hideOrganizerEmail
    var maxActiveBookingsPerBooker by EventTypeTable.maxActiveBookingsPerBooker
    var maxActiveBookingPerBookerOfferReschedule by EventTypeTable.maxActiveBookingPerBookerOfferReschedule
    var customReplyToEmail by EventTypeTable.customReplyToEmail
    val calVideoSettings by CalVideoSettingsEntity optionalBackReferencedOn CalVideoSettingsTable.id
    var eventTypeColor by EventTypeTable.eventTypeColor
    var rescheduleWithSameRoundRobinHost by EventTypeTable.rescheduleWithSameRoundRobinHost
    var secondaryEmailId by EventTypeTable.secondaryEmailId
    var secondaryEmail by SecondaryEmailEntity optionalReferencedOn EventTypeTable.secondaryEmailId
    var useBookerTimezone by EventTypeTable.useBookerTimezone
    var restrictionScheduleId by EventTypeTable.restrictionScheduleId
    var restrictionSchedule by ScheduleEntity optionalReferencedOn EventTypeTable.restrictionScheduleId
    val hostGroups by HostGroupEntity optionalReferrersOn HostGroupTable.eventTypeId
    var bookingRequiresAuthentication by EventTypeTable.bookingRequiresAuthentication
    var rrHostSubsetEnabled by EventTypeTable.rrHostSubsetEnabled
    var requiresCancellationReason by EventTypeTable.requiresCancellationReason
    var enablePerHostLocations by EventTypeTable.enablePerHostLocations
    var createdAt by EventTypeTable.createdAt
    var updatedAt by EventTypeTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EventTypeTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
