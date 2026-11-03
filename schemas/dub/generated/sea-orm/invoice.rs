use super::invoice_status::InvoiceStatus;
use super::invoice_type::InvoiceType;
use super::payment_method::PaymentMethod;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Invoice")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "workspaceId")]
    pub workspace_id: String,
    #[sea_orm(unique)]
    pub number: Option<String>,
    #[sea_orm(default_value = "processing")]
    pub status: InvoiceStatus,
    #[sea_orm(default_value = "domainRenewal")]
    pub r#type: InvoiceType,
    #[sea_orm(column_name = "paymentMethod")]
    pub payment_method: Option<PaymentMethod>,
    #[sea_orm(default_value = 0)]
    pub amount: i32,
    #[sea_orm(default_value = 0)]
    pub fee: i32,
    #[sea_orm(column_name = "refundedAmount", default_value = 0)]
    pub refunded_amount: i32,
    #[sea_orm(default_value = 0)]
    pub total: i32,
    #[sea_orm(column_name = "receiptUrl", column_type = "Text")]
    pub receipt_url: Option<String>,
    #[sea_orm(column_name = "refundReason", column_type = "Text")]
    pub refund_reason: Option<String>,
    #[sea_orm(column_name = "failedReason", column_type = "Text")]
    pub failed_reason: Option<String>,
    #[sea_orm(column_name = "registeredDomains")]
    pub registered_domains: Option<Json>,
    #[sea_orm(column_name = "stripeChargeMetadata")]
    pub stripe_charge_metadata: Option<Json>,
    #[sea_orm(column_name = "failedAttempts", default_value = 0)]
    pub failed_attempts: i32,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "paidAt")]
    pub paid_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::WorkspaceId",
        to = "super::project::Column::Id"
    )]
    Workspace,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workspace.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
