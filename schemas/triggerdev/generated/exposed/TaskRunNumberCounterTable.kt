package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object TaskRunNumberCounterTable : IdTable<String>("\"TaskRunNumberCounter\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val taskIdentifier = text("taskIdentifier")
    val environmentId =
        reference(
            "environmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunNumberCounter_environmentId_fkey\"",
        )
    val lastNumber = integer("lastNumber").default(0)
    override val primaryKey = PrimaryKey(id, name = "\"TaskRunNumberCounter_pkey\"")

    init {
        uniqueIndex("\"TaskRunNumberCounter_taskIdentifier_environmentId_key\"", taskIdentifier, environmentId)
    }
}
