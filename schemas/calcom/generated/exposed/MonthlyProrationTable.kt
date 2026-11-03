package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object MonthlyProrationTable : IdTable<String>("\"MonthlyProration\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MonthlyProration_teamId_fkey\"",
        )
    val monthKey = text("monthKey")
    val periodStart = pgTimestamp("periodStart", 3)
    val periodEnd = pgTimestamp("periodEnd", 3)
    val seatsAtStart = integer("seatsAtStart")
    val seatsAdded = integer("seatsAdded")
    val seatsRemoved = integer("seatsRemoved")
    val netSeatIncrease = integer("netSeatIncrease")
    val seatsAtEnd = integer("seatsAtEnd")
    val subscriptionId = text("subscriptionId")
    val subscriptionItemId = text("subscriptionItemId")
    val customerId = text("customerId")
    val subscriptionStart = pgTimestamp("subscriptionStart", 3)
    val subscriptionEnd = pgTimestamp("subscriptionEnd", 3)
    val remainingDays = integer("remainingDays")
    val pricePerSeat = integer("pricePerSeat")
    val proratedAmount = integer("proratedAmount")
    val invoiceItemId = text("invoiceItemId").nullable()
    val invoiceId = text("invoiceId").nullable()
    val invoiceUrl = text("invoiceUrl").nullable()
    val status = pgEnum("status", "\"ProrationStatus\"", ProrationStatus.entries, ProrationStatus::dbName).default(ProrationStatus.PENDING)
    val chargedAt = pgTimestamp("chargedAt", 3).nullable()
    val failedAt = pgTimestamp("failedAt", 3).nullable()
    val failureReason = text("failureReason").nullable()
    val retryCount = integer("retryCount").default(0)
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val teamBillingId =
        optReference(
            "teamBillingId",
            TeamBillingTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MonthlyProration_teamBillingId_fkey\"",
        )
    val organizationBillingId =
        optReference(
            "organizationBillingId",
            OrganizationBillingTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MonthlyProration_organizationBillingId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"MonthlyProration_pkey\"")

    init {
        index("\"MonthlyProration_monthKey_status_idx\"", false, monthKey, status)
        index("\"MonthlyProration_status_idx\"", false, status)
        index("\"MonthlyProration_teamId_idx\"", false, teamId)
        uniqueIndex("\"MonthlyProration_teamId_monthKey_key\"", teamId, monthKey)
    }
}
