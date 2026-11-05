package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object FilingFolderTable : IdTable<String>("\"FilingFolder\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val folderId = text("folderId")
    val folderName = text("folderName")
    val folderPath = text("folderPath")
    val driveConnectionId =
        reference(
            "driveConnectionId",
            DriveConnectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"FilingFolder_driveConnectionId_fkey\"",
        )
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"FilingFolder_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"FilingFolder_pkey\"")

    init {
        index("\"FilingFolder_driveConnectionId_idx\"", false, driveConnectionId)
        uniqueIndex("\"FilingFolder_emailAccountId_folderId_key\"", emailAccountId, folderId)
    }
}
