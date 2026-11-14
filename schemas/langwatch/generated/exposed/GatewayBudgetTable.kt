package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.math.BigDecimal
import java.time.Instant
import java.time.temporal.ChronoUnit

object GatewayBudgetTable : IdTable<String>("\"GatewayBudget\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val scopeType = pgEnum("scopeType", "\"GatewayBudgetScopeType\"", GatewayBudgetScopeType.entries, GatewayBudgetScopeType::dbName)
    val scopeId = text("scopeId")
    val providerKey = text("providerKey").nullable()
    val name = text("name")
    val description = text("description").nullable()
    val window = pgEnum("window", "\"GatewayBudgetWindow\"", GatewayBudgetWindow.entries, GatewayBudgetWindow::dbName)
    val limitUsd = decimal("limitUsd", 18, 6)
    val onBreach =
        pgEnum("onBreach", "\"GatewayBudgetBreachAction\"", GatewayBudgetBreachAction.entries, GatewayBudgetBreachAction::dbName).default(
            GatewayBudgetBreachAction.BLOCK,
        )
    val timezone = text("timezone").nullable()
    val externalId = text("externalId").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).default("{}")
    val spentUsd = decimal("spentUsd", 18, 6).default(BigDecimal("0"))
    val currentPeriodStartedAt =
        pgTimestamp("currentPeriodStartedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val resetsAt = pgTimestamp("resetsAt", 3)
    val lastResetAt = pgTimestamp("lastResetAt", 3).nullable()
    val cycleAnchorAt = pgTimestamp("cycleAnchorAt", 3).nullable()
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdById = text("createdById")
    val managedByVirtualKeyId = text("managedByVirtualKeyId").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"GatewayBudget_pkey\"")

    init {
        index("\"GatewayBudget_organizationId_idx\"", false, organizationId)
        index("\"GatewayBudget_scopeType_scopeId_providerKey_idx\"", false, scopeType, scopeId, providerKey)
        index("\"GatewayBudget_managedByVirtualKeyId_idx\"", false, managedByVirtualKeyId)
        index("\"GatewayBudget_resetsAt_idx\"", false, resetsAt)
        uniqueIndex("\"GatewayBudget_organizationId_externalId_key\"", organizationId, externalId)
    }
}
