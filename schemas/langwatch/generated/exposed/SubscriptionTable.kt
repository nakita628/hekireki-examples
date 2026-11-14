package langwatch.models

import org.jetbrains.exposed.v1.core.IntegerColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SubscriptionTable : IdTable<String>("\"Subscription\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val plan = pgEnum("plan", "\"PlanTypes\"", PlanTypes.entries, PlanTypes::dbName)
    val stripeSubscriptionId = text("stripeSubscriptionId").nullable().uniqueIndex("\"Subscription_stripeSubscriptionId_key\"")
    val status = pgEnum("status", "\"SubscriptionStatus\"", SubscriptionStatus.entries, SubscriptionStatus::dbName)
    val startDate = pgTimestamp("startDate", 3).nullable()
    val endDate = pgTimestamp("endDate", 3).nullable()
    val lastPaymentFailedDate = pgTimestamp("lastPaymentFailedDate", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val maxMembers = integer("maxMembers").nullable()
    val maxProjects = integer("maxProjects").nullable()
    val maxMessagesPerMonth = integer("maxMessagesPerMonth").nullable()
    val maxWorkflows = integer("maxWorkflows").nullable()
    val maxRetentionDays = integer("maxRetentionDays").nullable()
    val maxMembersLite = integer("maxMembersLite").nullable()
    val maxMembersDeveloper = integer("maxMembersDeveloper").nullable()
    val maxTeams = integer("maxTeams").nullable()
    val maxPrompts = integer("maxPrompts").nullable()
    val maxEvaluators = integer("maxEvaluators").nullable()
    val maxScenarios = integer("maxScenarios").nullable()
    val maxAgents = integer("maxAgents").nullable()
    val maxExperiments = integer("maxExperiments").nullable()
    val maxOnlineEvaluations = integer("maxOnlineEvaluations").nullable()
    val maxDatasets = integer("maxDatasets").nullable()
    val maxDashboards = integer("maxDashboards").nullable()
    val maxCustomGraphs = integer("maxCustomGraphs").nullable()
    val maxAutomations = integer("maxAutomations").nullable()
    val seatsKept = integer("seatsKept").nullable()
    val seatsKeptUntil = pgTimestamp("seatsKeptUntil", 3).nullable()
    val seatLineQuantity = integer("seatLineQuantity").nullable()
    val seatLineQuantityAt = pgTimestamp("seatLineQuantityAt", 3).nullable()
    val seatLineWriteQuantities = array("seatLineWriteQuantities", IntegerColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val seatLineWriteSince = pgTimestamp("seatLineWriteSince", 3).nullable()
    val seatLineWriteAt = pgTimestamp("seatLineWriteAt", 3).nullable()
    val seatLineWriteAttempts = integer("seatLineWriteAttempts").nullable()
    val seatLineWriteFlaggedAt = pgTimestamp("seatLineWriteFlaggedAt", 3).nullable()
    val seatRepairQuantity = integer("seatRepairQuantity").nullable()
    val seatRepairLiftedQuantity = integer("seatRepairLiftedQuantity").nullable()
    val seatRepairSince = pgTimestamp("seatRepairSince", 3).nullable()
    val seatRepairAttempts = integer("seatRepairAttempts").nullable()
    val seatRepairFlaggedAt = pgTimestamp("seatRepairFlaggedAt", 3).nullable()
    val seatLineLeaseToken = text("seatLineLeaseToken").nullable()
    val seatLineLeaseUntil = pgTimestamp("seatLineLeaseUntil", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Subscription_pkey\"")

    init {
        index("\"Subscription_organizationId_idx\"", false, organizationId)
    }
}
