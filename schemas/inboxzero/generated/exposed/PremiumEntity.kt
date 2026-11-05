package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PremiumEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PremiumEntity>(PremiumTable)

    var createdAt by PremiumTable.createdAt
    var updatedAt by PremiumTable.updatedAt
    val users by UserEntity optionalReferrersOn UserTable.premiumId
    val admins by UserEntity optionalReferrersOn UserTable.premiumAdminId
    var pendingInvites by PremiumTable.pendingInvites
    var lemonSqueezyRenewsAt by PremiumTable.lemonSqueezyRenewsAt
    var lemonSqueezyCustomerId by PremiumTable.lemonSqueezyCustomerId
    var lemonSqueezySubscriptionId by PremiumTable.lemonSqueezySubscriptionId
    var lemonSqueezySubscriptionItemId by PremiumTable.lemonSqueezySubscriptionItemId
    var lemonSqueezyOrderId by PremiumTable.lemonSqueezyOrderId
    var lemonSqueezyProductId by PremiumTable.lemonSqueezyProductId
    var lemonSqueezyVariantId by PremiumTable.lemonSqueezyVariantId
    var lemonLicenseKey by PremiumTable.lemonLicenseKey
    var lemonLicenseInstanceId by PremiumTable.lemonLicenseInstanceId
    var lemonSubscriptionStatus by PremiumTable.lemonSubscriptionStatus
    var stripeCustomerId by PremiumTable.stripeCustomerId
    var stripeSubscriptionId by PremiumTable.stripeSubscriptionId
    var stripeSubscriptionItemId by PremiumTable.stripeSubscriptionItemId
    var stripePriceId by PremiumTable.stripePriceId
    var stripeProductId by PremiumTable.stripeProductId
    var stripeSubscriptionStatus by PremiumTable.stripeSubscriptionStatus
    var stripeCancelAt by PremiumTable.stripeCancelAt
    var stripeCancelAtPeriodEnd by PremiumTable.stripeCancelAtPeriodEnd
    var stripeRenewsAt by PremiumTable.stripeRenewsAt
    var stripeTrialEnd by PremiumTable.stripeTrialEnd
    var stripeTrialConversionInvoiceId by PremiumTable.stripeTrialConversionInvoiceId
    var stripeTrialConvertedAt by PremiumTable.stripeTrialConvertedAt
    var stripeCancellationInitiatedAt by PremiumTable.stripeCancellationInitiatedAt
    var stripeCanceledAt by PremiumTable.stripeCanceledAt
    var stripeEndedAt by PremiumTable.stripeEndedAt
    var stripeCancellationFeedback by PremiumTable.stripeCancellationFeedback
    var stripeCancellationComment by PremiumTable.stripeCancellationComment
    var stripeAiOverageLastInvoiceId by PremiumTable.stripeAiOverageLastInvoiceId
    var stripeAiOverageLastPeriodEnd by PremiumTable.stripeAiOverageLastPeriodEnd
    var stripeAiOverageLastUnits by PremiumTable.stripeAiOverageLastUnits
    var stripeInvoiceEmailsEnabled by PremiumTable.stripeInvoiceEmailsEnabled
    var appleOriginalTransactionId by PremiumTable.appleOriginalTransactionId
    var appleLatestTransactionId by PremiumTable.appleLatestTransactionId
    var appleProductId by PremiumTable.appleProductId
    var appleSubscriptionGroupIdentifier by PremiumTable.appleSubscriptionGroupIdentifier
    var appleAppAccountToken by PremiumTable.appleAppAccountToken
    var appleSubscriptionStatus by PremiumTable.appleSubscriptionStatus
    var appleEnvironment by PremiumTable.appleEnvironment
    var applePurchaseDate by PremiumTable.applePurchaseDate
    var appleExpiresAt by PremiumTable.appleExpiresAt
    var appleRevokedAt by PremiumTable.appleRevokedAt
    var appleOfferDiscountType by PremiumTable.appleOfferDiscountType
    var adminGrantExpiresAt by PremiumTable.adminGrantExpiresAt
    var adminGrantTier by PremiumTable.adminGrantTier
    var tier by PremiumTable.tier
    var emailAccountsAccess by PremiumTable.emailAccountsAccess
    var unsubscribeMonth by PremiumTable.unsubscribeMonth
    var unsubscribeCredits by PremiumTable.unsubscribeCredits
    var aiMonth by PremiumTable.aiMonth
    var aiCredits by PremiumTable.aiCredits
    val payments by PaymentEntity optionalReferrersOn PaymentTable.premiumId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PremiumTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
