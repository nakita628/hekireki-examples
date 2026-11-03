package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ClippingClipTable : IdTable<String>("\"ClippingClip\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val clippingId =
        reference(
            "clippingId",
            ClippingTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ClippingClip_clippingId_fkey\"",
        )
    val title = text("title")
    val content = text("content")
    val start = double("start")
    val end = double("end")
    val trimStart = integer("trimStart").nullable()
    val status = text("status").default("pending")
    val error = text("error").nullable()
    val mediaId = text("mediaId").nullable()
    val path = text("path").nullable()
    val thumbnail = text("thumbnail").nullable()
    val draftedAt = pgTimestamp("draftedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ClippingClip_pkey\"")

    init {
        index("\"ClippingClip_clippingId_idx\"", false, clippingId)
    }
}
