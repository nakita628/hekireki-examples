package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RecordTrashTable : IdTable<String>("record_trash") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val tableId = text("table_id")
    val recordId = text("record_id")
    val snapshot = text("snapshot")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val reason = text("reason").default("deleted")
    val recordCreatedTime = pgTimestamp("record_created_time", 3).nullable()
    val recordCreatedBy = text("record_created_by").nullable()
    val recordLastModifiedTime = pgTimestamp("record_last_modified_time", 3).nullable()
    val recordLastModifiedBy = text("record_last_modified_by").nullable()
    val operationId = text("operation_id").nullable()
    override val primaryKey = PrimaryKey(id, name = "record_trash_pkey")

    init {
        index("record_trash_table_id_record_id_idx", false, tableId, recordId)
    }
}
