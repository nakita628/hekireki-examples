package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object GatewayBudgetBucketBoundaryTable : IdTable<String>("\"GatewayBudgetBucketBoundary\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val budgetId = text("budgetId")
    val bucketScopeId = text("bucketScopeId")
    val periodStartedAt = pgTimestamp("periodStartedAt", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"GatewayBudgetBucketBoundary_pkey\"")

    init {
        index("\"GatewayBudgetBucketBoundary_organizationId_idx\"", false, organizationId)
        uniqueIndex("\"GatewayBudgetBucketBoundary_budgetId_bucketScopeId_key\"", budgetId, bucketScopeId)
    }
}
