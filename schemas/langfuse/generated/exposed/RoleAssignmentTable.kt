package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RoleAssignmentTable : IdTable<String>("role_assignments") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val ownerId = text("owner_id")
    val projectId =
        optReference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "role_assignments_project_id_fkey",
        )
    val orgId =
        reference(
            "org_id",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "role_assignments_org_id_fkey",
        )
    val principalId = text("principal_id")
    val userId =
        optReference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "role_assignments_user_id_fkey",
        )
    val apiKeyId =
        optReference(
            "api_key_id",
            ApiKeyTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "role_assignments_api_key_id_fkey",
        )
    val roleId = text("role_id")
    val systemRole = pgEnum("system_role", "\"SystemRole\"", SystemRole.entries, SystemRole::dbName)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "role_assignments_pkey")

    init {
        index("role_assignments_org_id_idx", false, orgId)
        index("role_assignments_project_id_idx", false, projectId)
        index("role_assignments_principal_id_idx", false, principalId)
        index("role_assignments_user_id_idx", false, userId)
        index("role_assignments_api_key_id_idx", false, apiKeyId)
        uniqueIndex("role_assignments_owner_id_principal_id_role_id_key", ownerId, principalId, roleId)
    }
}
