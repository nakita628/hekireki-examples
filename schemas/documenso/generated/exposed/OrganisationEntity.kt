package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganisationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganisationEntity>(OrganisationTable)

    var createdAt by OrganisationTable.createdAt
    var updatedAt by OrganisationTable.updatedAt
    var type by OrganisationTable.type
    var name by OrganisationTable.name
    var url by OrganisationTable.url
    var avatarImageId by OrganisationTable.avatarImageId
    var customerId by OrganisationTable.customerId
    val subscription by SubscriptionEntity optionalBackReferencedOn SubscriptionTable.organisationId
    var organisationClaimId by OrganisationTable.organisationClaimId
    var organisationClaim by OrganisationClaimEntity referencedOn OrganisationTable.organisationClaimId
    val members by OrganisationMemberEntity referrersOn OrganisationMemberTable.organisationId
    val invites by OrganisationMemberInviteEntity referrersOn OrganisationMemberInviteTable.organisationId
    val groups by OrganisationGroupEntity referrersOn OrganisationGroupTable.organisationId
    val teams by TeamEntity referrersOn TeamTable.organisationId
    val emailDomains by EmailDomainEntity referrersOn EmailDomainTable.organisationId
    val organisationEmails by OrganisationEmailEntity referrersOn OrganisationEmailTable.organisationId
    val monthlyStats by OrganisationMonthlyStatEntity referrersOn OrganisationMonthlyStatTable.organisationId
    val embedSignatures by EmbedSignatureEntity referrersOn EmbedSignatureTable.organisationId
    var avatarImage by AvatarImageEntity optionalReferencedOn OrganisationTable.avatarImageId
    var ownerUserId by OrganisationTable.ownerUserId
    var owner by UserEntity referencedOn OrganisationTable.ownerUserId
    var organisationGlobalSettingsId by OrganisationTable.organisationGlobalSettingsId
    var organisationGlobalSettings by OrganisationGlobalSettingsEntity referencedOn OrganisationTable.organisationGlobalSettingsId
    var organisationAuthenticationPortalId by OrganisationTable.organisationAuthenticationPortalId
    var organisationAuthenticationPortal by OrganisationAuthenticationPortalEntity referencedOn
        OrganisationTable.organisationAuthenticationPortalId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganisationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
