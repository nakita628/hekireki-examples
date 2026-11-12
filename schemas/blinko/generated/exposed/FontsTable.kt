package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.json
import java.time.Instant
import java.time.temporal.ChronoUnit

object FontsTable : IdTable<Int>("fonts") {
    override val id = integer("id").autoIncrement().entityId()
    val name = varchar("name", 255).uniqueIndex("fonts_name_key")
    val displayName = varchar("displayName", 255)
    val url = text("url").nullable()
    val fileData = binary("fileData").nullable()
    val isLocal = bool("isLocal").default(false)
    val isSystem = bool("isSystem").default(false)
    val weights = json<String>("weights", { it }, { it })
    val category = varchar("category", 50).default("sans-serif")
    val sortOrder = integer("sortOrder").default(0)
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "fonts_pkey")
}
