package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DataroomFolderTable : IdTable<String>("\"DataroomFolder\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val path = text("path")
    val parentId =
        optReference(
            "parentId",
            DataroomFolderTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomFolder_parentId_fkey\"",
        )
    val icon = text("icon").nullable()
    val color = text("color").nullable()
    val dataroomId =
        reference(
            "dataroomId",
            DataroomTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomFolder_dataroomId_fkey\"",
        )
    val orderIndex = integer("orderIndex").nullable()
    val hierarchicalIndex = text("hierarchicalIndex").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DataroomFolder_pkey\"")

    init {
        index("\"DataroomFolder_parentId_idx\"", false, parentId)
        index("\"DataroomFolder_dataroomId_parentId_orderIndex_idx\"", false, dataroomId, parentId, orderIndex)
        uniqueIndex("\"DataroomFolder_dataroomId_path_key\"", dataroomId, path)
    }
}
