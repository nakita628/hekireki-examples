package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AnnotationImageTable : IdTable<String>("\"AnnotationImage\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val filename = text("filename")
    val url = text("url")
    val size = integer("size").nullable()
    val mimeType = text("mimeType").nullable()
    val annotationId =
        reference(
            "annotationId",
            DocumentAnnotationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AnnotationImage_annotationId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AnnotationImage_pkey\"")

    init {
        index("\"AnnotationImage_annotationId_idx\"", false, annotationId)
    }
}
