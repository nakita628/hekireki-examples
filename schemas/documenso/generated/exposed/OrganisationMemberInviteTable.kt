package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganisationMemberInviteTable : IdTable<String>("\"OrganisationMemberInvite\"") {
    override val id = text("id").entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val email = text("email")
    val token = text("token").uniqueIndex("\"OrganisationMemberInvite_token_key\"")
    val status =
        pgEnum(
            "status",
            "\"OrganisationMemberInviteStatus\"",
            OrganisationMemberInviteStatus.entries,
            OrganisationMemberInviteStatus::dbName,
        ).default(OrganisationMemberInviteStatus.PENDING)
    val organisationId =
        reference(
            "organisationId",
            OrganisationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganisationMemberInvite_organisationId_fkey\"",
        )
    val organisationRole =
        pgEnum("organisationRole", "\"OrganisationMemberRole\"", OrganisationMemberRole.entries, OrganisationMemberRole::dbName)
    override val primaryKey = PrimaryKey(id, name = "\"OrganisationMemberInvite_pkey\"")
}
