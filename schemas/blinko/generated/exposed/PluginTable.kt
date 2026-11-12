package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.json
import java.time.Instant
import java.time.temporal.ChronoUnit

object PluginTable : IdTable<Int>("plugin") {
    override val id = integer("id").autoIncrement().entityId()
    val metadata = json<String>("metadata", { it }, { it })
    val path = pgVarchar("path")
    val isUse = bool("isUse").default(true)
    val isDev = bool("isDev").default(false)
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "plugin_pkey")
}
