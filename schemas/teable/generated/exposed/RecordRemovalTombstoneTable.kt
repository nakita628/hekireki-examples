package teable.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RecordRemovalTombstoneTable : IdTable<String>("record_removal_tombstone") {
    override val id = text("id").entityId()
    val tableId = text("table_id")
    val recordId = text("record_id")
    val type = text("type")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "record_removal_tombstone_pkey")

    init {
        index("record_removal_tombstone_table_id_record_id_idx", false, tableId, recordId)
    }
}
