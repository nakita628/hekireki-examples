package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object RetentionRunItemTable : IdTable<String>("\"RetentionRunItem\"") {
    override val id = text("id").clientDefault { CUID.randomCUID2().toString() }.entityId()
    val runId =
        reference(
            "runId",
            RetentionRunTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RetentionRunItem_runId_fkey\"",
        )
    val targetType = pgEnum("targetType", "\"RetentionTargetType\"", RetentionTargetType.entries, RetentionTargetType::dbName)
    val targetId = text("targetId")
    val targetName = text("targetName").nullable()
    val action = pgEnum("action", "\"RetentionRunItemAction\"", RetentionRunItemAction.entries, RetentionRunItemAction::dbName)
    val count = integer("count").default(1)
    val recipient = text("recipient").nullable()
    val skipReason = pgEnum("skipReason", "\"RetentionSkipReason\"", RetentionSkipReason.entries, RetentionSkipReason::dbName).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"RetentionRunItem_pkey\"")

    init {
        index("\"RetentionRunItem_runId_id_idx\"", false, runId, id)
        index("\"RetentionRunItem_targetId_idx\"", false, targetId)
    }
}
