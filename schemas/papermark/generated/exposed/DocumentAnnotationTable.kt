package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.IntegerColumnType
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DocumentAnnotationTable : IdTable<String>("\"DocumentAnnotation\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val title = text("title")
    val content = jsonb<String>("content", { it }, { it })
    val pages = array("pages", IntegerColumnType()).nullable()
    val documentId =
        reference(
            "documentId",
            DocumentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentAnnotation_documentId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentAnnotation_teamId_fkey\"",
        )
    val createdById =
        reference(
            "createdById",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentAnnotation_createdById_fkey\"",
        )
    val isVisible = bool("isVisible").default(true)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DocumentAnnotation_pkey\"")

    init {
        index("\"DocumentAnnotation_documentId_idx\"", false, documentId)
        index("\"DocumentAnnotation_teamId_idx\"", false, teamId)
        index("\"DocumentAnnotation_createdById_idx\"", false, createdById)
    }
}
