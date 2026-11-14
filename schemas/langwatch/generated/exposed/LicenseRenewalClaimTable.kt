package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object LicenseRenewalClaimTable : IdTable<String>("\"LicenseRenewalClaim\"") {
    override val id = text("stripeInvoiceId").entityId()
    val stripeSubscriptionId = text("stripeSubscriptionId")
    val claimToken = text("claimToken")
    val claimedAt = pgTimestamp("claimedAt", 3)
    val issuedLicense = jsonb<String>("issuedLicense", { it }, { it }).nullable()
    val deliveredAt = pgTimestamp("deliveredAt", 3).nullable()
    val licenseId = text("licenseId").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LicenseRenewalClaim_pkey\"")
}
