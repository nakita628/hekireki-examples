package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PremiumTable : IdTable<String>("\"Premium\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val pendingInvites = array("pendingInvites", TextColumnType()).nullable()
    val lemonSqueezyRenewsAt = pgTimestamp("lemonSqueezyRenewsAt", 3).nullable()
    val lemonSqueezyCustomerId = integer("lemonSqueezyCustomerId").nullable()
    val lemonSqueezySubscriptionId = integer("lemonSqueezySubscriptionId").nullable()
    val lemonSqueezySubscriptionItemId = integer("lemonSqueezySubscriptionItemId").nullable()
    val lemonSqueezyOrderId = integer("lemonSqueezyOrderId").nullable()
    val lemonSqueezyProductId = integer("lemonSqueezyProductId").nullable()
    val lemonSqueezyVariantId = integer("lemonSqueezyVariantId").nullable()
    val lemonLicenseKey = text("lemonLicenseKey").nullable()
    val lemonLicenseInstanceId = text("lemonLicenseInstanceId").nullable()
    val lemonSubscriptionStatus = text("lemonSubscriptionStatus").nullable()
    val stripeCustomerId = text("stripeCustomerId").nullable().uniqueIndex("\"Premium_stripeCustomerId_key\"")
    val stripeSubscriptionId = text("stripeSubscriptionId").nullable().uniqueIndex("\"Premium_stripeSubscriptionId_key\"")
    val stripeSubscriptionItemId = text("stripeSubscriptionItemId").nullable().uniqueIndex("\"Premium_stripeSubscriptionItemId_key\"")
    val stripePriceId = text("stripePriceId").nullable()
    val stripeProductId = text("stripeProductId").nullable()
    val stripeSubscriptionStatus = text("stripeSubscriptionStatus").nullable()
    val stripeCancelAt = pgTimestamp("stripeCancelAt", 3).nullable()
    val stripeCancelAtPeriodEnd = bool("stripeCancelAtPeriodEnd").nullable()
    val stripeRenewsAt = pgTimestamp("stripeRenewsAt", 3).nullable()
    val stripeTrialEnd = pgTimestamp("stripeTrialEnd", 3).nullable()
    val stripeTrialConversionInvoiceId = text("stripeTrialConversionInvoiceId").nullable()
    val stripeTrialConvertedAt = pgTimestamp("stripeTrialConvertedAt", 3).nullable()
    val stripeCancellationInitiatedAt = pgTimestamp("stripeCancellationInitiatedAt", 3).nullable()
    val stripeCanceledAt = pgTimestamp("stripeCanceledAt", 3).nullable()
    val stripeEndedAt = pgTimestamp("stripeEndedAt", 3).nullable()
    val stripeCancellationFeedback = text("stripeCancellationFeedback").nullable()
    val stripeCancellationComment = text("stripeCancellationComment").nullable()
    val stripeAiOverageLastInvoiceId = text("stripeAiOverageLastInvoiceId").nullable()
    val stripeAiOverageLastPeriodEnd = pgTimestamp("stripeAiOverageLastPeriodEnd", 3).nullable()
    val stripeAiOverageLastUnits = integer("stripeAiOverageLastUnits").nullable()
    val stripeInvoiceEmailsEnabled = bool("stripeInvoiceEmailsEnabled").default(false)
    val appleOriginalTransactionId = text("appleOriginalTransactionId").nullable().uniqueIndex("\"Premium_appleOriginalTransactionId_key\"")
    val appleLatestTransactionId = text("appleLatestTransactionId").nullable().uniqueIndex("\"Premium_appleLatestTransactionId_key\"")
    val appleProductId = text("appleProductId").nullable()
    val appleSubscriptionGroupIdentifier = text("appleSubscriptionGroupIdentifier").nullable()
    val appleAppAccountToken = text("appleAppAccountToken").nullable()
    val appleSubscriptionStatus = text("appleSubscriptionStatus").nullable()
    val appleEnvironment = text("appleEnvironment").nullable()
    val applePurchaseDate = pgTimestamp("applePurchaseDate", 3).nullable()
    val appleExpiresAt = pgTimestamp("appleExpiresAt", 3).nullable()
    val appleRevokedAt = pgTimestamp("appleRevokedAt", 3).nullable()
    val appleOfferDiscountType = text("appleOfferDiscountType").nullable()
    val adminGrantExpiresAt = pgTimestamp("adminGrantExpiresAt", 3).nullable()
    val adminGrantTier = pgEnum("adminGrantTier", "\"PremiumTier\"", PremiumTier.entries, PremiumTier::dbName).nullable()
    val tier = pgEnum("tier", "\"PremiumTier\"", PremiumTier.entries, PremiumTier::dbName).nullable()
    val emailAccountsAccess = integer("emailAccountsAccess").nullable()
    val unsubscribeMonth = integer("unsubscribeMonth").nullable()
    val unsubscribeCredits = integer("unsubscribeCredits").nullable()
    val aiMonth = integer("aiMonth").nullable()
    val aiCredits = integer("aiCredits").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Premium_pkey\"")

    init {
        index("\"Premium_pendingInvites_idx\"", false, pendingInvites)
    }
}
