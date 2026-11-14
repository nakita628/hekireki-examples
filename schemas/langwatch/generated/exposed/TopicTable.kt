package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TopicTable : IdTable<String>("\"Topic\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val name = text("name")
    val parentId = text("parentId").nullable()
    val embeddingsModel = text("embeddings_model")
    val centroid = jsonb<String>("centroid", { it }, { it })
    val p95Distance = double("p95Distance")
    val automaticallyGenerated = bool("automaticallyGenerated").default(true)
    val lastEventId = text("lastEventId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Topic_pkey\"")

    init {
        index("\"Topic_parentId_idx\"", false, parentId)
        index("\"Topic_projectId_idx\"", false, projectId)
    }
}
