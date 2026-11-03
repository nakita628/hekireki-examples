package documenso.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object DocumentMetaTable : IdTable<String>("\"DocumentMeta\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val subject = text("subject").nullable()
    val message = text("message").nullable()
    val timezone = text("timezone").nullable().default("Etc/UTC")
    val dateFormat = text("dateFormat").nullable().default("yyyy-MM-dd hh:mm a")
    val redirectUrl = text("redirectUrl").nullable()
    val signingOrder =
        pgEnum("signingOrder", "\"DocumentSigningOrder\"", DocumentSigningOrder.entries, DocumentSigningOrder::dbName).default(
            DocumentSigningOrder.PARALLEL,
        )
    val allowDictateNextSigner = bool("allowDictateNextSigner").default(false)
    val typedSignatureEnabled = bool("typedSignatureEnabled").default(true)
    val uploadSignatureEnabled = bool("uploadSignatureEnabled").default(true)
    val drawSignatureEnabled = bool("drawSignatureEnabled").default(true)
    val language = text("language").default("en")
    val distributionMethod =
        pgEnum(
            "distributionMethod",
            "\"DocumentDistributionMethod\"",
            DocumentDistributionMethod.entries,
            DocumentDistributionMethod::dbName,
        ).default(DocumentDistributionMethod.EMAIL)
    val emailSettings = jsonb<String>("emailSettings", { it }, { it }).nullable()
    val emailReplyTo = text("emailReplyTo").nullable()
    val emailId = text("emailId").nullable()
    val envelopeExpirationPeriod = jsonb<String>("envelopeExpirationPeriod", { it }, { it }).nullable()
    val reminderSettings = jsonb<String>("reminderSettings", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"DocumentMeta_pkey\"")
}
