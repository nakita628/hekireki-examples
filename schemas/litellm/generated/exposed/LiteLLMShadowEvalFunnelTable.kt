package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object LiteLLMShadowEvalFunnelTable : IdTable<String>("\"LiteLLM_ShadowEvalFunnel\"") {
    override val id = text("job_id").entityId()
    val notSampled = integer("not_sampled").default(0)
    val unjudgeable = integer("unjudgeable").default(0)
    val shed = integer("shed").default(0)
    val withheld = integer("withheld").default(0)
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ShadowEvalFunnel_pkey\"")
}
