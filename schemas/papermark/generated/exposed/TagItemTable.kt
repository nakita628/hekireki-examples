package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TagItemTable : IdTable<String>("\"TagItem\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val tagId =
        reference(
            "tagId",
            TagTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TagItem_tagId_fkey\"",
        )
    val itemType = pgEnum("itemType", "\"TagType\"", TagType.entries, TagType::dbName)
    val linkId =
        optReference(
            "linkId",
            LinkTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TagItem_linkId_fkey\"",
        )
    val documentId =
        optReference(
            "documentId",
            DocumentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TagItem_documentId_fkey\"",
        )
    val dataroomId =
        optReference(
            "dataroomId",
            DataroomTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TagItem_dataroomId_fkey\"",
        )
    val taggedBy = text("taggedBy").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TagItem_pkey\"")

    init {
        index("\"TagItem_tagId_linkId_idx\"", false, tagId, linkId)
        index("\"TagItem_tagId_documentId_idx\"", false, tagId, documentId)
        index("\"TagItem_tagId_dataroomId_idx\"", false, tagId, dataroomId)
    }
}
