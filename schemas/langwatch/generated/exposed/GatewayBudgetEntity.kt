package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GatewayBudgetEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GatewayBudgetEntity>(GatewayBudgetTable)

    var organizationId by GatewayBudgetTable.organizationId
    var scopeType by GatewayBudgetTable.scopeType
    var scopeId by GatewayBudgetTable.scopeId
    var providerKey by GatewayBudgetTable.providerKey
    var name by GatewayBudgetTable.name
    var description by GatewayBudgetTable.description
    var window by GatewayBudgetTable.window
    var limitUsd by GatewayBudgetTable.limitUsd
    var onBreach by GatewayBudgetTable.onBreach
    var timezone by GatewayBudgetTable.timezone
    var externalId by GatewayBudgetTable.externalId
    var metadata by GatewayBudgetTable.metadata
    var spentUsd by GatewayBudgetTable.spentUsd
    var currentPeriodStartedAt by GatewayBudgetTable.currentPeriodStartedAt
    var resetsAt by GatewayBudgetTable.resetsAt
    var lastResetAt by GatewayBudgetTable.lastResetAt
    var cycleAnchorAt by GatewayBudgetTable.cycleAnchorAt
    var archivedAt by GatewayBudgetTable.archivedAt
    var createdAt by GatewayBudgetTable.createdAt
    var updatedAt by GatewayBudgetTable.updatedAt
    var createdById by GatewayBudgetTable.createdById
    var managedByVirtualKeyId by GatewayBudgetTable.managedByVirtualKeyId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GatewayBudgetTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
