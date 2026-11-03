package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskTable : IdTable<String>("task") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val type = text("type")
    val status = text("status")
    val snapshot = text("snapshot").nullable()
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val lastModifiedBy = text("last_modified_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "task_pkey")

    init {
        index("task_type_status_idx", false, type, status)
    }
}
