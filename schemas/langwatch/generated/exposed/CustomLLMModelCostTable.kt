package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CustomLLMModelCostTable : IdTable<String>("\"CustomLLMModelCost\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val scopeType =
        pgEnum("scopeType", "\"CustomLLMModelCostScopeType\"", CustomLLMModelCostScopeType.entries, CustomLLMModelCostScopeType::dbName)
            .default(CustomLLMModelCostScopeType.PROJECT)
    val scopeId = text("scopeId")
    val projectId = text("projectId").nullable()
    val model = text("model")
    val regex = text("regex")
    val inputCostPerToken = double("inputCostPerToken").nullable()
    val outputCostPerToken = double("outputCostPerToken").nullable()
    val cacheReadCostPerToken = double("cacheReadCostPerToken").nullable()
    val cacheCreationCostPerToken = double("cacheCreationCostPerToken").nullable()
    val cacheCreation1hCostPerToken = double("cacheCreation1hCostPerToken").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"CustomLLMModelCost_pkey\"")

    init {
        index("\"CustomLLMModelCost_organizationId_idx\"", false, organizationId)
        index("\"CustomLLMModelCost_scopeType_scopeId_idx\"", false, scopeType, scopeId)
        index("\"CustomLLMModelCost_projectId_idx\"", false, projectId)
    }
}
