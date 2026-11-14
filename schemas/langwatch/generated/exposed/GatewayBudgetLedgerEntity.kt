package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class GatewayBudgetLedgerEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GatewayBudgetLedgerEntity>(GatewayBudgetLedgerTable)

    var budgetId by GatewayBudgetLedgerTable.budgetId
    var virtualKeyId by GatewayBudgetLedgerTable.virtualKeyId
    var modelProviderId by GatewayBudgetLedgerTable.modelProviderId
    var providerKey by GatewayBudgetLedgerTable.providerKey
    var gatewayRequestId by GatewayBudgetLedgerTable.gatewayRequestId
    var amountUsd by GatewayBudgetLedgerTable.amountUsd
    var tokensInput by GatewayBudgetLedgerTable.tokensInput
    var tokensOutput by GatewayBudgetLedgerTable.tokensOutput
    var tokensCacheRead by GatewayBudgetLedgerTable.tokensCacheRead
    var tokensCacheWrite by GatewayBudgetLedgerTable.tokensCacheWrite
    var model by GatewayBudgetLedgerTable.model
    var durationMs by GatewayBudgetLedgerTable.durationMs
    var status by GatewayBudgetLedgerTable.status
    var occurredAt by GatewayBudgetLedgerTable.occurredAt
}
