package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AttachmentsTable : IdTable<String>("attachments") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val token = text("token").uniqueIndex("attachments_token_key")
    val hash = text("hash")
    val size = long("size")
    val mimetype = text("mimetype")
    val path = text("path")
    val width = integer("width").nullable()
    val height = integer("height").nullable()
    val deletedTime = pgTimestamp("deleted_time", 3).nullable()
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val lastModifiedBy = text("last_modified_by").nullable()
    val thumbnailPath = text("thumbnail_path").nullable()
    override val primaryKey = PrimaryKey(id, name = "attachments_pkey")
}
