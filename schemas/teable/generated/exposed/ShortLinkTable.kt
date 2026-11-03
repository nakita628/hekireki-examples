package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ShortLinkTable : IdTable<String>("short_link") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val code = varchar("code", 32).uniqueIndex("short_link_code_key")
    val type = varchar("type", 32)
    val resourceId = text("resource_id")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val deletedTime = pgTimestamp("deleted_time", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "short_link_pkey")

    init {
        index("short_link_deleted_time_idx", false, deletedTime)
        uniqueIndex("short_link_type_resource_id_key", type, resourceId)
    }
}
