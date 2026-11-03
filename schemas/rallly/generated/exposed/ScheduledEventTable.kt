package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ScheduledEventTable : IdTable<String>("scheduled_events") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "scheduled_events_user_id_fkey",
        )
    val spaceId =
        reference(
            "space_id",
            SpaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "scheduled_events_space_id_fkey",
        )
    val eventTypeId =
        optReference(
            "event_type_id",
            EventTypeTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "scheduled_events_event_type_id_fkey",
        )
    val sheetSlotId =
        optReference(
            "sheet_slot_id",
            SheetSlotTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "scheduled_events_sheet_slot_id_fkey",
        ).uniqueIndex("scheduled_events_sheet_slot_id_key")
    val title = text("title")
    val description = text("description").nullable()
    val location = jsonb<String>("location", { it }, { it }).nullable()
    val conferencing = jsonb<String>("conferencing", { it }, { it }).nullable()
    val capacity = integer("capacity").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val status =
        pgEnum("status", "\"scheduled_event_status\"", ScheduledEventStatus.entries, ScheduledEventStatus::dbName).default(
            ScheduledEventStatus.CONFIRMED,
        )
    val timeZone = text("time_zone").nullable()
    val start = pgTimestamp("start", 3)
    val end = pgTimestamp("end", 3)
    val allDay = bool("all_day").default(false)
    val deletedAt = pgTimestamp("deleted_at", 3).nullable()
    val sequence = integer("sequence").default(0)
    val uid = text("uid").uniqueIndex("scheduled_events_uid_key")
    val hideAttendees = bool("hide_attendees").default(false)
    override val primaryKey = PrimaryKey(id, name = "scheduled_events_pkey")

    init {
        index("scheduled_events_space_id_idx", false, spaceId)
        index("scheduled_events_user_id_idx", false, userId)
        index("scheduled_events_event_type_id_idx", false, eventTypeId)
        index("scheduled_events_space_id_status_start_idx", false, spaceId, status, start)
    }
}
