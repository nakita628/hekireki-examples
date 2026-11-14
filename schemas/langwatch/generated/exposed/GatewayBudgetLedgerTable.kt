package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object GatewayBudgetLedgerTable : IdTable<String>("\"GatewayBudgetLedger\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val budgetId = text("budgetId")
    val virtualKeyId = text("virtualKeyId")
    val modelProviderId = text("modelProviderId").nullable()
    val providerKey = text("providerKey").nullable()
    val gatewayRequestId = text("gatewayRequestId")
    val amountUsd = decimal("amountUsd", 18, 6)
    val tokensInput = integer("tokensInput").default(0)
    val tokensOutput = integer("tokensOutput").default(0)
    val tokensCacheRead = integer("tokensCacheRead").default(0)
    val tokensCacheWrite = integer("tokensCacheWrite").default(0)
    val model = text("model")
    val durationMs = integer("durationMs").nullable()
    val status =
        pgEnum("status", "\"GatewayBudgetLedgerStatus\"", GatewayBudgetLedgerStatus.entries, GatewayBudgetLedgerStatus::dbName).default(
            GatewayBudgetLedgerStatus.SUCCESS,
        )
    val occurredAt = pgTimestamp("occurredAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"GatewayBudgetLedger_pkey\"")

    init {
        index("\"GatewayBudgetLedger_virtualKeyId_idx\"", false, virtualKeyId)
        index("\"GatewayBudgetLedger_gatewayRequestId_idx\"", false, gatewayRequestId)
        index("\"GatewayBudgetLedger_occurredAt_idx\"", false, occurredAt)
        uniqueIndex("\"GatewayBudgetLedger_budgetId_gatewayRequestId_key\"", budgetId, gatewayRequestId)
    }
}
