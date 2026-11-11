package litellm.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMShadowEvalAttemptTable : IdTable<String>("\"LiteLLM_ShadowEvalAttempt\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val jobId = text("job_id")
    val requestId = text("request_id")
    val outcome = text("outcome")
    val routerName = text("router_name").nullable()
    val tier = text("tier").nullable()
    val realModel = text("real_model").nullable()
    val shadowModel = text("shadow_model").nullable()
    val confidence = double("confidence").nullable()
    val judgeCost = double("judge_cost").default(0.0)
    val shadowCost = double("shadow_cost").default(0.0)
    val realCost = double("real_cost").nullable()
    val realClassifierCost = double("real_classifier_cost").default(0.0)
    val shadowClassifierCost = double("shadow_classifier_cost").default(0.0)
    val realCacheHit = bool("real_cache_hit").default(false)
    val error = text("error").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ShadowEvalAttempt_pkey\"")

    init {
        index("\"LiteLLM_ShadowEvalAttempt_job_id_idx\"", false, jobId)
    }
}
