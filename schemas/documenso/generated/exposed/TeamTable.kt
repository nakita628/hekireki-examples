package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TeamTable : IdTable<Int>("\"Team\"") {
    override val id = integer("id").autoIncrement().entityId()
    val name = text("name")
    val url = text("url").uniqueIndex("\"Team_url_key\"")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val avatarImageId =
        optReference(
            "avatarImageId",
            AvatarImageTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Team_avatarImageId_fkey\"",
        )
    val organisationId =
        reference(
            "organisationId",
            OrganisationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Team_organisationId_fkey\"",
        )
    val teamGlobalSettingsId =
        reference(
            "teamGlobalSettingsId",
            TeamGlobalSettingsTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Team_teamGlobalSettingsId_fkey\"",
        ).uniqueIndex("\"Team_teamGlobalSettingsId_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"Team_pkey\"")

    init {
        index("\"Team_name_idx\"", false, name)
        index("\"Team_organisationId_idx\"", false, organisationId)
    }
}
