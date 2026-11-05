package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object InviteTable : IdTable<String>("invites") {
    override val id = text("id").entityId()
    val email = text("email")
    val createdById =
        reference(
            "createdById",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"invites_createdById_fkey\"",
        )
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"invites_organizationId_fkey\"",
        )
    val projectAccess = jsonb<String>("projectAccess", { it }, { it }).default("[]")
    val projectScope = pgEnum("projectScope", "\"ProjectScope\"", ProjectScope.entries, ProjectScope::dbName).default(ProjectScope.ALL)
    val expiresAt = pgTimestamp("expiresAt", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val role = text("role")
    override val primaryKey = PrimaryKey(id, name = "invites_pkey")
}
