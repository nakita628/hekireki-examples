package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object StoredObjectTable : CompositeIdTable("\"StoredObject\"") {
    val tenantId = text("tenantId").entityId()
    val id1 = text("id").entityId()
    val status = text("status")
    val purpose = text("purpose")
    val ownerKind = text("ownerKind")
    val ownerId = text("ownerId")
    val filename = text("filename")
    val sha256 = text("sha256")
    val sizeBytes = long("sizeBytes")
    val mediaType = text("mediaType")
    val mediaTypeVerified = bool("mediaTypeVerified").default(false)
    val storageProvider = text("storageProvider").nullable()
    val storageDestinationId = text("storageDestinationId").nullable()
    val storageProviderRelativeId = text("storageProviderRelativeId").nullable()
    val generation = integer("generation").default(0)
    val audiences = jsonb<String>("audiences", { it }, { it })
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val availableAt = pgTimestamp("availableAt", 3).nullable()
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val source1 = text("source")
    val legacyFingerprint = text("legacyFingerprint").nullable()
    val createdAt = pgTimestamp("createdAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(tenantId, id1, name = "\"StoredObject_pkey\"")

    init {
        index("\"StoredObject_tenantId_status_id_idx\"", false, tenantId, status, id1)
        index("\"StoredObject_ownerKind_ownerId_status_idx\"", false, ownerKind, ownerId, status)
        index("\"StoredObject_status_expiresAt_idx\"", false, status, expiresAt)
    }
}
