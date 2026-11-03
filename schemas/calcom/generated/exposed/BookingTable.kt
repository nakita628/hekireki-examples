package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object BookingTable : IdTable<Int>("\"Booking\"") {
    override val id = integer("id").autoIncrement().entityId()
    val uid = text("uid").uniqueIndex("\"Booking_uid_key\"")
    val idempotencyKey = text("idempotencyKey").nullable().uniqueIndex("\"Booking_idempotencyKey_key\"")
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Booking_userId_fkey\"",
        )
    val userPrimaryEmail = text("userPrimaryEmail").nullable()
    val eventTypeId =
        optReference(
            "eventTypeId",
            EventTypeTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Booking_eventTypeId_fkey\"",
        )
    val title = text("title")
    val description = text("description").nullable()
    val customInputs = jsonb<String>("customInputs", { it }, { it }).nullable()
    val responses = jsonb<String>("responses", { it }, { it }).nullable()
    val startTime = pgTimestamp("startTime", 3)
    val endTime = pgTimestamp("endTime", 3)
    val location = text("location").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val status = pgEnum("status", "\"BookingStatus\"", BookingStatus.entries, BookingStatus::dbName).default(BookingStatus.ACCEPTED)
    val paid = bool("paid").default(false)
    val destinationCalendarId =
        optReference(
            "destinationCalendarId",
            DestinationCalendarTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Booking_destinationCalendarId_fkey\"",
        )
    val cancellationReason = text("cancellationReason").nullable()
    val rejectionReason = text("rejectionReason").nullable()
    val reassignReason = text("reassignReason").nullable()
    val reassignById =
        optReference(
            "reassignById",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Booking_reassignById_fkey\"",
        )
    val dynamicEventSlugRef = text("dynamicEventSlugRef").nullable()
    val dynamicGroupSlugRef = text("dynamicGroupSlugRef").nullable()
    val rescheduled = bool("rescheduled").nullable()
    val fromReschedule = text("fromReschedule").nullable()
    val recurringEventId = text("recurringEventId").nullable()
    val smsReminderNumber = text("smsReminderNumber").nullable()
    val scheduledJobs = array("scheduledJobs", TextColumnType()).nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val isRecorded = bool("isRecorded").default(false)
    val iCalUID = text("iCalUID").nullable().default("")
    val iCalSequence = integer("iCalSequence").default(0)
    val rating = integer("rating").nullable()
    val ratingFeedback = text("ratingFeedback").nullable()
    val noShowHost = bool("noShowHost").nullable().default(false)
    val oneTimePassword =
        text("oneTimePassword").nullable().clientDefault { UUID.randomUUID().toString() }.uniqueIndex("\"Booking_oneTimePassword_key\"")
    val cancelledBy = text("cancelledBy").nullable()
    val rescheduledBy = text("rescheduledBy").nullable()
    val creationSource = pgEnum("creationSource", "\"CreationSource\"", CreationSource.entries, CreationSource::dbName).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Booking_pkey\"")

    init {
        index("\"Booking_eventTypeId_idx\"", false, eventTypeId)
        index("\"Booking_userId_idx\"", false, userId)
        index("\"Booking_destinationCalendarId_idx\"", false, destinationCalendarId)
        index("\"Booking_recurringEventId_idx\"", false, recurringEventId)
        index("\"Booking_uid_idx\"", false, uid)
        index("\"Booking_status_idx\"", false, status)
        index("\"Booking_startTime_endTime_status_idx\"", false, startTime, endTime, status)
        index("\"Booking_fromReschedule_idx\"", false, fromReschedule)
        index("\"Booking_userId_endTime_idx\"", false, userId, endTime)
        index("\"Booking_userId_status_startTime_idx\"", false, userId, status, startTime)
        index("\"Booking_eventTypeId_status_idx\"", false, eventTypeId, status)
        index("\"Booking_userId_createdAt_idx\"", false, userId, createdAt)
    }
}
