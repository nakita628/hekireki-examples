package postiz.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SubscriptionTable : IdTable<String>("\"Subscription\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Subscription_organizationId_fkey\"",
        ).uniqueIndex("\"Subscription_organizationId_key\"")
    val subscriptionTier = pgEnum("subscriptionTier", "\"SubscriptionTier\"", SubscriptionTier.entries, SubscriptionTier::dbName)
    val provider = text("provider").default("stripe")
    val identifier = text("identifier").nullable()
    val cancelAt = pgTimestamp("cancelAt", 3).nullable()
    val period = pgEnum("period", "\"Period\"", Period.entries, Period::dbName)
    val totalChannels = integer("totalChannels")
    val isLifetime = bool("isLifetime").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Subscription_pkey\"")

    init {
        index("\"Subscription_organizationId_idx\"", false, organizationId)
        index("\"Subscription_deletedAt_idx\"", false, deletedAt)
    }
}
