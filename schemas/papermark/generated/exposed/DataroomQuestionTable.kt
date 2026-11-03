package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DataroomQuestionTable : IdTable<String>("\"DataroomQuestion\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val content = text("content")
    val category = text("category").nullable()
    val priority = text("priority").nullable()
    val status = text("status").default("unanswered")
    val orderIndex = integer("orderIndex").nullable()
    val importBatchId = text("importBatchId").nullable()
    val dataroomId =
        reference(
            "dataroomId",
            DataroomTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomQuestion_dataroomId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomQuestion_teamId_fkey\"",
        )
    val createdByUserId =
        optReference(
            "createdByUserId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomQuestion_createdByUserId_fkey\"",
        )
    val dataroomDocumentId =
        optReference(
            "dataroomDocumentId",
            DataroomDocumentTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomQuestion_dataroomDocumentId_fkey\"",
        )
    val documentPageNumber = integer("documentPageNumber").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DataroomQuestion_pkey\"")

    init {
        index("\"DataroomQuestion_dataroomId_idx\"", false, dataroomId)
        index("\"DataroomQuestion_teamId_idx\"", false, teamId)
        index("\"DataroomQuestion_status_idx\"", false, status)
        index("\"DataroomQuestion_importBatchId_idx\"", false, importBatchId)
        index("\"DataroomQuestion_dataroomDocumentId_idx\"", false, dataroomDocumentId)
        index("\"DataroomQuestion_createdByUserId_idx\"", false, createdByUserId)
    }
}
