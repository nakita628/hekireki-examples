package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ViewerGroupAccessControlsTable : IdTable<String>("\"ViewerGroupAccessControls\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val groupId =
        reference(
            "groupId",
            ViewerGroupTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ViewerGroupAccessControls_groupId_fkey\"",
        )
    val itemId = text("itemId")
    val itemType = pgEnum("itemType", "\"ItemType\"", ItemType.entries, ItemType::dbName)
    val canView = bool("canView").default(true)
    val canDownload = bool("canDownload").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ViewerGroupAccessControls_pkey\"")

    init {
        index("\"ViewerGroupAccessControls_groupId_idx\"", false, groupId)
        uniqueIndex("\"ViewerGroupAccessControls_groupId_itemId_key\"", groupId, itemId)
    }
}
