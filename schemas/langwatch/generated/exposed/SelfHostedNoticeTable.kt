package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SelfHostedNoticeTable : IdTable<String>("\"SelfHostedNotice\"") {
    override val id = text("id").entityId()
    val severity = text("severity")
    val title = text("title")
    val body = text("body")
    val url = text("url").nullable()
    val fixedIn = text("fixedIn").nullable()
    val status = text("status")
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val publishedAt = pgTimestamp("publishedAt", 3).nullable()
    val publishedBy = text("publishedBy").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"SelfHostedNotice_pkey\"")
}
