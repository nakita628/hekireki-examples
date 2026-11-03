package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TrashTable : IdTable<String>("trash") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val resourceType = text("resource_type")
    val resourceId = text("resource_id")
    val parentId = text("parent_id").nullable()
    val deletedTime = pgTimestamp("deleted_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedBy = text("deleted_by")
    override val primaryKey = PrimaryKey(id, name = "trash_pkey")

    init {
        uniqueIndex("trash_resource_type_resource_id_key", resourceType, resourceId)
    }
}
