package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object SsoConnectionReproofCursorTable : IdTable<String>("\"SsoConnectionReproofCursor\"") {
    override val id = text("connectionId").entityId()
    val lastReproofAt = pgTimestamp("lastReproofAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"SsoConnectionReproofCursor_pkey\"")

    init {
        index("\"SsoConnectionReproofCursor_lastReproofAt_idx\"", false, lastReproofAt)
    }
}
