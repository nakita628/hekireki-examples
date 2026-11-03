package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RescheduledEventDateTable : IdTable<String>("rescheduled_event_dates") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val scheduledEventId =
        reference(
            "scheduled_event_id",
            ScheduledEventTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "rescheduled_event_dates_scheduled_event_id_fkey",
        )
    val start = pgTimestamp("start", 3)
    val end = pgTimestamp("end", 3)
    val allDay = bool("all_day").default(false)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "rescheduled_event_dates_pkey")

    init {
        index("rescheduled_event_dates_scheduled_event_id_idx", false, scheduledEventId)
    }
}
