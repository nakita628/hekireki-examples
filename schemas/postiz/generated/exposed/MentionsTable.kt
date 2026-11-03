package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MentionsTable : CompositeIdTable("\"Mentions\"") {
    val name = text("name").entityId()
    val username = text("username").entityId()
    val platform = text("platform").entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val image = text("image").entityId()
    override val primaryKey = PrimaryKey(name, username, platform, image, name = "\"Mentions_pkey\"")

    init {
        index("\"Mentions_createdAt_idx\"", false, createdAt)
    }
}
