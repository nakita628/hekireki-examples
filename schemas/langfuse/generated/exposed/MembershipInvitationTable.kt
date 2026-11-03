package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MembershipInvitationTable : IdTable<String>("membership_invitations") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val email = text("email")
    val orgId =
        reference(
            "org_id",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "membership_invitations_org_id_fkey",
        )
    val orgRole = pgEnum("org_role", "\"Role\"", Role.entries, Role::dbName)
    val projectId =
        optReference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "membership_invitations_project_id_fkey",
        )
    val projectRole = pgEnum("project_role", "\"Role\"", Role.entries, Role::dbName).nullable()
    val invitedByUserId =
        optReference(
            "invited_by_user_id",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "membership_invitations_invited_by_user_id_fkey",
        )
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "membership_invitations_pkey")

    init {
        uniqueIndex("membership_invitations_id_key", id)
        index("membership_invitations_project_id_idx", false, projectId)
        index("membership_invitations_org_id_idx", false, orgId)
        index("membership_invitations_email_idx", false, email)
        uniqueIndex("membership_invitations_email_org_id_key", email, orgId)
    }
}
