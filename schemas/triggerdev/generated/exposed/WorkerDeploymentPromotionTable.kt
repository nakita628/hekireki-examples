package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object WorkerDeploymentPromotionTable : IdTable<String>("\"WorkerDeploymentPromotion\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val label = text("label")
    val deploymentId =
        reference(
            "deploymentId",
            WorkerDeploymentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkerDeploymentPromotion_deploymentId_fkey\"",
        )
    val environmentId =
        reference(
            "environmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkerDeploymentPromotion_environmentId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"WorkerDeploymentPromotion_pkey\"")

    init {
        uniqueIndex("\"WorkerDeploymentPromotion_environmentId_label_key\"", environmentId, label)
    }
}
