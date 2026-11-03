package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.IntegerColumnType
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CommentTable : IdTable<String>("comments") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "comments_project_id_fkey",
        )
    val objectType = pgEnum("object_type", "\"CommentObjectType\"", CommentObjectType.entries, CommentObjectType::dbName)
    val objectId = text("object_id")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val content = text("content")
    val authorUserId = text("author_user_id").nullable()
    val dataField = text("data_field").nullable()
    val path = array("path", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val rangeStart = array("range_start", IntegerColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val rangeEnd = array("range_end", IntegerColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    override val primaryKey = PrimaryKey(id, name = "comments_pkey")

    init {
        index("comments_project_id_object_type_object_id_idx", false, projectId, objectType, objectId)
    }
}
