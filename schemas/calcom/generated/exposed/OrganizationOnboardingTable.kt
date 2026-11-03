package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object OrganizationOnboardingTable : IdTable<String>("\"OrganizationOnboarding\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val createdById =
        reference(
            "createdById",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganizationOnboarding_createdById_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val orgOwnerEmail = text("orgOwnerEmail").uniqueIndex("\"OrganizationOnboarding_orgOwnerEmail_key\"")
    val error = text("error").nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val organizationId =
        optReference(
            "organizationId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganizationOnboarding_organizationId_fkey\"",
        ).uniqueIndex("\"OrganizationOnboarding_organizationId_key\"")
    val billingPeriod = pgEnum("billingPeriod", "\"BillingPeriod\"", BillingPeriod.entries, BillingPeriod::dbName)
    val pricePerSeat = double("pricePerSeat")
    val seats = integer("seats")
    val isPlatform = bool("isPlatform").default(false)
    val name = text("name")
    val slug = text("slug")
    val logo = text("logo").nullable()
    val bio = text("bio").nullable()
    val brandColor = text("brandColor").nullable()
    val bannerUrl = text("bannerUrl").nullable()
    val isDomainConfigured = bool("isDomainConfigured").default(false)
    val stripeCustomerId = text("stripeCustomerId").nullable().uniqueIndex("\"OrganizationOnboarding_stripeCustomerId_key\"")
    val stripeSubscriptionId = text("stripeSubscriptionId").nullable()
    val stripeSubscriptionItemId = text("stripeSubscriptionItemId").nullable()
    val invitedMembers = jsonb<String>("invitedMembers", { it }, { it }).default("[]")
    val teams = jsonb<String>("teams", { it }, { it }).default("[]")
    val isComplete = bool("isComplete").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"OrganizationOnboarding_pkey\"")

    init {
        index("\"OrganizationOnboarding_orgOwnerEmail_idx\"", false, orgOwnerEmail)
        index("\"OrganizationOnboarding_stripeCustomerId_idx\"", false, stripeCustomerId)
    }
}
