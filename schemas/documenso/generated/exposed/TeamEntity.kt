package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TeamEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, TeamEntity>(TeamTable)

    var name by TeamTable.name
    var url by TeamTable.url
    var createdAt by TeamTable.createdAt
    var avatarImageId by TeamTable.avatarImageId
    val teamEmail by TeamEmailEntity optionalBackReferencedOn TeamEmailTable.id
    val emailVerification by TeamEmailVerificationEntity optionalBackReferencedOn TeamEmailVerificationTable.id
    var avatarImage by AvatarImageEntity optionalReferencedOn TeamTable.avatarImageId
    val profile by TeamProfileEntity optionalBackReferencedOn TeamProfileTable.teamId
    val envelopes by EnvelopeEntity referrersOn EnvelopeTable.teamId
    val folders by FolderEntity referrersOn FolderTable.teamId
    val apiTokens by ApiTokenEntity referrersOn ApiTokenTable.teamId
    val webhooks by WebhookEntity referrersOn WebhookTable.teamId
    val teamGroups by TeamGroupEntity referrersOn TeamGroupTable.teamId
    var organisationId by TeamTable.organisationId
    var organisation by OrganisationEntity referencedOn TeamTable.organisationId
    var teamGlobalSettingsId by TeamTable.teamGlobalSettingsId
    var teamGlobalSettings by TeamGlobalSettingsEntity referencedOn TeamTable.teamGlobalSettingsId
}
