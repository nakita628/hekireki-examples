package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object LiteLLMConfigTable : IdTable<String>("\"LiteLLM_Config\"") {
    override val id = text("param_name").entityId()
    val paramValue = jsonb<String>("param_value", { it }, { it }).nullable()
    val lastRunAt = pgTimestamp("last_run_at", 3).nullable()
    val reloadRevision = long("reload_revision").default(0L)
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_Config_pkey\"")
}
