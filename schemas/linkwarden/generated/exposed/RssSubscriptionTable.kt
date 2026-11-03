package linkwarden.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RssSubscriptionTable : IdTable<Int>("\"RssSubscription\"") {
    override val id = integer("id").autoIncrement().entityId()
    val url = text("url")
    val name = text("name")
    val lastBuildDate = pgTimestamp("lastBuildDate", 3).nullable()
    val collectionId =
        reference(
            "collectionId",
            CollectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RssSubscription_collectionId_fkey\"",
        )
    val ownerId = integer("ownerId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"RssSubscription_pkey\"")
}
