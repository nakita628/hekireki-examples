package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SavedViewTable : IdTable<String>("\"SavedView\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val userId = text("userId").nullable()
    val name = text("name")
    val filters = jsonb<String>("filters", { it }, { it }).default("{}")
    val query = text("query").nullable()
    val period = jsonb<String>("period", { it }, { it }).nullable()
    val order = integer("order").default(0)
    val kind = text("kind").default("v1-traces-filter")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"SavedView_pkey\"")

    init {
        index("\"SavedView_projectId_idx\"", false, projectId)
        index("\"SavedView_projectId_order_idx\"", false, projectId, order)
        index("\"SavedView_userId_idx\"", false, userId)
        index("\"SavedView_projectId_kind_order_idx\"", false, projectId, kind, order)
    }
}
