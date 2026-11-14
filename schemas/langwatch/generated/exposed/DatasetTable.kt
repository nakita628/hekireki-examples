package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DatasetTable : IdTable<String>("\"Dataset\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val name = text("name")
    val slug = text("slug")
    val columnTypes = jsonb<String>("columnTypes", { it }, { it })
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val mapping = jsonb<String>("mapping", { it }, { it }).nullable()
    val useS3 = bool("useS3").default(false)
    val s3RecordCount = integer("s3RecordCount").nullable()
    val contentLayout = text("contentLayout").default("postgres")
    val status = text("status").default("ready")
    val statusError = text("statusError").nullable()
    val stagingKey = text("stagingKey").nullable()
    val sourceStoredObjectId = text("sourceStoredObjectId").nullable()
    val uploadFilename = text("uploadFilename").nullable()
    val rowCount = integer("rowCount").nullable()
    val sizeBytes = long("sizeBytes").nullable()
    val chunkCount = integer("chunkCount").nullable()
    val chunkOffsets = jsonb<String>("chunkOffsets", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Dataset_pkey\"")

    init {
        uniqueIndex("\"Dataset_projectId_slug_key\"", projectId, slug)
    }
}
