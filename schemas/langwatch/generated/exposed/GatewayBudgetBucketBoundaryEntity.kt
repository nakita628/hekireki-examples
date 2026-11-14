package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GatewayBudgetBucketBoundaryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GatewayBudgetBucketBoundaryEntity>(GatewayBudgetBucketBoundaryTable)

    var organizationId by GatewayBudgetBucketBoundaryTable.organizationId
    var budgetId by GatewayBudgetBucketBoundaryTable.budgetId
    var bucketScopeId by GatewayBudgetBucketBoundaryTable.bucketScopeId
    var periodStartedAt by GatewayBudgetBucketBoundaryTable.periodStartedAt
    var createdAt by GatewayBudgetBucketBoundaryTable.createdAt
    var updatedAt by GatewayBudgetBucketBoundaryTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GatewayBudgetBucketBoundaryTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
