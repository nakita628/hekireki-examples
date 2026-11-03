package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DocumentPageTable : IdTable<String>("\"DocumentPage\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val versionId =
        reference(
            "versionId",
            DocumentVersionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentPage_versionId_fkey\"",
        )
    val pageNumber = integer("pageNumber")
    val embeddedLinks = array("embeddedLinks", TextColumnType()).nullable()
    val pageLinks = jsonb<String>("pageLinks", { it }, { it }).nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val file = text("file")
    val storageType =
        pgEnum("storageType", "\"DocumentStorageType\"", DocumentStorageType.entries, DocumentStorageType::dbName).default(
            DocumentStorageType.VERCEL_BLOB,
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DocumentPage_pkey\"")

    init {
        index("\"DocumentPage_versionId_idx\"", false, versionId)
        uniqueIndex("\"DocumentPage_pageNumber_versionId_key\"", pageNumber, versionId)
    }
}
