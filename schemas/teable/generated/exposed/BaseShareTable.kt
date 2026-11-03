package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object BaseShareTable : IdTable<String>("base_share") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val baseId = text("base_id")
    val shareId = text("share_id").uniqueIndex("base_share_share_id_key")
    val password = text("password").nullable()
    val nodeId = text("node_id").nullable()
    val allowSave = bool("allow_save").nullable()
    val allowCopy = bool("allow_copy").nullable()
    val allowEdit = bool("allow_edit").nullable()
    val enabled = bool("enabled").default(true)
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "base_share_pkey")

    init {
        index("base_share_base_id_idx", false, baseId)
        uniqueIndex("base_share_base_id_node_id_key", baseId, nodeId)
    }
}
