package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object BookingLinkTable : IdTable<String>("\"BookingLink\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val slug = text("slug").uniqueIndex("\"BookingLink_slug_key\"")
    val title = text("title")
    val description = text("description").nullable()
    val isActive = bool("isActive").default(true)
    val durationMinutes = integer("durationMinutes")
    val slotIntervalMinutes = integer("slotIntervalMinutes")
    val locationType =
        pgEnum("locationType", "\"BookingLinkLocationType\"", BookingLinkLocationType.entries, BookingLinkLocationType::dbName).default(
            BookingLinkLocationType.CUSTOM,
        )
    val locationValue = text("locationValue").nullable()
    val minimumNoticeMinutes = integer("minimumNoticeMinutes").default(120)
    val maxDaysAhead = integer("maxDaysAhead").default(90)
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BookingLink_emailAccountId_fkey\"",
        ).uniqueIndex("\"BookingLink_emailAccountId_key\"")
    val destinationCalendarId =
        optReference(
            "destinationCalendarId",
            CalendarTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BookingLink_destinationCalendarId_fkey\"",
        )
    val availabilityScheduleId = text("availabilityScheduleId")
    override val primaryKey = PrimaryKey(id, name = "\"BookingLink_pkey\"")

    init {
        index("\"BookingLink_destinationCalendarId_idx\"", false, destinationCalendarId)
        index("\"BookingLink_availabilityScheduleId_idx\"", false, availabilityScheduleId)
        foreignKey(
            availabilityScheduleId to AvailabilityScheduleTable.id,
            emailAccountId to AvailabilityScheduleTable.emailAccountId,
            onDelete = ReferenceOption.NO_ACTION,
            onUpdate = ReferenceOption.CASCADE,
            name = "\"BookingLink_availabilityScheduleId_emailAccountId_fkey\"",
        )
    }
}
