package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object BackgroundWorkerFileTable : IdTable<String>("\"BackgroundWorkerFile\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendlyId").uniqueIndex("\"BackgroundWorkerFile_friendlyId_key\"")
    val filePath = text("filePath")
    val contentHash = text("contentHash")
    val contents = binary("contents")
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BackgroundWorkerFile_projectId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"BackgroundWorkerFile_pkey\"")

    init {
        uniqueIndex("\"BackgroundWorkerFile_projectId_contentHash_key\"", projectId, contentHash)
    }
}
