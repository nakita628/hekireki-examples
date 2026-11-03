package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object PopularPostsTable : IdTable<String>("\"PopularPosts\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val category = text("category")
    val topic = text("topic")
    val content = text("content")
    val hook = text("hook")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"PopularPosts_pkey\"")
}
