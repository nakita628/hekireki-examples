package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ProviderCalendarTable : IdTable<String>("provider_calendars") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val calendarConnectionId =
        reference(
            "calendar_connection_id",
            CalendarConnectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "provider_calendars_calendar_connection_id_fkey",
        )
    val providerCalendarId = text("provider_calendar_id")
    val name = text("name")
    val isPrimary = bool("primary").default(false)
    val timeZone = text("time_zone").nullable()
    val isSelected = bool("selected").default(false)
    val isDeleted = bool("deleted").default(false)
    val isWritable = bool("writable").default(false)
    val providerData = jsonb<String>("provider_data", { it }, { it }).nullable()
    val lastSyncedAt =
        pgTimestamp("last_synced_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "provider_calendars_pkey")

    init {
        index("connection_selected_idx", false, calendarConnectionId, isSelected)
        index("primary_calendar_idx", false, isPrimary)
        index("sync_time_idx", false, lastSyncedAt)
        uniqueIndex("provider_calendars_calendar_connection_id_provider_calendar_key", calendarConnectionId, providerCalendarId)
    }
}
