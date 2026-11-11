package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object LiteLLMLensTable : IdTable<String>("\"LiteLLM_Lens\"") {
    override val id = text("id").entityId()
    val version = integer("version").default(0)
    val data = jsonb<String>("data", { it }, { it })
    val dueAt = pgTimestamp("due_at", 3).nullable().databaseDefault("'1970-01-01 00:00:00'::timestamp without time zone")
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_Lens_pkey\"")

    init {
        index("\"LiteLLM_Lens_due_at_idx\"", false, dueAt)
    }
}
