package teable.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SettingTable : IdTable<String>("setting") {
    override val id = text("name").entityId()
    val content = text("content").nullable()
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val lastModifiedBy = text("last_modified_by").nullable()

    init {
        uniqueIndex("setting_name_key", id)
    }
}
