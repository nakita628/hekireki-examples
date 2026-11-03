package triggerdev.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.Table

object BackgroundWorkerToBackgroundWorkerFileTable : Table("\"_BackgroundWorkerToBackgroundWorkerFile\"") {
    val a =
        reference(
            "\"A\"",
            BackgroundWorkerTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_BackgroundWorkerToBackgroundWorkerFile_A_fkey\"",
        )
    val b =
        reference(
            "\"B\"",
            BackgroundWorkerFileTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_BackgroundWorkerToBackgroundWorkerFile_B_fkey\"",
        )
    override val primaryKey = PrimaryKey(a, b, name = "\"_BackgroundWorkerToBackgroundWorkerFile_AB_pkey\"")

    init {
        index("\"_BackgroundWorkerToBackgroundWorkerFile_B_index\"", false, b)
    }
}
