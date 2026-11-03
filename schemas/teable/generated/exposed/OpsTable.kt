package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OpsTable : IdTable<String>("ops") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val collection = text("collection")
    val docId = text("doc_id")
    val docType = text("doc_type")
    val version = integer("version")
    val operation = text("operation")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    override val primaryKey = PrimaryKey(id, name = "ops_pkey")

    init {
        index("ops_collection_created_time_idx", false, collection, createdTime)
        uniqueIndex("ops_collection_doc_id_version_key", collection, docId, version)
    }
}
