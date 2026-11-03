use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "EmailTransportType")]
pub enum EmailTransportType {
    #[sea_orm(string_value = "SMTP_AUTH")]
    SmtpAuth,
    #[sea_orm(string_value = "SMTP_API")]
    SmtpApi,
    #[sea_orm(string_value = "RESEND")]
    Resend,
    #[sea_orm(string_value = "MAILCHANNELS")]
    Mailchannels,
}
