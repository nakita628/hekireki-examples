package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object CalendarCacheEventTable : IdTable<String>("\"CalendarCacheEvent\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val selectedCalendarId =
        reference(
            "selectedCalendarId",
            SelectedCalendarTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CalendarCacheEvent_selectedCalendarId_fkey\"",
        )
    val externalId = text("externalId")
    val externalEtag = text("externalEtag")
    val iCalUID = text("iCalUID").nullable()
    val iCalSequence = integer("iCalSequence").default(0)
    val summary = text("summary").nullable()
    val description = text("description").nullable()
    val location = text("location").nullable()
    val start = pgTimestamp("start", 3)
    val end = pgTimestamp("end", 3)
    val isAllDay = bool("isAllDay").default(false)
    val timeZone = text("timeZone").nullable()
    val status =
        pgEnum("status", "\"CalendarCacheEventStatus\"", CalendarCacheEventStatus.entries, CalendarCacheEventStatus::dbName).default(
            CalendarCacheEventStatus.CONFIRMED,
        )
    val recurringEventId = text("recurringEventId").nullable()
    val originalStartTime = pgTimestamp("originalStartTime", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val externalCreatedAt = pgTimestamp("externalCreatedAt", 3).nullable()
    val externalUpdatedAt = pgTimestamp("externalUpdatedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"CalendarCacheEvent_pkey\"")

    init {
        index("\"CalendarCacheEvent_start_end_status_idx\"", false, start, end, status)
        index("\"CalendarCacheEvent_selectedCalendarId_iCalUID_idx\"", false, selectedCalendarId, iCalUID)
        uniqueIndex("\"CalendarCacheEvent_selectedCalendarId_externalId_key\"", selectedCalendarId, externalId)
    }
}
