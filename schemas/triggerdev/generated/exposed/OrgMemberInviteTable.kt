package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrgMemberInviteTable : IdTable<String>("\"OrgMemberInvite\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val token = text("token").clientDefault { CUID.randomCUID1().toString() }.uniqueIndex("\"OrgMemberInvite_token_key\"")
    val email = text("email")
    val role = pgEnum("role", "\"OrgMemberRole\"", OrgMemberRole.entries, OrgMemberRole::dbName).default(OrgMemberRole.MEMBER)
    val rbacRoleId = text("rbacRoleId").nullable()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrgMemberInvite_organizationId_fkey\"",
        )
    val inviterId =
        reference(
            "inviterId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrgMemberInvite_inviterId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"OrgMemberInvite_pkey\"")

    init {
        uniqueIndex("\"OrgMemberInvite_organizationId_email_key\"", organizationId, email)
    }
}
