package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ShareLinkTable : IdTable<String>("\"ShareLink\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val token = text("token").uniqueIndex("\"ShareLink_token_key\"")
    val resourceType = pgEnum("resourceType", "\"ShareResourceType\"", ShareResourceType.entries, ShareResourceType::dbName)
    val resourceId = text("resourceId")
    val threadId = text("threadId").nullable()
    val projectId = text("projectId")
    val userId = text("userId").nullable()
    val visibility =
        pgEnum("visibility", "\"ShareVisibility\"", ShareVisibility.entries, ShareVisibility::dbName).default(ShareVisibility.PUBLIC)
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val maxViews = integer("maxViews").nullable()
    val viewCount = integer("viewCount").default(0)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ShareLink_pkey\"")

    init {
        index("\"ShareLink_projectId_resourceType_resourceId_idx\"", false, projectId, resourceType, resourceId)
        index("\"ShareLink_userId_idx\"", false, userId)
    }
}
