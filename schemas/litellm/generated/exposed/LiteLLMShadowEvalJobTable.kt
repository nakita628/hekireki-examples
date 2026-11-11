package litellm.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMShadowEvalJobTable : IdTable<String>("\"LiteLLM_ShadowEvalJob\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val groupId = text("group_id")
    val targetType = text("target_type").default("key")
    val targetId = text("target_id")
    val routerName = text("router_name")
    val routerNames = array("router_names", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val models = array("models", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val direction = text("direction").default("forward")
    val baselineModel = text("baseline_model").nullable()
    val judgeModel = text("judge_model")
    val shadowPercentage = double("shadow_percentage")
    val maxTurns = integer("max_turns")
    val maxBudget = double("max_budget").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by").nullable()
    val endsAt = pgTimestamp("ends_at", 3)
    val stoppedAt = pgTimestamp("stopped_at", 3).nullable()
    val stoppedBy = text("stopped_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ShadowEvalJob_pkey\"")

    init {
        index("\"LiteLLM_ShadowEvalJob_group_id_idx\"", false, groupId)
        index("\"LiteLLM_ShadowEvalJob_target_type_target_id_idx\"", false, targetType, targetId)
        index("\"LiteLLM_ShadowEvalJob_created_at_idx\"", false, createdAt)
    }
}
