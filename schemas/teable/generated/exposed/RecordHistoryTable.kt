package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RecordHistoryTable : IdTable<String>("record_history") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val tableId = text("table_id")
    val recordId = text("record_id")
    val fieldId = text("field_id")
    val before = text("before")
    val after = text("after")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    override val primaryKey = PrimaryKey(id, name = "record_history_pkey")

    init {
        index("record_history_table_id_record_id_created_time_idx", false, tableId, recordId, createdTime)
        index("record_history_table_id_created_time_idx", false, tableId, createdTime)
    }
}
