package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AnnotationTable : IdTable<String>("\"Annotation\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val traceId = text("traceId")
    val comment = text("comment").nullable()
    val isThumbsUp = bool("isThumbsUp").nullable()
    val userId = text("userId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val email = text("email").nullable()
    val scoreOptions = jsonb<String>("scoreOptions", { it }, { it }).nullable()
    val expectedOutput = text("expectedOutput").nullable()
    val anchorKind = text("anchorKind").nullable()
    val anchorId = text("anchorId").nullable()
    val anchorPath = text("anchorPath").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Annotation_pkey\"")

    init {
        index("\"Annotation_projectId_idx\"", false, projectId)
        index("\"Annotation_traceId_idx\"", false, traceId)
        index("\"Annotation_userId_idx\"", false, userId)
        index("\"Annotation_projectId_traceId_idx\"", false, projectId, traceId)
        index("\"Annotation_projectId_traceId_anchorKind_idx\"", false, projectId, traceId, anchorKind)
    }
}
