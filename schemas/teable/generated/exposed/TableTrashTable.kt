package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TableTrashTable : IdTable<String>("table_trash") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val tableId = text("table_id")
    val resourceType = text("resource_type")
    val snapshot = text("snapshot")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    override val primaryKey = PrimaryKey(id, name = "table_trash_pkey")

    init {
        index("table_trash_table_id_idx", false, tableId)
    }
}
