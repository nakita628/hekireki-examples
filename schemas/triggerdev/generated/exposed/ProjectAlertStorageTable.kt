package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ProjectAlertStorageTable : IdTable<String>("\"ProjectAlertStorage\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ProjectAlertStorage_projectId_fkey\"",
        )
    val alertChannelId =
        reference(
            "alertChannelId",
            ProjectAlertChannelTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ProjectAlertStorage_alertChannelId_fkey\"",
        )
    val alertType = pgEnum("alertType", "\"ProjectAlertType\"", ProjectAlertType.entries, ProjectAlertType::dbName)
    val storageId = text("storageId")
    val storageData = jsonb<String>("storageData", { it }, { it })
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ProjectAlertStorage_pkey\"")

    init {
        index("\"ProjectAlertStorage_alertChannelId_alertType_storageId_idx\"", false, alertChannelId, alertType, storageId)
    }
}
