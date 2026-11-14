package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ScimUserResourceTable : CompositeIdTable("\"ScimUserResource\"") {
    val organizationId = text("organizationId").entityId()
    val userId = text("userId").entityId()
    val userName = text("userName")
    val name = text("name").nullable()
    val active = bool("active").default(true)
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(organizationId, userId, name = "\"ScimUserResource_pkey\"")

    init {
        index("\"ScimUserResource_organizationId_userName_idx\"", false, organizationId, userName)
        index("\"ScimUserResource_userId_idx\"", false, userId)
    }
}
