package teable.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object ComputedUpdateDeadLetterSeedTable : CompositeIdTable("computed_update_dead_letter_seed") {
    val taskId =
        reference(
            "task_id",
            ComputedUpdateDeadLetterTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "computed_update_dead_letter_seed_task_id_fkey",
        )
    val tableId = text("table_id").entityId()
    val recordId = text("record_id").entityId()
    override val primaryKey = PrimaryKey(taskId, tableId, recordId, name = "computed_update_dead_letter_seed_pkey")

    init {
        addIdColumn(taskId)
    }
}
