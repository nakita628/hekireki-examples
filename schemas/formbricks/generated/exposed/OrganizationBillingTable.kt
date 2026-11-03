package formbricks.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationBillingTable : IdTable<String>("\"OrganizationBilling\"") {
    override val id =
        text("organization_id")
            .references(
                OrganizationTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"OrganizationBilling_organization_id_fkey\"",
            ).entityId()
    val stripeCustomerId = text("stripe_customer_id").nullable().uniqueIndex("\"OrganizationBilling_stripe_customer_id_key\"")
    val limits = jsonb<String>("limits", { it }, { it })
    val usageCycleAnchor = pgTimestamp("usage_cycle_anchor", 3).nullable()
    val stripe = jsonb<String>("stripe", { it }, { it }).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"OrganizationBilling_pkey\"")
}
