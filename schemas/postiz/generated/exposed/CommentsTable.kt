package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object CommentsTable : IdTable<String>("\"Comments\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val content = text("content")
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Comments_organizationId_fkey\"",
        )
    val postId =
        reference(
            "postId",
            PostTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Comments_postId_fkey\"",
        )
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Comments_userId_fkey\"",
        )
    val displayName = text("displayName").nullable()
    val parentId =
        optReference(
            "parentId",
            CommentsTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Comments_parentId_fkey\"",
        )
    val anchorStart = integer("anchorStart").nullable()
    val anchorEnd = integer("anchorEnd").nullable()
    val anchorQuote = text("anchorQuote").nullable()
    val resolvedAt = pgTimestamp("resolvedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Comments_pkey\"")

    init {
        index("\"Comments_createdAt_idx\"", false, createdAt)
        index("\"Comments_organizationId_idx\"", false, organizationId)
        index("\"Comments_userId_idx\"", false, userId)
        index("\"Comments_postId_idx\"", false, postId)
        index("\"Comments_parentId_idx\"", false, parentId)
        index("\"Comments_resolvedAt_idx\"", false, resolvedAt)
        index("\"Comments_deletedAt_idx\"", false, deletedAt)
    }
}
