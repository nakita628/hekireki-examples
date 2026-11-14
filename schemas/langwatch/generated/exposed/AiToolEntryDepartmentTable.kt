package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object AiToolEntryDepartmentTable : IdTable<String>("\"AiToolEntryDepartment\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val entryId = text("entryId")
    val departmentId = text("departmentId")
    override val primaryKey = PrimaryKey(id, name = "\"AiToolEntryDepartment_pkey\"")

    init {
        index("\"AiToolEntryDepartment_departmentId_idx\"", false, departmentId)
        uniqueIndex("\"AiToolEntryDepartment_entryId_departmentId_key\"", entryId, departmentId)
    }
}
