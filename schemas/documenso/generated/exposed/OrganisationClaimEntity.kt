package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganisationClaimEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganisationClaimEntity>(OrganisationClaimTable)

    var createdAt by OrganisationClaimTable.createdAt
    var updatedAt by OrganisationClaimTable.updatedAt
    var originalSubscriptionClaimId by OrganisationClaimTable.originalSubscriptionClaimId
    val organisation by OrganisationEntity optionalBackReferencedOn OrganisationTable.organisationClaimId
    var teamCount by OrganisationClaimTable.teamCount
    var memberCount by OrganisationClaimTable.memberCount
    var envelopeItemCount by OrganisationClaimTable.envelopeItemCount
    var recipientCount by OrganisationClaimTable.recipientCount
    var flags by OrganisationClaimTable.flags
    var documentRateLimits by OrganisationClaimTable.documentRateLimits
    var documentQuota by OrganisationClaimTable.documentQuota
    var emailRateLimits by OrganisationClaimTable.emailRateLimits
    var emailQuota by OrganisationClaimTable.emailQuota
    var apiRateLimits by OrganisationClaimTable.apiRateLimits
    var apiQuota by OrganisationClaimTable.apiQuota
    var emailTransportId by OrganisationClaimTable.emailTransportId
    var emailTransport by EmailTransportEntity optionalReferencedOn OrganisationClaimTable.emailTransportId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganisationClaimTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
