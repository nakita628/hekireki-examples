package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object SessionViewPinOverrideTable : IdTable<String>("session_view_pin_overrides") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "session_view_pin_overrides_project_id_fkey",
        )
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "session_view_pin_overrides_user_id_fkey",
        )
    val viewId = text("view_id").nullable()
    val isPinned = bool("is_pinned").default(true)
    val position = integer("position")
    override val primaryKey = PrimaryKey(id, name = "session_view_pin_overrides_pkey")

    init {
        index("session_view_pin_overrides_user_id_idx", false, userId)
        index("session_view_pin_overrides_project_id_view_id_idx", false, projectId, viewId)
        uniqueIndex("session_view_pin_overrides_project_id_user_id_view_id_key", projectId, userId, viewId)
        foreignKey(
            projectId to SessionViewTable.projectId,
            viewId to SessionViewTable.id,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "session_view_pin_overrides_project_id_view_id_fkey",
        )
    }
}
