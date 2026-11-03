package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object IntegrationTable : IdTable<String>("integration") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val resourceId = text("resource_id").uniqueIndex("integration_resource_id_key")
    val config = text("config")
    val type = text("type")
    val enable = bool("enable").nullable()
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "integration_pkey")

    init {
        index("integration_resource_id_idx", false, resourceId)
    }
}
