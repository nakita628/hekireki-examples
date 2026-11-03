package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganisationTable : IdTable<String>("\"Organisation\"") {
    override val id = text("id").entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val type = pgEnum("type", "\"OrganisationType\"", OrganisationType.entries, OrganisationType::dbName)
    val name = text("name")
    val url = text("url").uniqueIndex("\"Organisation_url_key\"")
    val avatarImageId =
        optReference(
            "avatarImageId",
            AvatarImageTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Organisation_avatarImageId_fkey\"",
        )
    val customerId = text("customerId").nullable().uniqueIndex("\"Organisation_customerId_key\"")
    val organisationClaimId =
        reference(
            "organisationClaimId",
            OrganisationClaimTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Organisation_organisationClaimId_fkey\"",
        ).uniqueIndex("\"Organisation_organisationClaimId_key\"")
    val ownerUserId =
        reference(
            "ownerUserId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Organisation_ownerUserId_fkey\"",
        )
    val organisationGlobalSettingsId =
        reference(
            "organisationGlobalSettingsId",
            OrganisationGlobalSettingsTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Organisation_organisationGlobalSettingsId_fkey\"",
        ).uniqueIndex("\"Organisation_organisationGlobalSettingsId_key\"")
    val organisationAuthenticationPortalId =
        reference(
            "organisationAuthenticationPortalId",
            OrganisationAuthenticationPortalTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Organisation_organisationAuthenticationPortalId_fkey\"",
        ).uniqueIndex("\"Organisation_organisationAuthenticationPortalId_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"Organisation_pkey\"")

    init {
        index("\"Organisation_name_idx\"", false, name)
        index("\"Organisation_ownerUserId_idx\"", false, ownerUserId)
    }
}
