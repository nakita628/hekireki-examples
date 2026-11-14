package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ConnectedBillingAccountTable : IdTable<String>("\"ConnectedBillingAccount\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId").uniqueIndex("\"ConnectedBillingAccount_organizationId_key\"")
    val stripeCustomerId = text("stripeCustomerId").uniqueIndex("\"ConnectedBillingAccount_stripeCustomerId_key\"")
    val usageSubscriptionId = text("usageSubscriptionId").nullable()
    val usageSubscriptionItemId = text("usageSubscriptionItemId").nullable()
    val termStartsAt = pgTimestamp("termStartsAt", 3)
    val termEndsAt = pgTimestamp("termEndsAt", 3)
    val commitUsdCents = integer("commitUsdCents").default(0)
    val seatCurrency = pgEnum("seatCurrency", "\"Currency\"", Currency.entries, Currency::dbName)
    val seatRateCents = integer("seatRateCents")
    val seats = integer("seats")
    val bankTransferType = text("bankTransferType").nullable()
    val bankTransferCountry = text("bankTransferCountry").nullable()
    val billingEmail = text("billingEmail")
    val pendingRenewal = jsonb<String>("pendingRenewal", { it }, { it }).nullable()
    val createdById = text("createdById").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ConnectedBillingAccount_pkey\"")
}
