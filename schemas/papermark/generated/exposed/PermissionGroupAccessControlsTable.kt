package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PermissionGroupAccessControlsTable : IdTable<String>("\"PermissionGroupAccessControls\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val groupId =
        reference(
            "groupId",
            PermissionGroupTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PermissionGroupAccessControls_groupId_fkey\"",
        )
    val itemId = text("itemId")
    val itemType = pgEnum("itemType", "\"ItemType\"", ItemType.entries, ItemType::dbName)
    val canView = bool("canView").default(true)
    val canDownload = bool("canDownload").default(false)
    val canDownloadOriginal = bool("canDownloadOriginal").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"PermissionGroupAccessControls_pkey\"")

    init {
        index("\"PermissionGroupAccessControls_groupId_idx\"", false, groupId)
        uniqueIndex("\"PermissionGroupAccessControls_groupId_itemId_key\"", groupId, itemId)
    }
}
