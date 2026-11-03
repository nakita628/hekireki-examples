package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object OrganizationBillingTable : IdTable<String>("\"OrganizationBilling\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganizationBilling_teamId_fkey\"",
        ).uniqueIndex("\"OrganizationBilling_teamId_key\"")
    val subscriptionId = text("subscriptionId").uniqueIndex("\"OrganizationBilling_subscriptionId_key\"")
    val subscriptionItemId = text("subscriptionItemId")
    val customerId = text("customerId")
    val status = text("status")
    val planName = text("planName")
    val subscriptionStart = pgTimestamp("subscriptionStart", 3).nullable()
    val subscriptionTrialEnd = pgTimestamp("subscriptionTrialEnd", 3).nullable()
    val subscriptionEnd = pgTimestamp("subscriptionEnd", 3).nullable()
    val billingPeriod = pgEnum("billingPeriod", "\"BillingPeriod\"", BillingPeriod.entries, BillingPeriod::dbName).nullable()
    val billingMode = pgEnum("billingMode", "\"BillingMode\"", BillingMode.entries, BillingMode::dbName).default(BillingMode.SEATS)
    val pricePerSeat = integer("pricePerSeat").nullable()
    val paidSeats = integer("paidSeats").nullable()
    val highWaterMark = integer("highWaterMark").nullable()
    val highWaterMarkPeriodStart = pgTimestamp("highWaterMarkPeriodStart", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"OrganizationBilling_pkey\"")
}
