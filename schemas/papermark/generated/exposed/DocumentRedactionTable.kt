package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DocumentRedactionTable : IdTable<String>("\"DocumentRedaction\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val jobId =
        reference(
            "jobId",
            DocumentRedactionJobTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentRedaction_jobId_fkey\"",
        )
    val pageNumber = integer("pageNumber")
    val x = double("x")
    val y = double("y")
    val width = double("width")
    val height = double("height")
    val textPreview = text("textPreview").nullable()
    val category = text("category").nullable()
    val confidence = text("confidence").nullable()
    val reason = text("reason").nullable()
    val source1 = text("source").default("AI")
    val status = text("status").default("PENDING")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DocumentRedaction_pkey\"")

    init {
        index("\"DocumentRedaction_jobId_idx\"", false, jobId)
        index("\"DocumentRedaction_jobId_pageNumber_idx\"", false, jobId, pageNumber)
        index("\"DocumentRedaction_jobId_status_idx\"", false, jobId, status)
    }
}
