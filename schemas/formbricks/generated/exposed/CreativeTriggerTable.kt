package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CreativeTriggerTable : IdTable<String>("\"CreativeTrigger\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val creativeId =
        reference(
            "creativeId",
            CreativeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CreativeTrigger_creativeId_fkey\"",
        )
    val actionClassId =
        reference(
            "actionClassId",
            ActionClassTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CreativeTrigger_actionClassId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"CreativeTrigger_pkey\"")

    init {
        index("\"CreativeTrigger_actionClassId_idx\"", false, actionClassId)
        uniqueIndex("\"CreativeTrigger_creativeId_actionClassId_key\"", creativeId, actionClassId)
    }
}
