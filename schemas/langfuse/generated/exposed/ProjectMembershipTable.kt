package langfuse.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ProjectMembershipTable : CompositeIdTable("project_memberships") {
    val orgMembershipId =
        reference(
            "org_membership_id",
            OrganizationMembershipTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "project_memberships_org_membership_id_fkey",
        )
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "project_memberships_project_id_fkey",
        )
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "project_memberships_user_id_fkey",
        )
    val role = pgEnum("role", "\"Role\"", Role.entries, Role::dbName)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(projectId, userId, name = "project_memberships_pkey")

    init {
        addIdColumn(projectId)
        addIdColumn(userId)
        index("project_memberships_user_id_idx", false, userId)
        index("project_memberships_project_id_idx", false, projectId)
        index("project_memberships_org_membership_id_idx", false, orgMembershipId)
    }
}
