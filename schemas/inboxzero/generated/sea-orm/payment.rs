use super::processor_type::ProcessorType;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Payment")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "premiumId")]
    pub premium_id: Option<String>,
    #[sea_orm(column_name = "processorType", default_value = "LEMON_SQUEEZY")]
    pub processor_type: ProcessorType,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "processorId")]
    pub processor_id: Option<String>,
    #[sea_orm(column_name = "processorSubscriptionId")]
    pub processor_subscription_id: Option<String>,
    #[sea_orm(column_name = "processorCustomerId")]
    pub processor_customer_id: Option<String>,
    pub amount: i32,
    pub currency: String,
    pub status: String,
    pub tax: i32,
    #[sea_orm(column_name = "taxInclusive")]
    pub tax_inclusive: bool,
    #[sea_orm(default_value = false)]
    pub refunded: bool,
    #[sea_orm(column_name = "refundedAt")]
    pub refunded_at: Option<DateTime>,
    #[sea_orm(column_name = "refundedAmount")]
    pub refunded_amount: Option<i32>,
    #[sea_orm(column_name = "billingReason")]
    pub billing_reason: Option<String>,
    #[sea_orm(column_name = "invoiceEmailSentAt")]
    pub invoice_email_sent_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::premium::Entity",
        from = "Column::PremiumId",
        to = "super::premium::Column::Id",
        on_delete = "SetNull"
    )]
    Premium,
}

impl Related<super::premium::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Premium.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
