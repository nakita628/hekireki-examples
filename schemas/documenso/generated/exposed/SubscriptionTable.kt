package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SubscriptionTable : IdTable<Int>("\"Subscription\"") {
    override val id = integer("id").autoIncrement().entityId()
    val status =
        pgEnum("status", "\"SubscriptionStatus\"", SubscriptionStatus.entries, SubscriptionStatus::dbName).default(
            SubscriptionStatus.INACTIVE,
        )
    val planId = text("planId").uniqueIndex("\"Subscription_planId_key\"")
    val priceId = text("priceId")
    val periodEnd = pgTimestamp("periodEnd", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val cancelAtPeriodEnd = bool("cancelAtPeriodEnd").default(false)
    val customerId = text("customerId")
    val organisationId =
        reference(
            "organisationId",
            OrganisationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Subscription_organisationId_fkey\"",
        ).uniqueIndex("\"Subscription_organisationId_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"Subscription_pkey\"")

    init {
        index("\"Subscription_organisationId_idx\"", false, organisationId)
    }
}
