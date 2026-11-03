package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskRunTagTable : IdTable<String>("\"TaskRunTag\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val friendlyId = text("friendlyId").uniqueIndex("\"TaskRunTag_friendlyId_key\"")
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunTag_projectId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TaskRunTag_pkey\"")

    init {
        index("\"TaskRunTag_name_id_idx\"", false, name, id)
        uniqueIndex("\"TaskRunTag_projectId_name_key\"", projectId, name)
    }
}
