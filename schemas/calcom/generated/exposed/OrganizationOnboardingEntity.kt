package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganizationOnboardingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationOnboardingEntity>(OrganizationOnboardingTable)

    var createdBy by UserEntity referencedOn OrganizationOnboardingTable.createdById
    var createdById by OrganizationOnboardingTable.createdById
    var createdAt by OrganizationOnboardingTable.createdAt
    var orgOwnerEmail by OrganizationOnboardingTable.orgOwnerEmail
    var error by OrganizationOnboardingTable.error
    var updatedAt by OrganizationOnboardingTable.updatedAt
    var organizationId by OrganizationOnboardingTable.organizationId
    var organization by TeamEntity optionalReferencedOn OrganizationOnboardingTable.organizationId
    var billingPeriod by OrganizationOnboardingTable.billingPeriod
    var pricePerSeat by OrganizationOnboardingTable.pricePerSeat
    var seats by OrganizationOnboardingTable.seats
    var isPlatform by OrganizationOnboardingTable.isPlatform
    var name by OrganizationOnboardingTable.name
    var slug by OrganizationOnboardingTable.slug
    var logo by OrganizationOnboardingTable.logo
    var bio by OrganizationOnboardingTable.bio
    var brandColor by OrganizationOnboardingTable.brandColor
    var bannerUrl by OrganizationOnboardingTable.bannerUrl
    var isDomainConfigured by OrganizationOnboardingTable.isDomainConfigured
    var stripeCustomerId by OrganizationOnboardingTable.stripeCustomerId
    var stripeSubscriptionId by OrganizationOnboardingTable.stripeSubscriptionId
    var stripeSubscriptionItemId by OrganizationOnboardingTable.stripeSubscriptionItemId
    var invitedMembers by OrganizationOnboardingTable.invitedMembers
    var teams by OrganizationOnboardingTable.teams
    var isComplete by OrganizationOnboardingTable.isComplete

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationOnboardingTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
