package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MemberProjectTable : CompositeIdTable("member_projects") {
    val memberId =
        reference(
            "memberId",
            MemberTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"member_projects_memberId_fkey\"",
        )
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"member_projects_projectId_fkey\"",
        )
    val level = pgEnum("level", "\"AccessLevel\"", AccessLevel.entries, AccessLevel::dbName).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(memberId, projectId, name = "member_projects_pkey")

    init {
        addIdColumn(memberId)
        addIdColumn(projectId)
        index("\"member_projects_projectId_idx\"", false, projectId)
    }
}
