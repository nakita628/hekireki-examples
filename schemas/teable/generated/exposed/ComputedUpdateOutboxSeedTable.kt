package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ComputedUpdateOutboxSeedTable : IdTable<String>("computed_update_outbox_seed") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val taskId =
        reference(
            "task_id",
            ComputedUpdateOutboxTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "computed_update_outbox_seed_task_id_fkey",
        )
    val tableId = text("table_id")
    val recordId = text("record_id")
    override val primaryKey = PrimaryKey(id, name = "computed_update_outbox_seed_pkey")

    init {
        index("computed_update_outbox_seed_task_id_idx", false, taskId)
        uniqueIndex("computed_update_outbox_seed_task_id_table_id_record_id_key", taskId, tableId, recordId)
    }
}
