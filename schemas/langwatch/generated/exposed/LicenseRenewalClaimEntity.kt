package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LicenseRenewalClaimEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LicenseRenewalClaimEntity>(LicenseRenewalClaimTable)

    var stripeSubscriptionId by LicenseRenewalClaimTable.stripeSubscriptionId
    var claimToken by LicenseRenewalClaimTable.claimToken
    var claimedAt by LicenseRenewalClaimTable.claimedAt
    var issuedLicense by LicenseRenewalClaimTable.issuedLicense
    var deliveredAt by LicenseRenewalClaimTable.deliveredAt
    var licenseId by LicenseRenewalClaimTable.licenseId
}
