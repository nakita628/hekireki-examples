package plunk.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ProjectTable : IdTable<String>("projects") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name")
    val public = text("public").uniqueIndex("projects_public_key")
    val secret = text("secret").uniqueIndex("projects_secret_key")
    val disabled = bool("disabled").default(false)
    val disabledReason =
        pgEnum("disabledReason", "\"ProjectDisabledReason\"", ProjectDisabledReason.entries, ProjectDisabledReason::dbName).nullable()
    val customer = text("customer").nullable().uniqueIndex("projects_customer_key")
    val subscription = text("subscription").nullable().uniqueIndex("projects_subscription_key")
    val cardVerification =
        pgEnum("cardVerification", "\"CardVerificationStatus\"", CardVerificationStatus.entries, CardVerificationStatus::dbName).nullable()
    val cardVerificationIntent = text("cardVerificationIntent").nullable()
    val cardVerificationAt = pgTimestamp("cardVerificationAt", 3).nullable()
    val cardVerificationSession = text("cardVerificationSession").nullable()
    val cardVerificationPaymentMethod = text("cardVerificationPaymentMethod").nullable()
    val cardVerificationFingerprint = text("cardVerificationFingerprint").nullable()
    val cardVerificationAttempts = integer("cardVerificationAttempts").default(0)
    val billingLimitWorkflows = integer("billingLimitWorkflows").nullable()
    val billingLimitCampaigns = integer("billingLimitCampaigns").nullable()
    val billingLimitTransactional = integer("billingLimitTransactional").nullable()
    val billingLimitInbound = integer("billingLimitInbound").nullable()
    val tracking = pgEnum("tracking", "\"TrackingMode\"", TrackingMode.entries, TrackingMode::dbName).default(TrackingMode.ENABLED)
    val language = text("language").default("en")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "projects_pkey")

    init {
        index("\"projects_cardVerification_cardVerificationAt_idx\"", false, cardVerification, cardVerificationAt)
    }
}
