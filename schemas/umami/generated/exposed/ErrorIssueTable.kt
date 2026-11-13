package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ErrorIssueTable : IdTable<UUID>("error_issue") {
    override val id = javaUUID("issue_id").entityId()
    val websiteId = javaUUID("website_id")
    val fingerprint = varchar("fingerprint", 64)
    val groupingVersion = integer("grouping_version").default(1)
    val title = varchar("title", 2200)
    val status = varchar("status", 20).default("unresolved")
    val resolvedAt = pgTimestamptz("resolved_at", 6).nullable()
    val createdAt = pgTimestamptz("created_at", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "error_issue_pkey")

    init {
        index("error_issue_website_id_status_idx", false, websiteId, status)
        uniqueIndex("error_issue_website_id_grouping_version_fingerprint_key", websiteId, groupingVersion, fingerprint)
    }
}
