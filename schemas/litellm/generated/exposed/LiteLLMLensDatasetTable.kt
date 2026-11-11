package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb

object LiteLLMLensDatasetTable : CompositeIdTable("\"LiteLLM_LensDataset\"") {
    val id1 = text("id").entityId()
    val revision = integer("revision").entityId()
    val createdAt = pgTimestamp("created_at", 3)
    val data = jsonb<String>("data", { it }, { it })
    override val primaryKey = PrimaryKey(id1, revision, name = "\"LiteLLM_LensDataset_pkey\"")
}
