package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object LiteLLMLensRunTable : IdTable<String>("\"LiteLLM_LensRun\"") {
    override val id = text("id").entityId()
    val lensId = text("lens_id")
    val createdAt = pgTimestamp("created_at", 3)
    val data = jsonb<String>("data", { it }, { it })
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_LensRun_pkey\"")

    init {
        index("\"LiteLLM_LensRun_lens_id_created_at_idx\"", false, lensId, createdAt)
    }
}
