package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AnnotationQueueTable : IdTable<String>("\"AnnotationQueue\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val name = text("name")
    val slug = text("slug")
    val projectId = text("projectId")
    val description = text("description").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AnnotationQueue_pkey\"")

    init {
        index("\"AnnotationQueue_projectId_idx\"", false, projectId)
        uniqueIndex("\"AnnotationQueue_projectId_slug_key\"", projectId, slug)
    }
}
