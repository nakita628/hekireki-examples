package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object BaseNodeTable : IdTable<String>("base_node") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val parentId =
        optReference(
            "parent_id",
            BaseNodeTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "base_node_parent_id_fkey",
        )
    val baseId = text("base_id")
    val resourceType = text("resource_type")
    val resourceId = text("resource_id")
    val order = double("order")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedBy = text("last_modified_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "base_node_pkey")

    init {
        uniqueIndex("base_node_base_id_resource_type_resource_id_key", baseId, resourceType, resourceId)
    }
}
