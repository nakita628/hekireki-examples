package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CommentSubscriptionTable : IdTable<String>("comment_subscription") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val tableId = text("table_id")
    val recordId = text("record_id")
    val createdBy = text("created_by")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "comment_subscription_pkey")

    init {
        index("comment_subscription_table_id_record_id_idx", false, tableId, recordId)
        uniqueIndex("comment_subscription_table_id_record_id_key", tableId, recordId)
    }
}
