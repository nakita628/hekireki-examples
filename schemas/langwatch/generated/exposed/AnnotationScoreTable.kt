package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AnnotationScoreTable : IdTable<String>("\"AnnotationScore\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val name = text("name")
    val projectId = text("projectId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val description = text("description").nullable()
    val active = bool("active").default(true)
    val dataType =
        pgEnum("dataType", "\"AnnotationScoreDataType\"", AnnotationScoreDataType.entries, AnnotationScoreDataType::dbName).nullable()
    val options1 = jsonb<String>("options", { it }, { it }).nullable()
    val defaultValue = jsonb<String>("defaultValue", { it }, { it }).nullable()
    val global = bool("global").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"AnnotationScore_pkey\"")

    init {
        index("\"AnnotationScore_projectId_idx\"", false, projectId)
    }
}
