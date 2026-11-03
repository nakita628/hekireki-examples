package formbricks.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object InviteTable : IdTable<String>("\"Invite\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val email = text("email")
    val name = text("name").nullable()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Invite_organizationId_fkey\"",
        )
    val creatorId =
        reference(
            "creatorId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Invite_creatorId_fkey\"",
        )
    val acceptorId =
        optReference(
            "acceptorId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Invite_acceptorId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val expiresAt = pgTimestamp("expiresAt", 3)
    val role = pgEnum("role", "\"OrganizationRole\"", OrganizationRole.entries, OrganizationRole::dbName).default(OrganizationRole.MEMBER)
    val teamIds = array("teamIds", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    override val primaryKey = PrimaryKey(id, name = "\"Invite_pkey\"")

    init {
        index("\"Invite_email_organizationId_idx\"", false, email, organizationId)
        index("\"Invite_organizationId_idx\"", false, organizationId)
    }
}
