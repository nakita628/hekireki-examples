package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationTable : IdTable<String>("organizations") {
    override val id = text("id").databaseDefault("gen_random_uuid()").entityId()
    val name = text("name")
    val slug = text("slug").nullable().uniqueIndex("organizations_slug_key")
    val createdByUserId =
        optReference(
            "createdByUserId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"organizations_createdByUserId_fkey\"",
        )
    val timezone = text("timezone").nullable()
    val onboarding = text("onboarding").nullable().default("completed")
    val attribution = jsonb<String>("attribution", { it }, { it }).nullable()
    val subscriptionId = text("subscriptionId").nullable()
    val subscriptionCustomerId = text("subscriptionCustomerId").nullable()
    val subscriptionPriceId = text("subscriptionPriceId").nullable()
    val subscriptionProductId = text("subscriptionProductId").nullable()
    val subscriptionStatus = text("subscriptionStatus").nullable()
    val subscriptionStartsAt = pgTimestamp("subscriptionStartsAt", 3).nullable()
    val subscriptionEndsAt = pgTimestamp("subscriptionEndsAt", 3).nullable()
    val subscriptionCanceledAt = pgTimestamp("subscriptionCanceledAt", 3).nullable()
    val subscriptionCreatedByUserId =
        optReference(
            "subscriptionCreatedByUserId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"organizations_subscriptionCreatedByUserId_fkey\"",
        )
    val subscriptionPeriodEventsCount = integer("subscriptionPeriodEventsCount").default(0)
    val subscriptionPeriodEventsCountExceededAt = pgTimestamp("subscriptionPeriodEventsCountExceededAt", 3).nullable()
    val subscriptionPeriodEventsLimit = integer("subscriptionPeriodEventsLimit").default(0)
    val subscriptionInterval = text("subscriptionInterval").nullable()
    val subscriptionCancelReason = text("subscriptionCancelReason").nullable()
    val subscriptionCancelComment = text("subscriptionCancelComment").nullable()
    val subscriptionSaveDiscountAppliedAt = pgTimestamp("subscriptionSaveDiscountAppliedAt", 3).nullable()
    val subscriptionPauseAtPeriodEnd = bool("subscriptionPauseAtPeriodEnd").default(false)
    val subscriptionResumesAt = pgTimestamp("subscriptionResumesAt", 3).nullable()
    val subscriptionFirstStartedAt = pgTimestamp("subscriptionFirstStartedAt", 3).nullable()
    val usageWarningSentAt = pgTimestamp("usageWarningSentAt", 3).nullable()
    val usageExceededSentAt = pgTimestamp("usageExceededSentAt", 3).nullable()
    val subscriptionDiscount = jsonb<String>("subscriptionDiscount", { it }, { it }).nullable()
    val windDownStartedAt = pgTimestamp("windDownStartedAt", 3).nullable()
    val windDownStep = text("windDownStep").nullable()
    val deleteAt = pgTimestamp("deleteAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "organizations_pkey")

    init {
        index("\"organizations_windDownStartedAt_idx\"", false, windDownStartedAt)
    }
}
