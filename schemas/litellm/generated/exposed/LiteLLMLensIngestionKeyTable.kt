package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object LiteLLMLensIngestionKeyTable : IdTable<String>("\"LiteLLM_LensIngestionKey\"") {
    override val id = text("id").entityId()
    val data = jsonb<String>("data", { it }, { it })
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_LensIngestionKey_pkey\"")
}
