package linkwarden.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TagTable : IdTable<Int>("\"Tag\"") {
    override val id = integer("id").autoIncrement().entityId()
    val name = text("name")
    val ownerId =
        reference(
            "ownerId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Tag_ownerId_fkey\"",
        )
    val archiveAsScreenshot = bool("archiveAsScreenshot").nullable()
    val archiveAsMonolith = bool("archiveAsMonolith").nullable()
    val archiveAsPDF = bool("archiveAsPDF").nullable()
    val archiveAsReadable = bool("archiveAsReadable").nullable()
    val archiveAsWaybackMachine = bool("archiveAsWaybackMachine").nullable()
    val aiTag = bool("aiTag").nullable()
    val aiGenerated = bool("aiGenerated").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Tag_pkey\"")

    init {
        index("\"Tag_ownerId_idx\"", false, ownerId)
        uniqueIndex("\"Tag_name_ownerId_key\"", name, ownerId)
    }
}
