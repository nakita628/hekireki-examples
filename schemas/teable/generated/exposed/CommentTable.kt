package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CommentTable : IdTable<String>("comment") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val tableId = text("table_id")
    val recordId = text("record_id")
    val quoteId = text("quote_Id").nullable()
    val content = text("content").nullable()
    val reaction = text("reaction").nullable()
    val deletedTime = pgTimestamp("deleted_time", 3).nullable()
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "comment_pkey")

    init {
        index("comment_table_id_record_id_idx", false, tableId, recordId)
    }
}
