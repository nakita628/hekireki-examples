package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object BaseNodeFolderTable : IdTable<String>("base_node_folder") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val baseId = text("base_id")
    val name = text("name")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedBy = text("last_modified_by").nullable()
    val deletedTime = pgTimestamp("deleted_time", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "base_node_folder_pkey")

    init {
        index("base_node_folder_base_id_idx", false, baseId)
    }
}
