package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AttachmentsTableTable : IdTable<String>("attachments_table") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val attachmentId = text("attachment_id")
    val name = text("name")
    val token = text("token")
    val tableId = text("table_id")
    val recordId = text("record_id")
    val fieldId = text("field_id")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val lastModifiedBy = text("last_modified_by").nullable()
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "attachments_table_pkey")

    init {
        index("attachments_table_table_id_record_id_idx", false, tableId, recordId)
        index("attachments_table_table_id_field_id_idx", false, tableId, fieldId)
        index("attachments_table_attachment_id_idx", false, attachmentId)
    }
}
