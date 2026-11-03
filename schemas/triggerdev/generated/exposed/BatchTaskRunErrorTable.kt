package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object BatchTaskRunErrorTable : IdTable<String>("\"BatchTaskRunError\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val batchTaskRunId =
        reference(
            "batchTaskRunId",
            BatchTaskRunTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BatchTaskRunError_batchTaskRunId_fkey\"",
        )
    val index = integer("index")
    val taskIdentifier = text("taskIdentifier")
    val payload = text("payload").nullable()
    val options1 = jsonb<String>("options", { it }, { it }).nullable()
    val error = text("error")
    val errorCode = text("errorCode").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"BatchTaskRunError_pkey\"")

    init {
        index("\"BatchTaskRunError_batchTaskRunId_idx\"", false, batchTaskRunId)
        uniqueIndex("\"BatchTaskRunError_batchTaskRunId_index_key\"", batchTaskRunId, index)
    }
}
