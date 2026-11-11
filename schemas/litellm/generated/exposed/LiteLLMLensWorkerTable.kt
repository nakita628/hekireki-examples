package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object LiteLLMLensWorkerTable : IdTable<String>("\"LiteLLM_LensWorker\"") {
    override val id = text("id").entityId()
    val tokenHash = text("token_hash").uniqueIndex("\"LiteLLM_LensWorker_token_hash_key\"")
    val data = jsonb<String>("data", { it }, { it })
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_LensWorker_pkey\"")
}
