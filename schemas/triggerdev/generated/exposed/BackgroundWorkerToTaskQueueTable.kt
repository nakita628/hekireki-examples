package triggerdev.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.Table

object BackgroundWorkerToTaskQueueTable : Table("\"_BackgroundWorkerToTaskQueue\"") {
    val a =
        reference(
            "\"A\"",
            BackgroundWorkerTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_BackgroundWorkerToTaskQueue_A_fkey\"",
        )
    val b =
        reference(
            "\"B\"",
            TaskQueueTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_BackgroundWorkerToTaskQueue_B_fkey\"",
        )
    override val primaryKey = PrimaryKey(a, b, name = "\"_BackgroundWorkerToTaskQueue_AB_pkey\"")

    init {
        index("\"_BackgroundWorkerToTaskQueue_B_index\"", false, b)
    }
}
