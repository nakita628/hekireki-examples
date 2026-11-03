package teable.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ViewTable : IdTable<String>("view") {
    override val id = text("id").entityId()
    val name = text("name")
    val description = text("description").nullable()
    val tableId =
        reference(
            "table_id",
            TableMetaTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "view_table_id_fkey",
        )
    val type = text("type")
    val sort = text("sort").nullable()
    val filter = text("filter").nullable()
    val group = text("group").nullable()
    val options1 = text("options").nullable()
    val order = double("order")
    val version = integer("version")
    val columnMeta = text("column_meta")
    val isLocked = bool("is_locked").nullable()
    val enableShare = bool("enable_share").nullable()
    val shareId = text("share_id").nullable().uniqueIndex("view_share_id_key")
    val shareMeta = text("share_meta").nullable()
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedTime = pgTimestamp("deleted_time", 3).nullable()
    val createdBy = text("created_by")
    val lastModifiedBy = text("last_modified_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "view_pkey")

    init {
        index("view_order_idx", false, order)
        index("view_table_id_deleted_time_idx", false, tableId, deletedTime)
    }
}
