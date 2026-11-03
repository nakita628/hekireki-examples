package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SheetSlotTable : IdTable<String>("sheet_slots") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val sheetId =
        reference(
            "sheet_id",
            SheetTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "sheet_slots_sheet_id_fkey",
        )
    val eventTypeId =
        reference(
            "event_type_id",
            EventTypeTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "sheet_slots_event_type_id_fkey",
        )
    val startTime = pgTimestamp("start_time", 3)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "sheet_slots_pkey")

    init {
        index("sheet_slots_sheet_id_start_time_idx", false, sheetId, startTime)
        index("sheet_slots_event_type_id_idx", false, eventTypeId)
    }
}
