package litellm.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMBudgetTableTable : IdTable<String>("\"LiteLLM_BudgetTable\"") {
    override val id = text("budget_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name").nullable()
    val maxBudget = double("max_budget").nullable()
    val softBudget = double("soft_budget").nullable()
    val maxParallelRequests = integer("max_parallel_requests").nullable()
    val tpmLimit = long("tpm_limit").nullable()
    val rpmLimit = long("rpm_limit").nullable()
    val tpdLimit = long("tpd_limit").nullable()
    val modelMaxBudget = jsonb<String>("model_max_budget", { it }, { it }).nullable()
    val modelTpmLimit = jsonb<String>("model_tpm_limit", { it }, { it }).nullable()
    val modelRpmLimit = jsonb<String>("model_rpm_limit", { it }, { it }).nullable()
    val budgetDuration = text("budget_duration").nullable()
    val budgetLimits = jsonb<String>("budget_limits", { it }, { it }).nullable()
    val capPolicy = text("cap_policy").nullable()
    val budgetResetAt = pgTimestamp("budget_reset_at", 3).nullable()
    val allowedModels = array("allowed_models", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val tempBudgetIncrease = double("temp_budget_increase").nullable()
    val tempBudgetExpiry = pgTimestamp("temp_budget_expiry", 3).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by")
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_BudgetTable_pkey\"")
}
