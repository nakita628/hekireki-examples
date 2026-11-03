package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object EventTypeTable : IdTable<Int>("\"EventType\"") {
    override val id = integer("id").autoIncrement().entityId()
    val title = text("title")
    val slug = text("slug")
    val description = text("description").nullable()
    val interfaceLanguage = text("interfaceLanguage").nullable()
    val position = integer("position").default(0)
    val locations = jsonb<String>("locations", { it }, { it }).nullable()
    val length = integer("length")
    val offsetStart = integer("offsetStart").default(0)
    val hidden = bool("hidden").default(false)
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EventType_userId_fkey\"",
        )
    val profileId =
        optReference(
            "profileId",
            ProfileTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EventType_profileId_fkey\"",
        )
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EventType_teamId_fkey\"",
        )
    val useEventLevelSelectedCalendars = bool("useEventLevelSelectedCalendars").default(false)
    val eventName = text("eventName").nullable()
    val parentId =
        optReference(
            "parentId",
            EventTypeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EventType_parentId_fkey\"",
        )
    val bookingFields = jsonb<String>("bookingFields", { it }, { it }).nullable()
    val timeZone = text("timeZone").nullable()
    val periodType = pgEnum("periodType", "\"PeriodType\"", PeriodType.entries, PeriodType::dbName).default(PeriodType.UNLIMITED)
    val periodStartDate = pgTimestamp("periodStartDate", 3).nullable()
    val periodEndDate = pgTimestamp("periodEndDate", 3).nullable()
    val periodDays = integer("periodDays").nullable()
    val periodCountCalendarDays = bool("periodCountCalendarDays").nullable()
    val lockTimeZoneToggleOnBookingPage = bool("lockTimeZoneToggleOnBookingPage").default(false)
    val lockedTimeZone = text("lockedTimeZone").nullable()
    val requiresConfirmation = bool("requiresConfirmation").default(false)
    val requiresConfirmationWillBlockSlot = bool("requiresConfirmationWillBlockSlot").default(false)
    val requiresConfirmationForFreeEmail = bool("requiresConfirmationForFreeEmail").default(false)
    val requiresBookerEmailVerification = bool("requiresBookerEmailVerification").default(false)
    val canSendCalVideoTranscriptionEmails = bool("canSendCalVideoTranscriptionEmails").default(true)
    val autoTranslateDescriptionEnabled = bool("autoTranslateDescriptionEnabled").default(false)
    val autoTranslateInstantMeetingTitleEnabled = bool("autoTranslateInstantMeetingTitleEnabled").default(true)
    val recurringEvent = jsonb<String>("recurringEvent", { it }, { it }).nullable()
    val disableGuests = bool("disableGuests").default(false)
    val hideCalendarNotes = bool("hideCalendarNotes").default(false)
    val hideCalendarEventDetails = bool("hideCalendarEventDetails").default(false)
    val minimumBookingNotice = integer("minimumBookingNotice").default(120)
    val beforeEventBuffer = integer("beforeEventBuffer").default(0)
    val afterEventBuffer = integer("afterEventBuffer").default(0)
    val seatsPerTimeSlot = integer("seatsPerTimeSlot").nullable()
    val onlyShowFirstAvailableSlot = bool("onlyShowFirstAvailableSlot").default(false)
    val showOptimizedSlots = bool("showOptimizedSlots").nullable().default(false)
    val disableCancelling = bool("disableCancelling").nullable().default(false)
    val disableRescheduling = bool("disableRescheduling").nullable().default(false)
    val minimumRescheduleNotice = integer("minimumRescheduleNotice").nullable()
    val seatsShowAttendees = bool("seatsShowAttendees").nullable().default(false)
    val seatsShowAvailabilityCount = bool("seatsShowAvailabilityCount").nullable().default(true)
    val schedulingType = pgEnum("schedulingType", "\"SchedulingType\"", SchedulingType.entries, SchedulingType::dbName).nullable()
    val scheduleId =
        optReference(
            "scheduleId",
            ScheduleTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EventType_scheduleId_fkey\"",
        )
    val allowReschedulingCancelledBookings = bool("allowReschedulingCancelledBookings").nullable().default(false)
    val price = integer("price").default(0)
    val currency = text("currency").default("usd")
    val slotInterval = integer("slotInterval").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val successRedirectUrl = text("successRedirectUrl").nullable()
    val forwardParamsSuccessRedirect = bool("forwardParamsSuccessRedirect").nullable().default(true)
    val bookingLimits = jsonb<String>("bookingLimits", { it }, { it }).nullable()
    val durationLimits = jsonb<String>("durationLimits", { it }, { it }).nullable()
    val isInstantEvent = bool("isInstantEvent").default(false)
    val instantMeetingExpiryTimeOffsetInSeconds = integer("instantMeetingExpiryTimeOffsetInSeconds").default(90)
    val instantMeetingScheduleId =
        optReference(
            "instantMeetingScheduleId",
            ScheduleTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EventType_instantMeetingScheduleId_fkey\"",
        )
    val instantMeetingParameters = array("instantMeetingParameters", TextColumnType()).nullable()
    val assignAllTeamMembers = bool("assignAllTeamMembers").default(false)
    val assignRRMembersUsingSegment = bool("assignRRMembersUsingSegment").default(false)
    val rrSegmentQueryValue = jsonb<String>("rrSegmentQueryValue", { it }, { it }).nullable()
    val useEventTypeDestinationCalendarEmail = bool("useEventTypeDestinationCalendarEmail").default(false)
    val isRRWeightsEnabled = bool("isRRWeightsEnabled").default(false)
    val maxLeadThreshold = integer("maxLeadThreshold").nullable()
    val includeNoShowInRRCalculation = bool("includeNoShowInRRCalculation").default(false)
    val allowReschedulingPastBookings = bool("allowReschedulingPastBookings").default(false)
    val hideOrganizerEmail = bool("hideOrganizerEmail").default(false)
    val maxActiveBookingsPerBooker = integer("maxActiveBookingsPerBooker").nullable()
    val maxActiveBookingPerBookerOfferReschedule = bool("maxActiveBookingPerBookerOfferReschedule").default(false)
    val customReplyToEmail = text("customReplyToEmail").nullable()
    val eventTypeColor = jsonb<String>("eventTypeColor", { it }, { it }).nullable()
    val rescheduleWithSameRoundRobinHost = bool("rescheduleWithSameRoundRobinHost").default(false)
    val secondaryEmailId =
        optReference(
            "secondaryEmailId",
            SecondaryEmailTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EventType_secondaryEmailId_fkey\"",
        )
    val useBookerTimezone = bool("useBookerTimezone").default(false)
    val restrictionScheduleId =
        optReference(
            "restrictionScheduleId",
            ScheduleTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EventType_restrictionScheduleId_fkey\"",
        )
    val bookingRequiresAuthentication = bool("bookingRequiresAuthentication").default(false)
    val rrHostSubsetEnabled = bool("rrHostSubsetEnabled").default(false)
    val requiresCancellationReason =
        pgEnum(
            "requiresCancellationReason",
            "\"CancellationReasonRequirement\"",
            CancellationReasonRequirement.entries,
            CancellationReasonRequirement::dbName,
        ).nullable().default(CancellationReasonRequirement.MANDATORY_HOST_ONLY)
    val enablePerHostLocations = bool("enablePerHostLocations").default(false)
    val createdAt =
        pgTimestamp("createdAt", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"EventType_pkey\"")

    init {
        index("\"EventType_userId_idx\"", false, userId)
        index("\"EventType_teamId_idx\"", false, teamId)
        index("\"EventType_profileId_idx\"", false, profileId)
        index("\"EventType_scheduleId_idx\"", false, scheduleId)
        index("\"EventType_secondaryEmailId_idx\"", false, secondaryEmailId)
        index("\"EventType_parentId_idx\"", false, parentId)
        index("\"EventType_parentId_teamId_idx\"", false, parentId, teamId)
        index("\"EventType_restrictionScheduleId_idx\"", false, restrictionScheduleId)
        uniqueIndex("\"EventType_userId_slug_key\"", userId, slug)
        uniqueIndex("\"EventType_teamId_slug_key\"", teamId, slug)
        uniqueIndex("\"EventType_userId_parentId_key\"", userId, parentId)
    }
}
